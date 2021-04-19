using AlphaS_Web.Models.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlphaS_Web.Models
{
    public class Module
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }

        [BsonElement("Module_Id")]
        [JsonPropertyName("Module_Id")]
        public int ModuleId { get; set; }

        [BsonElement("Module_Name")]
        [JsonPropertyName("Module_Name")]
        public string ModuleName { get; set; }

        [BsonElement("Module_Type_Name")]
        [JsonPropertyName("Module_Type_Name")]
        public string ModuleTypeName { get; set; }

        [BsonElement("Input_Variables")]
        [JsonPropertyName("Input_Variables")]
        public List<ModuleVariable> InputVariables { get; set; }

        [BsonElement("Output_Variables")]
        [JsonPropertyName("Output_Variables")]
        public List<ModuleVariable> OutputVariables { get; set; }


        [BsonElement("Path_To_Exe")]
        [JsonPropertyName("Path_To_Exe")]
        public string PathToExe { get; set; }

        [BsonElement("Description")]
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        public Module( int moduleId, string moduleName, string moduleTypeName, string pathToExe, string description)
        {
            ModuleId = moduleId;
            ModuleName = moduleName;
            ModuleTypeName = moduleTypeName;
            PathToExe = pathToExe;
            Description = description;
            InputVariables = new List<ModuleVariable>();
            OutputVariables = new List<ModuleVariable>();
        }

        public Module()
        {
            InputVariables = new List<ModuleVariable>();
            OutputVariables = new List<ModuleVariable>();
        }
    }
}
