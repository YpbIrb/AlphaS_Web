using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Utils
{
    public class ExperimentWithParticipantsInfo
    {
        public Experiment experiment { get; set; }
        public Participant firstParticipant { get; set; }
        public Participant secondParticipant { get; set; }

        public ExperimentWithParticipantsInfo(Experiment experiment, Participant firstParticipant, Participant secondParticipant)
        {
            this.experiment = experiment;
            this.firstParticipant = firstParticipant;
            this.secondParticipant = secondParticipant;
        }

        public ExperimentWithParticipantsInfo()
        {
        }
    }
}
