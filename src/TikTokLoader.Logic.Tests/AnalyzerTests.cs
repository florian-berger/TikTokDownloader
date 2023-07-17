namespace TikTokLoader.Logic.Tests
{
    [TestClass]
    public class AnalyzerTests
    {
        [TestMethod]
        public async Task ThrowsUrlNotDefinedException()
        {
            try
            {
                await Analyzer.AnalyzeUri("");
            }
            catch (System.Exception ex)
            {
                Assert.IsTrue(ex is DownloaderException { Code: DownloaderExceptionCodes.NoUriDefined }, "Correct exception code not thrown!");
            }
        }

        [TestMethod]
        public async Task ThrowsInvalidUriException()
        {
            try
            {
                await Analyzer.AnalyzeUri("https://github.com/florian-berger/TikTokDownloader");
            }
            catch (System.Exception ex)
            {
                Assert.IsTrue(ex is DownloaderException { Code: DownloaderExceptionCodes.InvalidUri }, "Correct exception code not thrown!");
            }
        }

        [TestMethod]
        public async Task VideoIdFromFullUriIsCorrect()
        {
            const string expectedId = "7109835066808831238";
            const string uriToCheck = "https://www.tiktok.com/@tobinatorclips/video/7109835066808831238";

            var foundId = await Analyzer.GetVideoId(uriToCheck);
            Assert.AreEqual(expectedId, foundId, "Ids are not matching!");
        }

        [TestMethod]
        public async Task VideoIdFromShareUriIsCorrect()
        {
            const string expectedId = "7109835066808831238";
            const string uriToCheck = "https://vm.tiktok.com/ZGJVd91ox";

            var foundId = await Analyzer.GetVideoId(uriToCheck);
            Assert.AreEqual(expectedId, foundId, "Ids are not matching!");
        }

        [TestMethod]
        public async Task AuthorFromShareUriIsCorrect()
        {
            const string expectedAuthor = "TobinatorClips";
            const string uriToCheck = "https://vm.tiktok.com/ZGJVd91ox";

            var data = await Analyzer.AnalyzeUri(uriToCheck);
            Assert.IsNotNull(data);
            Assert.AreEqual(data.UploadUser, expectedAuthor, true, "The author information does not match!");
        }
    }
}