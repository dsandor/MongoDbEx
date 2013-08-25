using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.Driver.Extensions
{
    public static class MongoDBExtensions
    {
        /// <summary>
        /// Extension to create a MongoDB client from a connection string.
        /// </summary>
        /// <param name="connectionString">MongoDB connection string.</param>
        /// <returns>MongoDB client.</returns>
        public static MongoClient ToMongoDBClient(this String connectionString)
        {
            return new MongoClient(connectionString);
        }

        /// <summary>
        /// Extension to create a MongoDB server from a connection string.
        /// </summary>
        /// <param name="connectionString">MongoDB connection string.</param>
        /// <returns>MongoDB client.</returns>
        public static MongoServer ToMongoDBServer(this String connectionString)
        {
            var client = ToMongoDBClient(connectionString);

            if (client != null)
                return client.GetServer();

            return null;
        }

        /// <summary>
        /// Extension to create a MongoDB database from a connection string.
        /// </summary>
        /// <param name="connectionString">MongoDB connection string.</param>
        /// <param name="databaseName">The name of the Mongo Database.</param>
        /// <returns>MongoDB client.</returns>
        public static MongoDatabase ToMongoDB(this String connectionString, string databaseName)
        {
            var server = ToMongoDBServer(connectionString);

            if (server != null)
                return ToMongoDBServer(connectionString).GetDatabase(databaseName);

            return null;
        }
        
        /// <summary>
        /// Inserts entity into the db.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="connectionString">The connection string to the mongo db.</param>
        /// <param name="databaseName">The name of the database in the mongo db.</param>
        /// <returns>A WriteConcernResult with regard to the insert.</returns>
        public static WriteConcernResult InsertIntoMongoDB(this object entity, string connectionString, string databaseName)
        {
            Type typeOfEntity = entity.GetType();

            var db          = connectionString.ToMongoDB(databaseName);
            var coll        = db.GetCollection(typeOfEntity, typeOfEntity.Name);
            var writeResult = coll.Insert(entity);

            return writeResult;
        }

    }
}
