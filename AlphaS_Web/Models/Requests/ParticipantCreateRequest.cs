using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Requests
{
    public class ParticipantCreateRequest
    {
        [JsonPropertyName("Birth_Date")]
        public DateTime Birth_Date { get; set; }

        [JsonPropertyName("Gender")]
        public string Gender { get; set; }

        [JsonPropertyName("Nationality")]
        public string Nationality { get; set; }

        [JsonPropertyName("Additional_Info")]
        public string AdditionalInfo { get; set; }

        public ParticipantCreateRequest(DateTime birth_Date, string gender, string nationality, string additionalInfo)
        {
            Birth_Date = birth_Date;
            Gender = gender;
            Nationality = nationality;
            AdditionalInfo = additionalInfo;
        }

        public ParticipantCreateRequest()
        {
        }
    }
}
