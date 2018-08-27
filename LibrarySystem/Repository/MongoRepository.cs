using Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class MongoRepository
    {
        private readonly IMongoDatabase _database;

        public MongoRepository(IOptions<Settings> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);

                if(client != null)
                {
                    _database = client.GetDatabase(settings.Value.Database);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access MongoDB server. Error :", ex);
            }
        }

        public IMongoCollection<InstituteSettingModel> _instituteSettingModel => _database.GetCollection<InstituteSettingModel>("InstituteSettingModels");

    }
}
