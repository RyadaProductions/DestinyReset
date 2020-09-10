using DestinyReset.ResetInfo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DestinyReset.ResetInfo
{
    public class Downloader : IDownloader
    {
        private const string _downloadUrl = @"https://docs.google.com/spreadsheets/d/e/2PACX-1vSOSn3UC6NoaI8N0L1Ysh3L5_HZa5aPgnO6KBhKFEE5IWofAlVmpkVSFiJsOOHUJGwDH7Z8pYO_NADT/pub?output=csv&gid=";
        private const string _weeklyGid = "968294774";
        private const string _dailyGid = "0";

        public List<WeeklyReset> WeeklyResets { get; } = new List<WeeklyReset>();
        public List<DailyReset> DailyResets { get; } = new List<DailyReset>();

        public async Task DownloadWeeklyAndDailyResetsAsync()
        {
            WeeklyResets.Clear();
            await DownloadResetsAsync("weekly", _weeklyGid, WeeklyResets, RowToWeeklyReset);
            DailyResets.Clear();
            await DownloadResetsAsync("daily", _dailyGid, DailyResets, RowToDailyReset);
        }

        private async Task DownloadResetsAsync<T>(string suffix, string gid, List<T> list, Func<string[], T> parseMethod)
        {
            var request = WebRequest.Create(_downloadUrl + gid);
            request.Credentials = CredentialCache.DefaultCredentials;

            var response = await request.GetResponseAsync();

            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);

            var responseString = await reader.ReadToEndAsync();

            ParseValuesToResets(responseString, list, parseMethod);
        }

        private void ParseValuesToResets<T>(string csvTable, List<T> list, Func<string[], T> parseMethod)
        {
            var table = csvTable.Split(Environment.NewLine);

            // Skip the header
            var dataRows = table.Skip(2);

            foreach (var line in dataRows)
            {
                var values = line.Split(',');

                // FirstColumn (DateTime) is only empty on empty rows, so we can safely skip them.
                if (values[0] == string.Empty) continue;

                list.Add(parseMethod(values));
            }
        }

        private WeeklyReset RowToWeeklyReset(string[] values)
        {
            for (var i = 0; i < values.Length; i++)
                if (string.IsNullOrWhiteSpace(values[i]))
                    values[i] = "Unknown";

            return new WeeklyReset()
            {
                Reset = DateTime.Parse(values[0]),
                Flashpoint = values[1],
                ContactFlashpoint = values[2],
                ContactHeroicBoss = values[3],
                Singe = new Singe()
                {
                    VanguardAndMenagerie = values[5],
                    Reckoning = values[6],
                    ZeroHourAndWhisper = values[7]
                },
                Raids = new Raids()
                {
                    GoSChallenge = values[9],
                    LastWishChallenge = values[10],
                    ScourgeChallenge = values[11],
                    CrownChallenge = values[12],
                    LeviathanChallenge = values[13],
                    PrestigeModifier = values[14]
                },
                MenagerieBoss = values[16],
                ReckoningBoss = values[18],
                EscalationProtocol = new EscalationProtocol()
                {
                    Weapons = values[20],
                    Boss = values[21]
                },
                Nightmares = new Nightmares()
                {
                    Boss1 = values[23],
                    Boss2 = values[24],
                    Boss3 = values[25],
                    Roaming = values[26]
                },
                DreamingCity = new DreamingCity()
                {
                    CurseWeek = values[28],
                    AscendantChallenge = values[29],
                    ChallengeLocation = values[30]
                }
            };
        }

        private DailyReset RowToDailyReset(string[] values)
        {
            return new DailyReset()
            {
                Reset = DateTime.Parse(values[0]),
                Day = values[1],
                SolsticeSinge = values[3],
                VanguardMenagerie = new VanguardMenagerie()
                {
                    Singe = values[4],
                    Pro = values[5],
                    Con = values[6],
                },
                Reckoning = new Reckoning()
                {
                    Singe = values[8],
                    Pro = values[9],
                },
                Forge = values[11],
                AlterWeapon = values[13]
            };
        }

        private StreamWriter CreateFile(string id)
        {
            var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var fullFileName = Path.Combine(desktopFolder, $"Resets_{id}.json");

            return new StreamWriter(fullFileName);
        }
    }
}
