using AutoMapper;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;

namespace ProjectX.Application.Service
{
    public class UserService : IUserService      
    {
        private readonly IUser _userRepository;
        private readonly IMapper _mapper;
        private readonly ICryptography _cryptography; 
        public UserService(IUser userRepository, IMapper mapper, ICryptography cryptography)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _cryptography = cryptography;
        }

        public async Task<User> UpdateUser(UserUpdateRequest userUpdateRequest)
        {
            var user = _mapper.Map<User>(userUpdateRequest);
            var existingUser = await _userRepository.GetByIdAsync(userUpdateRequest.Id);
            if (existingUser != null)
            {
                user.CreatedOn = existingUser.CreatedOn;
                user.CreatedBy = existingUser.CreatedBy;
                user.Password = existingUser.Password;
            }
            var updatedUser = await _userRepository.UpdateAsync(user);
            return updatedUser;
        }

        public async Task<User> AddUser(UserAddRequest userAddRequest)
        {
            var encrypt = string.Empty;
            var userData = _mapper.Map<User>(userAddRequest);
            if (!string.IsNullOrEmpty(userData.Password))
            {
                 encrypt = _cryptography.HashThePassword(userData.Password);
            }           
            userData.Password = encrypt;
            var user = await _userRepository.AddAsync(userData);
            return user;
        }

        public async Task<User> RemoveUser(Guid Id)
        {
            var user = await _userRepository.GetByIdAsync(Id);
            if (user != null)
            {
                await _userRepository.RemoveByIdAsync(user.Id);
                return user;
            }
            return user;
        }

        public async Task<IEnumerable<GetUserResponse>> GetAll()
        {
            List<GetUserResponse> listOfUser = new List<GetUserResponse>();
            var IsUserExist = await _userRepository.GetAllAsync();
            if (IsUserExist.Count() > 0)
                listOfUser = _mapper.Map<List<GetUserResponse>>(IsUserExist);
            return listOfUser;
        }

        public async Task<GetUserResponse> GetByID(Guid ID)
        {
            var isUserExist = await _userRepository.GetByIdAsync(ID);
            var getUserById = _mapper.Map<GetUserResponse>(isUserExist);
            return getUserById;
        }

    }
}
