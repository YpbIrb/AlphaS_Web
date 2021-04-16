using AlphaS_Web.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Contexts
{
    public class ExperimentContext
    {
        private readonly IMongoCollection<Experiment> _experiments;
        private readonly CounterContext counterContext;

        public ExperimentContext(IAlphaSDatabaseSettings settings, CounterContext _counterContext)
        {
            //Console.WriteLine("In Participant Service Create");
            counterContext = _counterContext;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _experiments = database.GetCollection<Experiment>("Experiments");
        }


        public Experiment Create(Experiment experiment)
        {
            int new_experiment_id = counterContext.GetNextId("experiment");
            experiment.ExperimentId = new_experiment_id;

            Console.WriteLine("Inserting new Experiment. Id: " + new_experiment_id);

            _experiments.InsertOne(experiment);
            return experiment;
        }

        public List<Experiment> GetAll()
        {
            return _experiments.Find(module => true).ToList();

        }

        public Experiment Find(int id) =>
            _experiments.Find(exp => exp.ExperimentId == id).SingleOrDefault();


        public Experiment Update(int id, Experiment experiment)
        {
            Experiment OldExp = _experiments.Find(exp => exp.ExperimentId == id).SingleOrDefault();

            experiment._id = OldExp._id;
            _experiments.ReplaceOne(exp => exp.ExperimentId == id, experiment);
            return experiment;

        }

    }
}
