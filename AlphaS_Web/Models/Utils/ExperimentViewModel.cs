using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Utils
{
    public class ExperimentViewModel
    {

        [DisplayName("Modules")]
        public List<ModuleInExperimentViewModel> Modules { get; set; }

        [DisplayName("Preset")]
        public string PresetName { get; set; }

        public ExperimentViewModel()
        {
            Modules = new List<ModuleInExperimentViewModel>();
        }
    }
}
