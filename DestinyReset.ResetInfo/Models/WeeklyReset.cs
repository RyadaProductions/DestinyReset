using System;

namespace DestinyReset.ResetInfo.Models
{
    public class WeeklyReset
    {
        public DateTime Reset { get; set; }

        public string Flashpoint { get; set; }

        public string ContactFlashpoint { get; set; }

        public string ContactHeroicBoss { get; set; }

        public Singe Singe { get; set; }

        public Raids Raids { get; set; }

        public string MenagerieBoss { get; set; }

        public string ReckoningBoss { get; set; }

        public EscalationProtocol EscalationProtocol { get; set; }

        public Nightmares Nightmares { get; set; }

        public DreamingCity DreamingCity { get; set; }
    }

}
