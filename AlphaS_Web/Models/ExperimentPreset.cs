using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
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
        public string PresetName { get; set; }

        [BsonElement("Modules")]
        [JsonPropertyName("Modules")]
        public List<ModuleInExperiment> Modules { get; set; }
    }
}
