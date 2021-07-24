using System.Collections.Generic;

namespace Up.Models
{
    public class ExportResultInputModel
    {
        public IList<ChallengeResultInputModel> Results { get; set; }

        public int Score { get; set; }

        public bool IsPass { get; set; }

        public string TenHocVien { get; set; }

        public string Trigram { get; set; }

        public string ChallengeName { get; set; }

        public int LanThi { get; set; }
    }
}
