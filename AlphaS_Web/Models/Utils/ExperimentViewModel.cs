using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Utils
{
    public class ExperimentViewModel
    {
        [DisplayName("Id ���������")]
        public int OperatorId { get; set; }

        [DisplayName("������")]
        public List<ModuleInExperimentViewModel> Modules { get; set; }

        public ExperimentViewModel()
        {
            Modules = new List<ModuleInExperimentViewModel>();
        }
    }
}
