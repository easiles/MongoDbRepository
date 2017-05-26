using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbRepository.Test
{
    /// <summary>
    /// PersonRepository
    /// 创 建 者：mrf10849
    /// 创建日期：2017/5/26 16:33:31
    /// </summary>
    public class PersonRepository : MongoDbRepository
    {
        private static readonly string _connectionString = "mongodb://localhost:27017";
        private static readonly string _databaseName = "BaseData";
        public PersonRepository() : base(_connectionString, _databaseName)
        {
        }
    }
}
