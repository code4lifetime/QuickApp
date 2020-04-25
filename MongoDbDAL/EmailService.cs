
using MongoDB.Driver;
using MongoDbDAL;
using MongoDbDAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MongoDbDAL
{
    public class EmailService
    {
        private readonly IMongoCollection<Email> _emails;

        public EmailService(IQuickAppDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _emails = database.GetCollection<Email>(settings.QuickAppCollectionName);
        }

        public List<Email> Get() =>
            _emails.Find(email => true).ToList();

        public Email Get(string id) =>
            _emails.Find<Email>(email => email.Id == id).FirstOrDefault();

        public Email Create(Email email)
        {
            _emails.InsertOne(email);
            return email;
        }

        public void Update(string id, Email emailIn) =>
            _emails.ReplaceOne(book => book.Id == id, emailIn);

        public void Remove(Email emailIn) =>
            _emails.DeleteOne(email => email.Id == emailIn.Id);

        public void Remove(string id) =>
            _emails.DeleteOne(email => email.Id == id);
    }
}