using Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Helpers
{
    public class SequenceRepository
    {
        protected readonly IMongoDatabase _database;
        protected readonly IMongoCollection<Sequence> _collection;

        public SequenceRepository(IMongoDatabase database)
        {
            _database = database;
            _collection = _database.GetCollection<Sequence>(typeof(Sequence).Name);
        }

        public Counter GetSequenceValue(string sequenceName)
        {
            var filter = Builders<Sequence>.Filter.Eq(s => s.SequenceName, sequenceName);
            var update = Builders<Sequence>.Update.Inc(s => s.SequenceValue, 1);

            var result = _collection.FindOneAndUpdate(filter, update, new FindOneAndUpdateOptions<Sequence, Sequence> { IsUpsert = true, ReturnDocument = ReturnDocument.After });

            Counter _counter = new Counter();

            _counter.Value = result.SequenceValue;
            _counter.Id = result.SequenceName;

            return _counter;
        }
    }
}
