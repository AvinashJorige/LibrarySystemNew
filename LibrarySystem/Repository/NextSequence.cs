using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities;
using MongoDB.Driver;

namespace Repository
{

    public partial class Repository<T> where T : IEntity
    {
        /// <summary>
        /// find entities
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>collection of entity</returns>
        public virtual Counter NextCountSequence(string ColumnSequence)
        {
            return GetNextCount(ColumnSequence);
        }
    }
}
