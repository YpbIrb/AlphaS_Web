using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Utils
{
    public class ExperimentViewModel
    {
        public int OperatorId { get; set; }
        public List<ModuleInExperimentViewModel> Modules { get; set; }

        public ExperimentViewModel()
        {
            Modules = new List<ModuleInExperimentViewModel>();
        }
    }
}
