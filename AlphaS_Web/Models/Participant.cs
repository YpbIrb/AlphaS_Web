using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AlphaS_Web.Models
{

    public class Participant
    {
        [BsonId]
        [JsonIgnore]
        public ObjectId _id { get; set; }

        [BsonElement("Participant_Id")]
        [JsonPropertyName("Participant_Id")]
        [DisplayName("Id испытуемого")]
        public int ParticipantId { get; set; }

        [JsonPropertyName("Birth_Date")]
        [BsonElement("Birth_Date")]
        [Required(ErrorMessage = "Birth_Date is required.")]
        [DataType(DataType.Date)]
        [DisplayName("Дата рождения")]
        public DateTime Birth_Date { get; set; }

        [BsonElement("Gender")]
        [JsonPropertyName("Gender")]
        [Required(ErrorMessage = "Gender is required.")]
        [DisplayName("Пол")]
        public string Gender { get; set; }

        [BsonElement("Nationality")]
        [JsonPropertyName("Nationality")]
        [Required(ErrorMessage = "Nationality is required.")]
        [DisplayName("Национальность")]
        public string Nationality { get; set; }

        [BsonElement("Additional_Info")]
        [JsonPropertyName("Additional_Info")]
        [DisplayName("Дополнительная информация")]
        public string AdditionalInfo { get; set; }



        public Participant(Participant _participant)
        {
            ParticipantId = _participant.ParticipantId;
            Birth_Date = _participant.Birth_Date;
            Gender = _participant.Gender;
            Nationality = _participant.Nationality;
            AdditionalInfo = _participant.AdditionalInfo;
        }

        public Participant(int id, DateTime birth_Date, string gender, string nationality, string addtitionalInfo)
        {
            ParticipantId = id;
            Birth_Date = birth_Date;
            Gender = gender;
            Nationality = nationality;
            AdditionalInfo = addtitionalInfo;
        }
    }
}
