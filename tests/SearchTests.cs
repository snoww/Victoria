using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Victoria.Interfaces;
using Victoria.Responses.Search;

namespace Victoria.Tests {
    [TestClass]
    public class SearchTests {
        private readonly ILavaNode _lavaNode
            = new AbstractLavaNode(new NodeConfiguration());

        [DataTestMethod]
        [DataRow("The Weeknd Valerie")]
        [DataRow("Logic OCD")]
        public async Task SearchYouTubeAsync(string query) {
            var searchResponse = await _lavaNode.SearchAsync(SearchType.YouTube, query);
            
            Assert.IsNotNull(searchResponse);
            Assert.IsNotNull(searchResponse.Exception, "searchResponse.Exception != null");
            Assert.IsNotNull(searchResponse.Playlist, "searchResponse.Playlist != null");
            Assert.IsNotNull(searchResponse.Tracks, "searchResponse.Tracks != null");
            Assert.IsNotNull(searchResponse.Status, "searchResponse.Status != null");
        }
    }
}