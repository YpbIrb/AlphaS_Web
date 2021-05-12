using AlphaS_Web.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Contexts
{
    public class ExperimentPresetContext
    {
        private readonly IMongoCollection<ExperimentPreset> _presets;
        private readonly CounterContext counterContext;

        public ExperimentPresetContext(IAlphaSDatabaseSettings settings, CounterContext _counterContext)
        {
            //Console.WriteLine("In Participant Service Create");
            counterContext = _counterContext;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _presets = database.GetCollection<ExperimentPreset>("ExperimentPresets");

            //Unique constraint for PresetName
            var option = new CreateIndexOptions() { Unique = true };
            var field = new StringFieldDefinition<ExperimentPreset>("PresetName");
            var indexDefinition = new IndexKeysDefinitionBuilder<ExperimentPreset>().Ascending(field);
            var indexModel = new CreateIndexModel<ExperimentPreset>(indexDefinition, option);
            _presets.Indexes.CreateOne(indexModel);


        }

        public ExperimentPreset Create(ExperimentPreset preset)
        {

            Console.WriteLine("Inserting new Preset. Name: " + preset.PresetName);

            _presets.InsertOne(preset);
            return preset;
        }

        public List<ExperimentPreset> GetAll()
        {
            //Console.WriteLine("In Participant Get");
            //var docCount = _participants.CountDocuments(part => true);
            //Console.WriteLine(docCount);
            return _presets.Find(presets => true).ToList();

        }

        public ExperimentPreset Find(string name) =>
            _presets.Find(preset => preset.PresetName == name).SingleOrDefault();

        public void Delete(string id) =>
            _presets.DeleteOne(preset => preset.PresetName == id);

    }
}
