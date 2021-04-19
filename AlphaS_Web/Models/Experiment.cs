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
    public class Experiment
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }

        [BsonElement("Experiment_Id")]
        [JsonPropertyName("Experiment_Id")]
        public int ExperimentId { get; set; }

        [BsonElement("Operator_Id")]
        [JsonPropertyName("Operator_Id")]
        public int OperatorId { get; set; }

        [BsonElement("First_Participant")]
        [JsonPropertyName("First_Participant")]
        public ParticipantInExperiment FirstParticipant { get; set; }

        [BsonElement("Second_Participant")]
        [JsonPropertyName("Second_Participant")]
        public ParticipantInExperiment SecondParticipant { get; set; }

        [BsonElement("Modules")]
        [JsonPropertyName("Modules")]
        public List<ModuleInExperiment> Modules { get; set; }

        [BsonElement("Start_Time")]
        [JsonPropertyName("Start_Time")]
        public DateTime StartTime { get; set; }

        [BsonElement("Finish_Time")]
        [JsonPropertyName("Finish_Time")]
        public DateTime FinishTime { get; set; }


        public Experiment()
        {
            Modules = new List<ModuleInExperiment>();
        }

    }
}
