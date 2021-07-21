using AlphaS_Web.Models;
using AlphaS_Web.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaS_Web.Utils
{
    public static class CSVConverter
    {

        public static string GetCSVForPresetExperiments(ExperimentPreset preset, List<Experiment> experiments)
        {
            StringBuilder res_sb = new StringBuilder();
            res_sb.Append(GenerateHeader(preset));

            if(experiments != null)
            {
                foreach(Experiment experiment in experiments)
                {
                    res_sb.Append("\n");
                    res_sb.Append(experiment.ExperimentId + ";");
                    res_sb.Append(experiment.OperatorId + ";");

                    res_sb.Append(experiment.FirstParticipant.ParticipantId + ";");
                    res_sb.Append(experiment.FirstParticipant.Intoxication + ";");
                    res_sb.Append(experiment.FirstParticipant.HeadInjury + ";");
                    res_sb.Append(experiment.FirstParticipant.Periods + ";");

                    res_sb.Append(experiment.SecondParticipant.ParticipantId + ";");
                    res_sb.Append(experiment.SecondParticipant.Intoxication + ";");
                    res_sb.Append(experiment.SecondParticipant.HeadInjury + ";");
                    res_sb.Append(experiment.SecondParticipant.Periods + ";");

                    foreach (ModuleInExperiment moduleInExperiment in experiment.Modules)
                    {
                        foreach (KeyValuePair<string, string> pair in moduleInExperiment.InputValues)
                        {
                            res_sb.Append(pair.Value + ";");
                        }

                        foreach (KeyValuePair<string, string> pair in moduleInExperiment.OutputValues)
                        {
                            res_sb.Append(pair.Value + ";");
                        }
                    }
                    res_sb.Append(experiment.StartTime + ";");
                    res_sb.Append(experiment.FinishTime + "");
                }
            }
            return res_sb.ToString();
        }


        public static string GetCSVWithParticipantsForPresetExperiments(ExperimentPreset preset, List<ExperimentWithParticipantsInfo> experiments)
        {
            StringBuilder res_sb = new StringBuilder();
            res_sb.Append(GenerateHeaderWithParticipantInfo(preset));

            if (experiments != null)
            {
                foreach (ExperimentWithParticipantsInfo experiment in experiments)
                {
                    res_sb.Append("\n");
                    res_sb.Append(experiment.experiment.ExperimentId + ";");
                    res_sb.Append(experiment.experiment.OperatorId + ";");

                    res_sb.Append(experiment.experiment.FirstParticipant.ParticipantId + ";");
                    if(experiment.firstParticipant != null)
                    {
                        res_sb.Append(experiment.firstParticipant.Gender + ";");
                        res_sb.Append(experiment.firstParticipant.Nationality + ";");
                        res_sb.Append(experiment.firstParticipant.Birth_Date + ";");
                    }
                    else
                    {
                        res_sb.Append(';');
                        res_sb.Append(';');
                        res_sb.Append(';');
                    }

                    res_sb.Append(experiment.experiment.FirstParticipant.Intoxication + ";");
                    res_sb.Append(experiment.experiment.FirstParticipant.HeadInjury + ";");
                    res_sb.Append(experiment.experiment.FirstParticipant.Periods + ";");

                    res_sb.Append(experiment.experiment.SecondParticipant.ParticipantId + ";");
                    if (experiment.secondParticipant != null)
                    {
                        res_sb.Append(experiment.secondParticipant.Gender + ";");
                        res_sb.Append(experiment.secondParticipant.Nationality + ";");
                        res_sb.Append(experiment.secondParticipant.Birth_Date + ";");
                    }
                    else
                    {
                        res_sb.Append(';');
                        res_sb.Append(';');
                        res_sb.Append(';');
                    }
                    res_sb.Append(experiment.experiment.SecondParticipant.Intoxication + ";");
                    res_sb.Append(experiment.experiment.SecondParticipant.HeadInjury + ";");
                    res_sb.Append(experiment.experiment.SecondParticipant.Periods + ";");

                    foreach (ModuleInExperiment moduleInExperiment in experiment.experiment.Modules)
                    {
                        foreach (KeyValuePair<string, string> pair in moduleInExperiment.InputValues)
                        {
                            res_sb.Append(pair.Value + ";");
                        }

                        foreach (KeyValuePair<string, string> pair in moduleInExperiment.OutputValues)
                        {
                            res_sb.Append(pair.Value + ";");
                        }
                    }
                    res_sb.Append(experiment.experiment.StartTime + ";");
                    res_sb.Append(experiment.experiment.FinishTime + "");
                }
            }
            return res_sb.ToString();
        }


        private static string GenerateHeader(ExperimentPreset preset)
        {
            StringBuilder header_sb = new StringBuilder();


            header_sb.Append("Experiment_Id;");
            header_sb.Append("Operator_Id;");

            header_sb.Append("First_Participant_Id;");
            header_sb.Append("First_Participant_Intoxication;");
            header_sb.Append("First_Participant_HeadInjury;");
            header_sb.Append("First_Participant_Periods;");

            header_sb.Append("Second_Participant_Id;");
            header_sb.Append("Second_Participant_Intoxication;");
            header_sb.Append("Second_Participant_HeadInjury;");
            header_sb.Append("Second_Participant_Periods;");

            foreach(ModuleInExperiment moduleInExperiment in preset.Modules)
            {
                foreach(KeyValuePair<string, string> pair in moduleInExperiment.InputValues)
                {
                    header_sb.Append(moduleInExperiment.ModuleOrder + "_" + pair.Key + ";");
                }

                foreach (KeyValuePair<string, string> pair in moduleInExperiment.OutputValues)
                {
                    header_sb.Append(moduleInExperiment.ModuleOrder + "_" + pair.Key + ";");
                }
            }

            header_sb.Append("Start_Time;");
            header_sb.Append("Finish_Time");

            return header_sb.ToString();
        }


        private static string GenerateHeaderWithParticipantInfo(ExperimentPreset preset)
        {
            StringBuilder header_sb = new StringBuilder();


            header_sb.Append("Experiment_Id;");
            header_sb.Append("Operator_Id;");

            header_sb.Append("First_Participant_Id;");
            header_sb.Append("First_Participant_Gender;");
            header_sb.Append("First_Participant_Nationality;");
            header_sb.Append("First_Participant_BirthDate;");
            header_sb.Append("First_Participant_Intoxication;");
            header_sb.Append("First_Participant_HeadInjury;");
            header_sb.Append("First_Participant_Periods;");

            header_sb.Append("Second_Participant_Id;");
            header_sb.Append("Second_Participant_Gender;");
            header_sb.Append("Second_Participant_Nationality;");
            header_sb.Append("Second_Participant_BirthDate;");
            header_sb.Append("Second_Participant_Intoxication;");
            header_sb.Append("Second_Participant_HeadInjury;");
            header_sb.Append("Second_Participant_Periods;");

            foreach (ModuleInExperiment moduleInExperiment in preset.Modules)
            {
                foreach (KeyValuePair<string, string> pair in moduleInExperiment.InputValues)
                {
                    header_sb.Append(moduleInExperiment.ModuleOrder + "_" + pair.Key + ";");
                }

                foreach (KeyValuePair<string, string> pair in moduleInExperiment.OutputValues)
                {
                    header_sb.Append(moduleInExperiment.ModuleOrder + "_" + pair.Key + ";");
                }
            }

            header_sb.Append("Start_Time;");
            header_sb.Append("Finish_Time");

            return header_sb.ToString();
        }



    }
}
