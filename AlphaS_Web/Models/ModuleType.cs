using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models
{
    public class ModuleType
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("Module_Type")]
        public string ModuleTypeName;

        [BsonElement("Module_Type_Description")]
        public string ModuleTypeDescription;

        public ModuleType(string moduleType, string moduleTypeDescription)
        {
            this.ModuleTypeName = moduleType;
            this.ModuleTypeDescription = moduleTypeDescription;
        }

        public ModuleType()
        {
        }
    }
}
