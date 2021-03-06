﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities;
using MongoDB.Driver;
using Polly;
using Polly.Retry;
using Repository.Helpers;

namespace Repository
{
    /// <summary>
    /// repository implementation for mongo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly string ConnectionString = "mongodb://localhost:27017/Library";

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="connectionString">connection string</param>
        public Repository()
        {
            Collection = CollectionHelpers<T>.GetCollectionFromConnectionString(ConnectionString);
            SequenceCollection = SequenceHelper<T>.GetCollectionSequenceFromConnectionString(ConnectionString, string.Empty);
        }

        /// <summary>
        /// mongo collection
        /// </summary>
        public IMongoCollection<T> Collection
        {
            get; private set;
        }

        public SequenceRepository SequenceCollection { get; set; }



        public Counter GetNextCount(string sequenceVal)
        {
            return SequenceCollection.GetSequenceValue(sequenceVal);
        }

        /// <summary>
        /// filter for collection
        /// </summary>
        public FilterDefinitionBuilder<T> Filter => Builders<T>.Filter;

        /// <summary>
        /// projector for collection
        /// </summary>
        public ProjectionDefinitionBuilder<T> Project => Builders<T>.Projection;


        private IFindFluent<T, T> Query(Expression<Func<T, bool>> filter)
        {
            return Collection.Find(filter);
        }

        private IFindFluent<T, T> Query()
        {
            return Collection.Find(Filter.Empty);
        }



        #region Simplicity

        /// <summary>
        /// validate if filter result exists
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>true if exists, otherwise false</returns>
        public bool Any(Expression<Func<T, bool>> filter)
        {
            return Retry(() => Collection.AsQueryable<T>().Any(filter));
        }

        #endregion Simplicity

        #region RetryPolicy
        /// <summary>
        /// retry operation for three times if IOException occurs
        /// </summary>
        /// <typeparam name="TResult">return type</typeparam>
        /// <param name="action">action</param>
        /// <returns>action result</returns>
        /// <example>
        /// return Retry(() => 
        /// { 
        ///     do_something;
        ///     return something;
        /// });
        /// </example>
        protected virtual TResult Retry<TResult>(Func<TResult> action)
        {
            return RetryPolicy
                .Handle<MongoConnectionException>(i => i.InnerException.GetType() == typeof(IOException))
                .Retry(3)
                .Execute(action);
        }

        #endregion
    }
}