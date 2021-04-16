using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Utils
{
    public class ModuleInExperimentViewModel
    {
        public string ModuleName { get; set; }

        public int ModuleOrder { get; set; }

        public List<MyKeyValuePair> InputValues { get; set; }

        public List<MyKeyValuePair> OutputValues { get; set; }

        public ModuleInExperimentViewModel(string moduleName, int moduleOrder, List<MyKeyValuePair> inputValues, List<MyKeyValuePair> outputValues)
        {
            ModuleName = moduleName;
            ModuleOrder = moduleOrder;
            InputValues = inputValues;
            OutputValues = outputValues;
        }

        public ModuleInExperimentViewModel()
        {
            OutputValues = new List<MyKeyValuePair>();
            InputValues = new List<MyKeyValuePair>();
        }
    }
}
