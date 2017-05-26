mongodb仓储类
功能：简单操作mongodb数据

工具：vs2017
Framework 4.5.2
mongodb.driver 2.4.3

使用方法：
项目中可能会有自己的规范，把一个数据的相关类都存放在一个文件夹下，所以，仓储也是以一个database 为一个仓储


新建仓储类需要继承MongoDbRepository类，在该类中有一个构造方法需要传入连接字符串和操作的database
例如：


public class PersonRepository : MongoDbRepository
{

    private static readonly string _connectionString = "mongodb://localhost:27017";
    
    private static readonly string _databaseName = "BaseData";
    
    public PersonRepository() : base(_connectionString, _databaseName)
    {
    }
    
}


实体集合：
实体集合按照我们正常的entity编写即可，只需要在entity上打个[MongoDbCollection("person")]标签，标志为哪个collection
例如：

[MongoDbCollection("person")]

public class PersonEntity
{
    public ObjectId Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public DateTime Birthday { get; set; }
}

具体方法：

插入(封装了mongodb.driver中的InsertOne方法)：

var person = new PersonEntity()
{

    Id = ObjectId.GenerateNewId(),
    Name = "我是测试",
    Age = 18,
    Birthday = DateTime.Parse("1999-01-01"),
    
};
_personRepository.InsertOne<PersonEntity>(person);

//_personRepository.InsertOneAsync<PersonEntity>(person);


批量插入

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


删除

_personRepository.DeleteOne<PersonEntity>(p => p.Age == 18);

//_personRepository.DeleteOneAsync<PersonEntity>(p => p.Age == 18);


批量删除：

_personRepository.DeleteMany<PersonEntity>(p => p.Age == 18);

//_personRepository.DeleteManyAsync<PersonEntity>(p => p.Age == 18);


修改：
注：修改封装了FindOneAndReplace方法，特别注意，先要查找到数据，才可修改，不然会报错。

var list = _personRepository.Find<PersonEntity>(p => p.Age == 18);

var person = list.FirstOrDefault(p => p.Age == 18);

person.Name = "这是测试测试0";


查询：

var result = _personRepository.Find<PersonEntity>(p => p.Age == 18 && p.Name == "我是测试2");

分页，查询总数等，未添加。
_personRepository.UpdateOne<PersonEntity>(p => p.Age == 18, person);
            
            


