using AlphaS_Web.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Contexts
{
    public class ModuleContext
    {
        private readonly IMongoCollection<Module> _modules;
        private readonly CounterContext counterContext;

        
        public ModuleContext(IAlphaSDatabaseSettings settings, CounterContext _counterContext)
        {
            //Console.WriteLine("In Participant Service Create");
            counterContext = _counterContext;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _modules = database.GetCollection<Module>("Modules");

            //Unique constraint for ModuleName
            var option = new CreateIndexOptions() { Unique = true };
            var field = new StringFieldDefinition<Module>("ModuleName");
            var indexDefinition = new IndexKeysDefinitionBuilder<Module>().Ascending(field);
            var indexModel = new CreateIndexModel<Module>(indexDefinition, option);
            _modules.Indexes.CreateOne(indexModel);

            //Unique constraint for PathToExe
            field = new StringFieldDefinition<Module>("PathToExe");
            indexDefinition = new IndexKeysDefinitionBuilder<Module>().Ascending(field);
            indexModel = new CreateIndexModel<Module>(indexDefinition, option);
            _modules.Indexes.CreateOne(indexModel);
        }


        public Module Create(Module module)
        {
            int new_module_id = counterContext.GetNextId("module");
            module.ModuleId = new_module_id;

            Console.WriteLine("Inserting new Module. Name: " + module.ModuleName + " Id: " + new_module_id);

            _modules.InsertOne(module);
            return module;
        }

        public List<Module> GetAll()
        {
            //Console.WriteLine("In Participant Get");
            //var docCount = _participants.CountDocuments(part => true);
            //Console.WriteLine(docCount);
            return _modules.Find(module => true).ToList();

        }

        public Module Find(int id) =>
            _modules.Find(module => module.ModuleId == id).SingleOrDefault();

        public Module Find(string moduleName) =>
            _modules.Find(module => module.ModuleName == moduleName).SingleOrDefault();

        public void Delete(int id) =>
            _modules.DeleteOne(part => part.ModuleId == id);

    }
}
