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
    public class ParticipantInExperiment 
    {
        /*
        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }
        */

        [BsonElement("Participant_Id")]
        [JsonPropertyName("Participant_Id")]
        [DisplayName("Id испытуемого")]
        public int ParticipantId { get; set; }

        [BsonElement("Intoxication")]
        [JsonPropertyName("Intoxication")]
        [DisplayName("Состояние опьянения")]
        public bool Intoxication { get; set; }

        [BsonElement("Head_Injury")]
        [JsonPropertyName("Head_Injury")]
        [DisplayName("Травмы головы")]
        public bool HeadInjury { get; set; }

        [BsonElement("Periods")]
        [JsonPropertyName("Periods")]
        [DisplayName("Фаза менструации")]
        public bool Periods { get; set; }

        [BsonElement("Additional_Info")]
        [JsonPropertyName("Additional_Info")]
        [DisplayName("Дополнительная информация")]
        public string AdditionalInfo { get; set; }
    }
}
