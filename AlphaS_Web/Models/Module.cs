using AlphaS_Web.Models.Utils;
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
    public class Module
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("Module_Id")]
        public int ModuleId { get; set; }

        [BsonElement("Module_Name")]
        public string ModuleName { get; set; }

        [BsonElement("Module_Type_Name")]
        public string ModuleTypeName { get; set; }

        [BsonElement("Input_Variables")]
        public Dictionary<string, string> InputVariables { get; set; }

        [BsonElement("Output_Variables")]
        public Dictionary<string, string> OutputVariables { get; set; }


        [BsonElement("Path_To_Exe")]
        public string PathToExe { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        public Module( int moduleId, string moduleName, string moduleTypeName, string pathToExe, string description)
        {
            ModuleId = moduleId;
            ModuleName = moduleName;
            ModuleTypeName = moduleTypeName;
            PathToExe = pathToExe;
            Description = description;
            InputVariables = new Dictionary<string, string>();
            OutputVariables = new Dictionary<string, string>();
        }





    }
}
