using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models.Requests
{
    public class ParticipantCreateRequest
    {
        public DateTime Birth_Date { get; set; }

        public string Gender { get; set; }

        public string Nationality { get; set; }

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
