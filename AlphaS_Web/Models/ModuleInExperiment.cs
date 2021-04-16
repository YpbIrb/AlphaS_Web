using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models
{
    public class ModuleInExperiment
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("Module_Name")]
        public string ModuleName { get; set; }

        [BsonElement("Module_Order")]
        public int ModuleOrder { get; set; }

        [BsonElement("Start_Time")]
        public DateTime StartTime { get; set; }

        [BsonElement("Finish_Time")]
        public DateTime FinishTime { get; set; }

        [BsonElement("Output_Values")]
        public Dictionary<string, string> OutputValues { get; set; }

        [BsonElement("Input_Values")]
        public Dictionary<string, string> InputValues { get; set; }


        public ModuleInExperiment()
        {
            OutputValues = new Dictionary<string, string>();
            InputValues = new Dictionary<string, string>();
        }

        public ModuleInExperiment(int moduleOrder)
        {
            OutputValues = new Dictionary<string, string>();
            InputValues = new Dictionary<string, string>();
            ModuleOrder = moduleOrder;
        }

        public ModuleInExperiment(string moduleName, int moduleOrder, 
                                 Dictionary<string, string> outputValues, Dictionary<string, string> inputValues)
        {
            ModuleName = moduleName;
            ModuleOrder = moduleOrder;
            OutputValues = outputValues;
            InputValues = inputValues;
        }
    }
}
