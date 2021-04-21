using AlphaS_Web.Models.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("Id модуля")]
        public int ModuleId { get; set; }

        [BsonElement("Module_Name")]
        [JsonPropertyName("Module_Name")]
        [Required]
        [DisplayName("Название модуля")]
        public string ModuleName { get; set; }

        [BsonElement("Module_Type_Name")]
        [JsonPropertyName("Module_Type_Name")]
        [DisplayName("Тип модуля")]
        public string ModuleTypeName { get; set; }

        [BsonElement("Input_Variables")]
        [JsonPropertyName("Input_Variables")]
        [DisplayName("Входные переменные")]
        public List<ModuleVariable> InputVariables { get; set; }

        [BsonElement("Output_Variables")]
        [JsonPropertyName("Output_Variables")]
        [DisplayName("Выходные переменные")]
        public List<ModuleVariable> OutputVariables { get; set; }


        [BsonElement("Path_To_Exe")]
        [JsonPropertyName("Path_To_Exe")]
        [DisplayName("Название исполняемого файла")]
        [Required]
        public string PathToExe { get; set; }

        [BsonElement("Description")]
        [JsonPropertyName("Description")]
        [DisplayName("Описание модуля")]
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
