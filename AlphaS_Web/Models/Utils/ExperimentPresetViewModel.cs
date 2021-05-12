using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Utils
{
    public class ExperimentPresetViewModel
    {

        [DisplayName("Название пресета")]
        public string PresetName { get; set; }

        [DisplayName("Модули")]
        public List<ModuleInExperimentViewModel> Modules { get; set; }

        public ExperimentPresetViewModel()
        {
            Modules = new List<ModuleInExperimentViewModel>();
        }
    }
}
