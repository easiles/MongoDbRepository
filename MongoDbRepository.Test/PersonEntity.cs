using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbRepository.Test
{
    /// <summary>
    /// PersonEntity
    /// </summary>
    [MongoDbCollection("person")]
    public class PersonEntity
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
        
        public DateTime Birthday { get; set; }
    }
}
