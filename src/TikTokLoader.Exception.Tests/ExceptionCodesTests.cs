namespace TikTokLoader.Exception.Tests
{
    [TestClass]
    public class ExceptionCodesTests
    {
        [TestMethod]
        public void EnsureUniqueExceptionCodes()
        {
            var enumsList = Enum.GetValues<DownloaderExceptionCodes>().ToList();
            if (!enumsList.Any())
            {
                Assert.Fail("Unable to find any exception codes.");
            }

            var groupedIntCode = enumsList.Select(code => (int) code).GroupBy(code => code);
            Assert.IsTrue(groupedIntCode.All(i => i.Count() < 2), "At least one int exception code is not unique!");
        }

        [TestMethod]
        public void CheckIfCodeIsPresentInException()
        {
            try
            {
                throw new DownloaderException(DownloaderExceptionCodes.NoUriDefined);
            }
            catch (DownloaderException ex)
            {
                Assert.IsTrue(ex.Code == DownloaderExceptionCodes.NoUriDefined, "ExceptionCode is not correct in caught exception!");
            }
        }
    }
}