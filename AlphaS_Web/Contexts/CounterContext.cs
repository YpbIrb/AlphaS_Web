using AlphaS_Web.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Contexts
{
    public class CounterContext
    {
        private readonly IMongoCollection<Counter> _counters;

        public CounterContext(IAlphaSDatabaseSettings settings)
        {
            //Console.WriteLine("In Counter Context Create");
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _counters = database.GetCollection<Counter>("Counters");
        }

        public int GetNextId(string neaded_id)
        {
            Counter counter = _counters.Find(counter => counter.id == neaded_id).SingleOrDefault();

            if (counter == null)
            {
                _counters.InsertOne(new Counter(neaded_id, 2));
                return 1;
            }
            else
            {
                int res = counter.sequence_value;
                counter.sequence_value++;
                _counters.ReplaceOne(counter => counter.id == neaded_id, counter);
                return res;
            }

        }

    }
}
