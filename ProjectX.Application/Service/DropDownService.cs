using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.DropDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Service
{
    public class DropDownService: IDropDownService
    {
        private readonly IClient _clientRepository;
        private readonly IProject _projectRepository;
        private readonly IProjectUser _projectUserRepository;
        private readonly IUser _userRepository;
        public DropDownService(IClient clientRepository, IProjectUser projectUserRepository, IProject projectRepository, IUser userRepository)
        {
            _clientRepository = clientRepository;
            _projectUserRepository = projectUserRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<List<DropDownModel>> CheckProjectUserDropdown(Guid projectId)
        {
            var userValue = await _projectUserRepository.CheckProjectUserExist(projectId);
            return userValue;
        }

        public async Task<List<DropDownModel>> GetClientDropdownList()
        {
            var clientValue = await _clientRepository.ClientList();
            return clientValue;
        }

        public async Task<List<DropDownModel>> GetProjectDropdownList()
        {
            var projectValue = await _projectRepository.ProjectList();
            return projectValue;
        }

        public async Task<List<DropDownModel>> GetUserDropdownList()
        {
            var projectValue = await _userRepository.UserList();
            return projectValue;
        }
    }
}
