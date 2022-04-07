using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProjAndreAirlinesWebAPI.Model;
using ProjAndreAirlinesWebAPI.Utils;

namespace ProjAndreAirlinesWebAPIUser.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IProjAndreAirlinesDatabaseSettings settings)
        {
            var user = new MongoClient(settings.ConnectionString);
            var database = user.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.CollectionName);
        }

        public async Task<List<User>> Get() =>
             await _users.Find<User>(user => true).ToListAsync();

        public async Task<User> Get(string id) =>
            await _users.Find(user => user.Id == id).FirstOrDefaultAsync();

        public async Task<User> GetUserByDocument(string cpf) =>
           await _users.Find(user => user.Cpf == cpf).FirstOrDefaultAsync();

        public async Task<User> GetUserByUsername(string username) =>
           await _users.Find(user => user.Username == username).FirstOrDefaultAsync();

        public async Task<User> Create(User userIn)
        {
            await _users.InsertOneAsync(userIn);
            return userIn;
        }

        public async Task Update(string id, User userIn) =>
            await _users.ReplaceOneAsync<User>(user => user.Id == id, userIn);

        public async Task Remove(string id) =>
            await _users.DeleteOneAsync<User>(user => user.Id == id);

    }
}
