using Microsoft.EntityFrameworkCore;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.DropDown;
using ProjectX.Domain.Entities;
using ProjectX.Persistence.Data;

namespace ProjectX.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IGenericRepository<User> _repository;
        public UserRepository(ApplicationDbContext dbContext, IGenericRepository<User> repository) : base(dbContext)
        {
            _dbContext = dbContext;
            _repository = repository;
        }

        public async Task<List<DropDownModel>> UserList()
        {

            var clientLists = await (from user in _dbContext.Users
                                     select new
                                     DropDownModel
                                     { Id = user.Id, Name = user.Name }).
                                     ToListAsync();
            return clientLists;
        }

        public async Task<User> IsUserNamePasswordExist(User user)
        {
            User userList = null;
            var userexist = await _repository.FindAsync(x => x.UserName == user.UserName && x.Password == user.Password);
            if (userexist.Any())
            {
                foreach (var item in userexist)
                {
                    userList = item;
                }
            }
            return userList;
        }
    }
}
