using Entities;
using MongoDB.Driver;
using Repository.Attributes;
using System;
using System.Reflection;

namespace Repository.Helpers
{
    internal static class SequenceHelper<T> where T : IEntity
    {
        #region Collection Flow 
        /// <summary>
        /// Creates and returns a MongoCollection from the specified type and connectionstring.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="connectionString">The connectionstring to use to get the collection from.</param>
        /// <returns>Returns a MongoCollection from the specified type and connectionstring.</returns>
        internal static SequenceRepository GetCollectionSequenceFromConnectionString(string connectionString)
        {
            return GetCollectionSequenceFromConnectionString(connectionString, GetCollectionName());
        }

        /// <summary>
        /// Creates and returns a MongoCollection from the specified type and connectionstring.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="connectionString">The connectionstring to use to get the collection from.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        /// <returns>Returns a MongoCollection from the specified type and connectionstring.</returns>
        internal static SequenceRepository GetCollectionSequenceFromConnectionString(string connectionString, string sequenceVal)
        {
            return GetCollectionSequenceFromUrl(new MongoUrl(connectionString), sequenceVal);
        }

        /// <summary>
        /// Creates and returns a MongoCollection from the specified type and url.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="url">The url to use to get the collection from.</param>
        /// <returns>Returns a MongoCollection from the specified type and url.</returns>
        internal static SequenceRepository GetCollectionSequenceFromUrl(MongoUrl url)
        {
            return GetCollectionSequenceFromUrl(url, GetCollectionName());
        }

        /// <summary>
        /// Creates and returns a MongoCollection from the specified type and url.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="url">The url to use to get the collection from.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        /// <returns>Returns a MongoCollection from the specified type and url.</returns>
        internal static SequenceRepository GetCollectionSequenceFromUrl(MongoUrl url, string sequenceVal)
        {
            return DatabaseHelpers<T>.GetDatabaseSequenceFromUrl(url, sequenceVal);
        }
        #endregion

        #region Collection Name

        /// <summary>
        /// Determines the collection name for T and assures it is not empty
        /// </summary>
        /// <typeparam name="T">The type to determine the collection name for.</typeparam>
        /// <returns>Returns the collection name for T.</returns>
        private static string GetCollectionName()
        {
            var collectionName = typeof(T).GetTypeInfo().BaseType == typeof(object) ?
                GetCollectionNameFromInterface() :
                GetCollectionNameFromType();

            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(T).Name;
            }
            return collectionName.ToLowerInvariant();
        }

        /// <summary>
        /// Determines the collection name from the specified type.
        /// </summary>
        /// <typeparam name="T">The type to get the collection name from.</typeparam>
        /// <returns>Returns the collection name from the specified type.</returns>
        private static string GetCollectionNameFromInterface()
        {
            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = typeof(T).GetTypeInfo().GetCustomAttribute(typeof(CollectionNameAttribute));

            return att != null ? ((CollectionNameAttribute)att).Name : typeof(T).Name;
        }

        /// <summary>
        /// Determines the collectionname from the specified type.
        /// </summary>
        /// <param name="entitytype">The type of the entity to get the collectionname from.</param>
        /// <returns>Returns the collectionname from the specified type.</returns>
        private static string GetCollectionNameFromType()
        {
            Type entitytype = typeof(T);
            string collectionname;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = entitytype.GetTypeInfo().GetCustomAttribute(typeof(CollectionNameAttribute));
            if (att != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionname = ((CollectionNameAttribute)att).Name;
            }
            else
            {
                //if (typeof(Entity).IsAssignableFrom(entitytype))
                //{
                //    // No attribute found, get the basetype
                //    while (!entitytype.BaseType.Equals(typeof(Entity)))
                //    {
                //        entitytype = entitytype.BaseType;
                //    }
                //}
                collectionname = entitytype.Name;
            }

            return collectionname;
        }

        #endregion Collection Name
    }
}