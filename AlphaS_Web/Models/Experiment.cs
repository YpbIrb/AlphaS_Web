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
    public class Experiment
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("Experiment_Id")]
        public int ExperimentId { get; set; }

        [BsonElement("Operator_Id")]
        public int OperatorId { get; set; }

        [BsonElement("First_Participant")]
        public ParticipantInExperiment FirstParticipant { get; set; }

        [BsonElement("Second_Participant")]
        public ParticipantInExperiment SecondParticipant { get; set; }

        [BsonElement("Modules")]
        public List<ModuleInExperiment> Modules { get; set; }

        [BsonElement("Start_Time")]
        public DateTime StartTime { get; set; }

        [BsonElement("Finish_Time")]
        public DateTime FinishTime { get; set; }


        public Experiment()
        {
            Modules = new List<ModuleInExperiment>();
        }

    }
}
