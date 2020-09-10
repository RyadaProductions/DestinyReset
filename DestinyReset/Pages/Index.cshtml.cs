using System;
using System.Linq;
using System.Threading.Tasks;
using DestinyReset.ResetInfo;
using DestinyReset.ResetInfo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DestinyReset.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDownloader Downloader;

        public WeeklyReset CurrentReset { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IDownloader downloader)
        {
            _logger = logger;
            Downloader = downloader;
        }

        public async Task OnGetAsync()
        {
            if (Downloader.WeeklyResets.Count != 0)
                return;

            _logger.LogInformation("Downloading reset information");
            await Downloader.DownloadWeeklyAndDailyResetsAsync();

            var currentResetDay = CalculateCurrentReset(DateTime.Today);

            CurrentReset = Downloader.WeeklyResets.First(x => x.Reset.Date == currentResetDay.Date);
            _logger.LogInformation($"Current reset is: {CurrentReset.Reset.Date}");
        }

        private DateTime CalculateCurrentReset(DateTime currentDay)
        {
            return currentDay.AddDays(-(int)currentDay.AddDays(-(int)DayOfWeek.Tuesday).DayOfWeek);
        }
    }
}
