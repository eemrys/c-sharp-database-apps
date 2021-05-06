using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace WindowsFormsApp1.DAL
{
    public class MongoRepository
    {
        private MongoClient client;

        public MongoRepository(string login, string pass)
        {
            string _ConnectionString = "mongodb+srv://{0}:{1}@cluster0.znxuk.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
            client = new MongoClient(string.Format(_ConnectionString, login, pass));
        }

        public MedicalRecord Get(int id)
        {
            var database = client.GetDatabase("Staff");
            var collection = database.GetCollection<MedicalRecord>("StaffInfo");
            var filter = Builders<MedicalRecord>.Filter.Eq("person_id", id);
            var document = collection.Find(filter).FirstOrDefault();
            return document;
        }

        public void Delete(int id)
        {
            var database = client.GetDatabase("Staff");
            var collection = database.GetCollection<MedicalRecord>("StaffInfo");
            var deleteFilter = Builders<MedicalRecord>.Filter.Eq("person_id", id);
            collection.DeleteOne(deleteFilter);
        }

        public void Update(MedicalRecord record)
        {
            var database = client.GetDatabase("Staff");
            var collection = database.GetCollection<MedicalRecord>("StaffInfo");
            var updateFilter = Builders<MedicalRecord>.Filter.Eq("person_id", record.person_id);

            var updateValues = new List<UpdateDefinition<MedicalRecord>>
            {
                Builders<MedicalRecord>.Update.Set("height", record.height),
                Builders<MedicalRecord>.Update.Set("weight", record.weight),
                Builders<MedicalRecord>.Update.Set("blood_type", record.blood_type),
                Builders<MedicalRecord>.Update.Set("eyesight", record.eyesight),
                Builders<MedicalRecord>.Update.Set("drug_dispensary", record.drug_dispensary),
                Builders<MedicalRecord>.Update.Set("psychoneurological_dispensary", record.psychoneurological_dispensary)
            };
            var update = Builders<MedicalRecord>.Update.Combine(updateValues);
            collection.UpdateOne(updateFilter, update);
        }

        public void Insert(MedicalRecord record)
        {
            var database = client.GetDatabase("Staff");
            var collection = database.GetCollection<MedicalRecord>("StaffInfo");
            collection.InsertOne(record);
        }
    }
}
