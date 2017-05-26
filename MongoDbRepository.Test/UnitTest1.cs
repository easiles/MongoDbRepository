using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq;

namespace MongoDbRepository.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly PersonRepository _personRepository = new PersonRepository();
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void InsertOne()
        {
            var person = new PersonEntity()
            {
                Id = ObjectId.GenerateNewId(),
                Name = "我是测试",
                Age = 18,
                Birthday = DateTime.Parse("1999-01-01"),
            };
            _personRepository.InsertOne<PersonEntity>(person);
            //_personRepository.InsertOneAsync<PersonEntity>(person);
        }

        [TestMethod]
        public void InsertMany()
        {
            var list = new List<PersonEntity>()
            {
                new PersonEntity()
                {
                    Id = ObjectId.GenerateNewId(),
                    Name = "我是测试",
                    Age = 18,
                    Birthday = DateTime.Parse("1999-01-01"),
                },
                new PersonEntity()
                {
                    Id = ObjectId.GenerateNewId(),
                    Name = "我是测试2",
                    Age = 18,
                    Birthday = DateTime.Parse("1999-01-01"),
                }
            };
            _personRepository.InsertMany<PersonEntity>(list);
            //_personRepository.InsertManyAsync<PersonEntity>(list);
        }

        [TestMethod]
        public void UpdateOne()
        {

            var list = _personRepository.Find<PersonEntity>(p => p.Age == 18);

            var person = list.FirstOrDefault(p => p.Age == 18);
            person.Name = "这是测试测试0";


            _personRepository.UpdateOne<PersonEntity>(p => p.Age == 18, person);
            //_personRepository.InsertManyAsync<PersonEntity>(list);
        }

        [TestMethod]
        public void Find()
        {
            var result = _personRepository.Find<PersonEntity>(p => p.Age == 18 && p.Name == "我是测试2");
        }

        [TestMethod]
        public void DeleteOne()
        {
            _personRepository.DeleteOne<PersonEntity>(p => p.Age == 18);
            //_personRepository.DeleteOneAsync<PersonEntity>(p => p.Age == 18);
        }

        [TestMethod]
        public void DeleteMany()
        {
            _personRepository.DeleteMany<PersonEntity>(p => p.Age == 18);
            //_personRepository.DeleteManyAsync<PersonEntity>(p => p.Age == 18);
        }
    }
}
