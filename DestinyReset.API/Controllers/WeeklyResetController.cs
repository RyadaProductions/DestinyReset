using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DestinyReset.ResetInfo;
using DestinyReset.ResetInfo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DestinyReset.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeeklyResetController : ControllerBase
    {
        private readonly IDownloader _downloader;

        private readonly ILogger<WeeklyResetController> _logger;

        public WeeklyResetController(ILogger<WeeklyResetController> logger)
        {
            _logger = logger;
            _downloader = new Downloader();
        }

        [HttpGet]
        public IEnumerable<WeeklyReset> GetAllResets()
        {
            return _downloader.WeeklyResets;
        }

        [HttpGet]
        public WeeklyReset GetCurrentReset()
        {
            return _downloader.CurrentReset;
        }
    }
}
