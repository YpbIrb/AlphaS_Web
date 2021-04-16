using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Models
{
    public class Counter
    {
        [BsonElement("id")]
        public string id { get; set; }

        [BsonElement("sequence_value")]
        public int sequence_value { get; set; }

        public Counter(string id, int sequence_value)
        {
            this.id = id;
            this.sequence_value = sequence_value;
        }

        public Counter()
        {
        }
    }
}
