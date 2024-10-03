using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using RestApi.Models;
using RestApi.Infrasctructure.Mongo;
using RestApi.Mappers;

namespace RestApi.Repositories;

public class GroupRepository : IGroupRepository{

    private readonly IMongoCollection<GroupEntity>_groups;

    public GroupRepository(IMongoClient mongoClient, IConfiguration configuration)
    {
        var database = mongoClient.GetDatabase(configuration.GetValue<string>("MongoDB:Groups:DatabaseName"));
        _groups = database.GetCollection<GroupEntity>(configuration.GetValue<string>("MongoDB:Groups:CollectionName"));
    }
    public async Task<GroupModel> GetByIdAsync(string id, CancellationToken cancellationToken){
        try
        {
            var filter = Builders<GroupEntity>.Filter.Eq(group => group.Id, id);
            var group = await _groups.Find(filter).FirstOrDefaultAsync(cancellationToken);
            return group.ToModel();

        } catch (FormatException)
        {
            return null;
        }
    }

    public async Task<IList<GroupModel>> GetAllByNameAsync(string name, int pageNumber, int pageSize, string orderBy, CancellationToken cancellationToken)
    {
        var filter = Builders<GroupEntity>.Filter.Regex(group => group.Name, new BsonRegularExpression(name, "i"));
        var sort = Builders<GroupEntity>.Sort.Ascending(group => group.Name);

        if (orderBy == "creationDate")
        {
            sort = Builders<GroupEntity>.Sort.Ascending(group => group.CreatedAt);
        }

        var groups = await _groups.Find(filter).Skip(pageSize * (pageNumber - 1)).Limit(pageSize).Sort(sort).ToListAsync(cancellationToken);

        return groups.Select(group => group.ToModel()).ToList();
    }

    public async Task<GroupModel> CreateAsync(string name, Guid[] Users, CancellationToken cancellationToken)
    {
       var group= new GroupEntity
       {
            Name = name,
            Users = Users,
            CreatedAt = DateTime.UtcNow,
            Id = ObjectId.GenerateNewId().ToString()
       };

       await _groups.InsertOneAsync(group, new InsertOneOptions(), cancellationToken);
       return group.ToModel();
    }

    public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken)
    {
       var filter = Builders<GroupEntity>.Filter.Eq(s => s.Id, id);
       await _groups.DeleteOneAsync(filter, cancellationToken);
    }
}