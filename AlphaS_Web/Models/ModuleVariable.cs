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
    public class ModuleVariable
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }

        [BsonElement("Variable_Name")]
        [JsonPropertyName("Variable_Name")]
        [DisplayName("Название переменной")]
        public string VariableName { get; set; }

        [BsonElement("Variable_DefaultValue")]
        [JsonPropertyName("Variable_DefaultValue")]
        [DisplayName("Значение по умолчанию")]
        public string VariableDefaultValue { get; set; }

        [BsonElement("Variable_Description")]
        [JsonPropertyName("Variable_Description")]
        [DisplayName("Описание переменной")]
        public string VariableDescription { get; set; }

        public ModuleVariable()
        {
        }
    }
}
