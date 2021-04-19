using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlphaS_Web.Models
{
    public class ModuleVariable
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }

        [BsonElement("Variable_Name")]
        [JsonPropertyName("Variable_Name")]
        public string VariableName { get; set; }

        [BsonElement("Variable_DefaultValue")]
        [JsonPropertyName("Variable_DefaultValue")]
        public string VariableDefaultValue { get; set; }

        [BsonElement("Variable_Description")]
        [JsonPropertyName("Variable_Description")]
        public string VariableDescription { get; set; }

        public ModuleVariable()
        {
        }
    }
}
