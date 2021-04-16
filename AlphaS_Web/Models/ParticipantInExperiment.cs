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
    public class ParticipantInExperiment 
    {
        [BsonId]
        public ObjectId _id { get; set; }


        [BsonElement("Participant_Id")]
        public int ParticipantId { get; set; }

        [BsonElement("Intoxication")]
        public bool Intoxication { get; set; }

        [BsonElement("Head_Injury")]
        public bool HeadInjury { get; set; }

        [BsonElement("Periods")]
        public bool Periods { get; set; }

        [BsonElement("Additional_Info")]
        public string AdditionalInfo { get; set; }
    }
}
