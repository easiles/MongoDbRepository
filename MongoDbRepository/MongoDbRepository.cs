using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MongoDbRepository
{
    /// <summary>
    /// MongoRepository
    /// </summary>
    public class MongoDbRepository
    {
        private MongoClient _client;
        private IMongoDatabase _database;

        public MongoDbRepository(string _connection, string database)
        {
            if (_client == null)
            {
                _client = new MongoClient(_connection);
            }
            if (_database == null)
            {
                _database = _client.GetDatabase(database);
            }
        }

        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        private IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).GetTypeInfo().GetCustomAttribute<MongoDbCollectionAttribute>().CollectionName);
        }

        #region Insert

        public void InsertOne<TEntity>(TEntity item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            collection.InsertOne(item);
        }

        public async void InsertOneAsync<TEntity>(TEntity item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            await collection.InsertOneAsync(item);
        }

        public void InsertMany<TEntity>(List<TEntity> item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            collection.InsertMany(item);
        }

        public async void InsertManyAsync<TEntity>(List<TEntity> item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            await collection.InsertManyAsync(item);
        }

        #endregion

        #region Update

        public TEntity UpdateOne<TEntity>(Expression<Func<TEntity, bool>> fun, TEntity item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var options = new FindOneAndReplaceOptions<TEntity>
            {
                ReturnDocument = ReturnDocument.After
            };
            return collection.FindOneAndReplace(fun, item);
        }

        public void UpdateOneAsync<TEntity>(Expression<Func<TEntity, bool>> fun, TEntity item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var options = new FindOneAndReplaceOptions<TEntity>
            {
                ReturnDocument = ReturnDocument.After
            };
            collection.FindOneAndReplaceAsync(fun, item);
        }

        #endregion

        #region Delete

        public bool DeleteOne<TEntity>(Expression<Func<TEntity, bool>> fun) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            return collection.DeleteOne(fun).DeletedCount > 0;
        }

        public async Task DeleteOneAsync<TEntity>(Expression<Func<TEntity, bool>> fun) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            await collection.DeleteOneAsync(fun);
        }

        public long DeleteMany<TEntity>(Expression<Func<TEntity, bool>> fun) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            return collection.DeleteMany(fun).DeletedCount;
        }


        public async Task DeleteManyAsync<TEntity>(Expression<Func<TEntity, bool>> fun) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            await collection.DeleteManyAsync(fun);
        }

        #endregion 

        #region Find

        public List<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> fun) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            return collection.Find(fun).ToList();
        }
        //TODO
        public List<TEntity> FindByPage<TEntity>(Expression<Func<TEntity, bool>> fun, int pageIndex, int pageSize) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            return collection.Find(fun).ToList();
        }

        #endregion
    }
}
