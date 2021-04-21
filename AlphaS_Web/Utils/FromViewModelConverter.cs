using AlphaS_Web.Contexts;
using AlphaS_Web.Models;
using AlphaS_Web.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Utils
{
    public static class FromViewModelConverter
    {

        public static ExperimentPreset PresetFromViewModel(ExperimentPresetViewModel presetViewModel, ModuleContext _modules)
        {
            ExperimentPreset res = new ExperimentPreset();
            res.PresetName = presetViewModel.PresetName;
            List<ModuleInExperiment> modules = new List<ModuleInExperiment>();
            foreach (ModuleInExperimentViewModel e in presetViewModel.Modules)
            {
                ModuleInExperiment new_module = ModuleinExperimentFromViewModel(e, _modules);
                modules.Add(new_module);
            }
            res.Modules = modules;
            return res;
        }


        public static Experiment ExperimentFromViewModel(ExperimentViewModel experimentViewModel, ModuleContext _modules)
        {
            Experiment res = new Experiment();
            res.OperatorId = experimentViewModel.OperatorId;
            List<ModuleInExperiment> modules = new List<ModuleInExperiment>();
            foreach (ModuleInExperimentViewModel e in experimentViewModel.Modules)
            {
                ModuleInExperiment new_module = ModuleinExperimentFromViewModel(e, _modules);
                modules.Add(new_module);
            }
            res.Modules = modules;
            return res;
        }


        public static ModuleInExperiment ModuleinExperimentFromViewModel(ModuleInExperimentViewModel moduleInExperimentViewModel, ModuleContext _modules)
        {
            ModuleInExperiment res = new ModuleInExperiment();
            res.ModuleName = moduleInExperimentViewModel.ModuleName;
            res.ModuleOrder = moduleInExperimentViewModel.ModuleOrder;
            Dictionary<string, string> inputValues = new Dictionary<string, string>();
            Dictionary<string, string> outputValues = new Dictionary<string, string>();
            foreach (MyKeyValuePair pair in moduleInExperimentViewModel.InputValues)
            {
                inputValues.Add(pair.Key, pair.Value);
            }

            List<ModuleVariable> outputVariables = _modules.Find(moduleInExperimentViewModel.ModuleName).OutputVariables;
            foreach (ModuleVariable e in outputVariables)
            {
                outputValues.Add(e.VariableName, e.VariableDefaultValue);
            }

            res.InputValues = inputValues;
            res.OutputValues = outputValues;
            return res;
        }





    }
}
