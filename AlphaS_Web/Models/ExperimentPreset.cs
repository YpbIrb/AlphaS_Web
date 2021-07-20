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
    public class ExperimentPreset
    {

        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }

        [BsonElement("Preset_Name")]
        [JsonPropertyName("Preset_Name")]
        [DisplayName("Название шаблона")]
        [Required]
        public string PresetName { get; set; }

        [BsonElement("Modules")]
        [JsonPropertyName("Modules")]
        [DisplayName("Модули в шаблоне")]
        public List<ModuleInExperiment> Modules { get; set; }
    }
}
