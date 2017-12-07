using System;
using System.Collections.Generic;

namespace SacramentProject.Models
{
    public partial class SacramentProgram
    {
        public SacramentProgram()
        {
            Speakers = new HashSet<Speakers>();
        }

        public int SacramentProgramId { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Conducting { get; set; }
        public int OpeningSong { get; set; }
        public string OpeningPrayer { get; set; }
        public int SacramentSong { get; set; }
        public int? IntermediateSong { get; set; }
        public int ClosingSong { get; set; }
        public string ClosingPrayer { get; set; }

        public ICollection<Speakers> Speakers { get; set; }
    }
}
