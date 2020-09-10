using DestinyReset.ResetInfo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DestinyReset.ResetInfo
{
    public interface IDownloader
    {
        List<WeeklyReset> WeeklyResets { get; }
        List<DailyReset> DailyResets { get; }

        Task DownloadWeeklyAndDailyResetsAsync();
    }
}