using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Utils
{
    public class ExperimentPresetViewModel
    {
        
        public string PresetName { get; set; }

        public List<ModuleInExperimentViewModel> Modules { get; set; }

        public ExperimentPresetViewModel()
        {
            Modules = new List<ModuleInExperimentViewModel>();
        }
    }
}
