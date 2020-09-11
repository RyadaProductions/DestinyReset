using System;

namespace DestinyReset.ResetInfo.Models
{
    public class DailyReset
    {
        public DateTime Reset { get; set; }

        public string Day { get; set; }

        public string SolsticeSinge { get; set; }

        public VanguardMenagerie VanguardMenagerie { get; set; }

        public Reckoning Reckoning { get; set; }

        public string Forge { get; set; }

        public string AlterWeapon { get; set; }
    }
}
