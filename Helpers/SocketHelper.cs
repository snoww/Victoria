using System;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Newtonsoft.Json;
using Victoria.Entities.Payloads;

namespace Victoria.Helpers {
	internal sealed class SocketHelper {
		private readonly Configuration _config;
		private readonly Encoding _encoding;
		private readonly Func<LogMessage, Task> ShadowLog;
		private CancellationTokenSource _cancellationTokenSource;
		private ClientWebSocket _clientWebSocket;
		private TimeSpan _interval;
		private bool _isUseable;
		private int _reconnectAttempts;

		public SocketHelper(Configuration configuration, Func<LogMessage, Task> log) {
			ShadowLog = log;
			_config = configuration;
			_encoding = new UTF8Encoding(false);
			ServicePointManager.ServerCertificateValidationCallback += (_, __, ___, ____) => true;
		}

		public event Func<Task> OnClosed;
		public event Func<string, bool> OnMessage;

		public async Task ConnectAsync() {
			_cancellationTokenSource = new CancellationTokenSource();

			_clientWebSocket = new ClientWebSocket();
			_clientWebSocket.Options.SetRequestHeader("User-Id", $"{_config.UserId}");
			_clientWebSocket.Options.SetRequestHeader("Num-Shards", $"{_config.Shards}");
			_clientWebSocket.Options.SetRequestHeader("Authorization", _config.Password);
			var url = new Uri($"ws://{_config.Host}:{_config.Port}");

			if (_reconnectAttempts == _config.ReconnectAttempts) {
				return;
			}

			try {
				ShadowLog?.WriteLog(LogSeverity.Info, $"Connecting to {url}.");
				await _clientWebSocket.ConnectAsync(url, CancellationToken.None).ContinueWith(VerifyConnectionAsync);
			}
			catch {
			}
		}

		public Task SendPayloadAsync(BasePayload payload) {
			if (!_isUseable) {
				return Task.CompletedTask;
			}

			var serialize = JsonConvert.SerializeObject(payload);
			ShadowLog?.WriteLog(LogSeverity.Debug, serialize);
			var seg = new ArraySegment<byte>(_encoding.GetBytes(serialize));
			return _clientWebSocket.SendAsync(seg, WebSocketMessageType.Text, true, CancellationToken.None);
		}

		public async ValueTask DisposeAsync() {
			_isUseable = false;

			await _clientWebSocket
			   .CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed called.", CancellationToken.None)
			   .ConfigureAwait(false);

			_cancellationTokenSource.Cancel(false);
			_clientWebSocket.Dispose();
		}

		private async Task VerifyConnectionAsync(Task task) {
			if (task.IsCanceled || task.IsFaulted || task.Exception != null) {
				_isUseable = false;
				await RetryConnectionAsync().ConfigureAwait(false);
			}
			else {
				ShadowLog?.WriteLog(LogSeverity.Info, "WebSocket connection established!");
				_isUseable = true;
				_reconnectAttempts = 0;
				await ReceiveAsync(_cancellationTokenSource.Token).ConfigureAwait(false);
			}
		}

		private async Task RetryConnectionAsync() {
			_cancellationTokenSource.Cancel(false);

			if (_reconnectAttempts > _config.ReconnectAttempts && _config.ReconnectAttempts != -1) {
				return;
			}

			if (_isUseable) {
				return;
			}

			_reconnectAttempts++;
			_interval += _config.ReconnectInterval;
			ShadowLog?.WriteLog(LogSeverity.Warning,
				_reconnectAttempts == _config.ReconnectAttempts
					? "This was the last attempt at re-establishing websocket connection."
					: $"Attempt #{_reconnectAttempts}. Next retry in {_interval.TotalSeconds} seconds.");

			await Task.Delay(_interval).ContinueWith(_ => ConnectAsync()).ConfigureAwait(false);
		}

		private async Task ReceiveAsync(CancellationToken cancellationToken) {
			try {
				while (!cancellationToken.IsCancellationRequested) {
					byte[] bytes;
					using (var stream = new MemoryStream()) {
						var buffer = new byte[_config.BufferSize.Value];
						var segment = new ArraySegment<byte>(buffer);
						while (_clientWebSocket.State == WebSocketState.Open) {
							var result = await _clientWebSocket.ReceiveAsync(segment, cancellationToken)
							   .ConfigureAwait(false);
							if (result.MessageType == WebSocketMessageType.Close) {
								if (result.CloseStatus == WebSocketCloseStatus.EndpointUnavailable) {
									_isUseable = false;
									await RetryConnectionAsync().ConfigureAwait(false);
									break;
								}
							}

							stream.Write(buffer, 0, result.Count);
							if (result.EndOfMessage) {
								break;
							}
						}

						bytes = stream.ToArray();
					}

					if (bytes.Length <= 0) {
						continue;
					}

					var parse = _encoding.GetString(bytes).Trim('\0');
					OnMessage(parse);
				}
			}
			catch (Exception ex) when (ex.HResult == -2147467259) {
				_isUseable = false;
				await OnClosed.Invoke();
				await RetryConnectionAsync().ConfigureAwait(false);
			}
		}
	}
}