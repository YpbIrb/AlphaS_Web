using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlphaS_Web.Models
{
    public class ModuleInExperiment
    {
        /*
        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }
        */

        [BsonElement("Module_Name")]
        [JsonPropertyName("Module_Name")]
        [DisplayName("Название модуля")]
        public string ModuleName { get; set; }

        [BsonElement("Module_Order")]
        [JsonPropertyName("Module_Order")]
        [DisplayName("Порядок модуля")]
        public int ModuleOrder { get; set; }

        [BsonElement("Start_Time")]
        [JsonPropertyName("Start_Time")]
        [DisplayName("Время начала")]
        public DateTime StartTime { get; set; }

        [BsonElement("Finish_Time")]
        [JsonPropertyName("Finish_Time")]
        [DisplayName("Время конца")]
        public DateTime FinishTime { get; set; }

        [BsonElement("Output_Values")]
        [JsonPropertyName("Output_Values")]
        [DisplayName("Выходные значения")]
        public Dictionary<string, string> OutputValues { get; set; }

        [BsonElement("Input_Values")]
        [JsonPropertyName("Input_Values")]
        [DisplayName("Входные значения")]
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
