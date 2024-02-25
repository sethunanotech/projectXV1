using Microsoft.EntityFrameworkCore;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.DropDown;
using ProjectX.Domain.Entities;
using ProjectX.Persistence.Data;

namespace ProjectX.Persistence.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClient
    {
        private readonly ApplicationDbContext _dbContext;
        public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<DropDownModel>> ClientList()
        {
            var clientLists = await (from client in _dbContext.Clients
                                     select new
                                     DropDownModel
                                     { Id = client.Id, Name = client.Name }).
                                     ToListAsync();
            return clientLists;
        }
    }
}
