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
    public class Experiment
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }

        [BsonElement("Experiment_Id")]
        [JsonPropertyName("Experiment_Id")]
        [DisplayName("Id эксперимента")]
        public int ExperimentId { get; set; }

        [BsonElement("Operator_Id")]
        [JsonPropertyName("Operator_Id")]
        [DisplayName("Id оператора")]
        public int OperatorId { get; set; }

        [BsonElement("First_Participant")]
        [JsonPropertyName("First_Participant")]
        [DisplayName("Первый участник")]
        public ParticipantInExperiment FirstParticipant { get; set; }

        [BsonElement("Second_Participant")]
        [JsonPropertyName("Second_Participant")]
        [DisplayName("Второй участник")]
        public ParticipantInExperiment SecondParticipant { get; set; }

        [BsonElement("Modules")]
        [JsonPropertyName("Modules")]
        [DisplayName("Модули")]
        public List<ModuleInExperiment> Modules { get; set; }

        [BsonElement("Start_Time")]
        [JsonPropertyName("Start_Time")]
        [DisplayName("Время начала")]
        public DateTime StartTime { get; set; }

        [BsonElement("Finish_Time")]
        [JsonPropertyName("Finish_Time")]
        [DisplayName("Время конца")]
        public DateTime FinishTime { get; set; }


        public Experiment()
        {
            Modules = new List<ModuleInExperiment>();
            FirstParticipant = new ParticipantInExperiment();
            SecondParticipant = new ParticipantInExperiment();
        }

    }
}
