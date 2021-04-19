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
    public class ParticipantInExperiment 
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }


        [BsonElement("Participant_Id")]
        [JsonPropertyName("Participant_Id")]
        public int ParticipantId { get; set; }

        [BsonElement("Intoxication")]
        [JsonPropertyName("Intoxication")]
        public bool Intoxication { get; set; }

        [BsonElement("Head_Injury")]
        [JsonPropertyName("Head_Injury")]
        public bool HeadInjury { get; set; }

        [BsonElement("Periods")]
        [JsonPropertyName("Periods")]
        public bool Periods { get; set; }

        [BsonElement("Additional_Info")]
        [JsonPropertyName("Additional_Info")]
        public string AdditionalInfo { get; set; }
    }
}
