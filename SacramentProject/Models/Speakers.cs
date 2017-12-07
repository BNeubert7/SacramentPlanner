using System;
using System.Collections.Generic;

namespace SacramentProject.Models
{
    public partial class Speakers
    {
        public int SpeakerProgramId { get; set; }
        public int SacramentProgramId { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }

        public SacramentProgram SacramentProgram { get; set; }
    }
}