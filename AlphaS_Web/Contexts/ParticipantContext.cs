using AlphaS_Web.Models;
using AlphaS_Web.Models.Requests;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaS_Web.Contexts

{
    public class ParticipantContext
    {
        private readonly IMongoCollection<Participant> _participants;
        private readonly CounterContext counterContext;

        public ParticipantContext(IAlphaSDatabaseSettings settings, CounterContext _counterContext)
        {
            //Console.WriteLine("In Participant Service Create");
            counterContext = _counterContext;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _participants = database.GetCollection<Participant>("Participants");
        }

        /*
        public Participant Create(Participant participant)
        {
            _participants.InsertOne(participant);
            return participant;
        }
        */

        public Participant Create(ParticipantCreateRequest participantCR)
        {
            Participant NewPart = GetParticipantFromCR(participantCR);

            _participants.InsertOne(NewPart);
            return NewPart;
        }

        public List<Participant> Get()
        {
            //Console.WriteLine("In Participant Get");
            //var docCount = _participants.CountDocuments(part => true);
            //Console.WriteLine(docCount);
            return _participants.Find(part => true).ToList();

        }

        public Participant Find(int id) =>
            _participants.Find(part => part.ParticipantId == id).SingleOrDefault();

        public Participant Update(int id, ParticipantCreateRequest participantCR) {

            Participant NewPart = new Participant(id, participantCR.Birth_Date, participantCR.Gender, participantCR.Nationality, participantCR.AdditionalInfo);
            Participant OldPart = _participants.Find(part => part.ParticipantId == id).SingleOrDefault();
            NewPart._id = OldPart._id;
            _participants.ReplaceOne(part => part.ParticipantId == id, NewPart);
            return NewPart;
        }
            

        public void Delete(int id) =>
            _participants.DeleteOne(part => part.ParticipantId == id);

        private Participant GetParticipantFromCR(ParticipantCreateRequest participantCR)
        {
            int new_id = counterContext.GetNextId("participant");
            //int new_id = 10;    //todo
            Participant res = new Participant(new_id, participantCR.Birth_Date, participantCR.Gender, participantCR.Nationality, participantCR.AdditionalInfo);

            return res;
        }


    }
}
