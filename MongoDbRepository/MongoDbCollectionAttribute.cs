using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbRepository
{
    /// <summary>
    /// MongoDbCollectionAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MongoDbCollectionAttribute : Attribute
    {
        public MongoDbCollectionAttribute(string _collectionName)
        {
            CollectionName = _collectionName;
        }

        private string _collectionName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string CollectionName
        {
            get { return _collectionName; }
            set { _collectionName = value; }
        }
    }
}
