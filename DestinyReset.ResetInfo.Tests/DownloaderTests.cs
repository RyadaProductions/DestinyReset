using NUnit.Framework;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace DestinyReset.ResetInfo.Tests
{
    public class DownloaderTests
    {
        private Downloader _info;

        [SetUp]
        public void Setup()
        {
            // Check how to unit test with the logging abstraction from .NET
            //_info = new Downloader();
        }

        [Test]
        public async Task DoesDownloadWeeklyAndDailyResetsAsyncCompleteWithoutException()
        {
            await _info.DownloadWeeklyAndDailyResetsAsync();

            Assert.IsTrue(_info.WeeklyResets.Count != 0);
            Assert.IsTrue(_info.DailyResets.Count != 0);
        }
    }
}