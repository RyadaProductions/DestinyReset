using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using DestinyReset.ResetInfo;
using DestinyReset.ResetInfo.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DestinyReset.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDownloader _downloader;

        public WeeklyReset CurrentReset => _downloader.CurrentReset;

        public IndexModel(ILogger<IndexModel> logger, IDownloader downloader)
        {
            _logger = logger;
            _downloader = downloader;
        }

        public async Task OnGetAsync()
        {
            await _downloader.StartAutomaticDownloaderAsync();
        }
    }
}
