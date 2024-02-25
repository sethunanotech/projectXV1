using AutoMapper;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.ProjectUsers;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Service
{
    public class ProjectUserService : IProjectUserService
    {
        private readonly IProjectUser _projectUserRepository;
        private readonly IMapper _mapper;
        public ProjectUserService(IProjectUser projectUserRepository, IMapper mapper)
        {
            _projectUserRepository = projectUserRepository;
            _mapper = mapper;
        }
        public async Task<ProjectUser> AddProjectUser(ProjectUserAddRequest projectUserAddRequest)
        {
            var projectUserData = _mapper.Map<ProjectUser>(projectUserAddRequest);
            var project = await _projectUserRepository.AddAsync(projectUserData);
            return project;

        }

        public async Task<bool> CheckCombination(Guid UserId, Guid ProjectId)
        {
            var isUserProjectExist = await _projectUserRepository.CheckCombinationExist(UserId, ProjectId);
            return isUserProjectExist;
        }

        public async Task<ProjectUser> RemoveProjectUser(Guid id)
        {

            var projectUser = await _projectUserRepository.GetByIdAsync(id);
            if (projectUser != null)
            {
                await _projectUserRepository.RemoveByIdAsync(projectUser.Id);
                return projectUser;
            }
            return null;
        }

        public async Task<IEnumerable<GetProjectUserResponse>> GetAll()
        {
            List<GetProjectUserResponse> listOfProjectUser = new List<GetProjectUserResponse>();
            var projectUser = await _projectUserRepository.GetAllAsync();
            if (projectUser.Count() > 0)
            {
                listOfProjectUser = _mapper.Map<List<GetProjectUserResponse>>(projectUser);

            }
            return listOfProjectUser;
        }

        public async Task<GetProjectUserResponse> GetByID(Guid ID)
        {
            var projectUser = await _projectUserRepository.GetByIdAsync(ID);
            var getProjectById = _mapper.Map<GetProjectUserResponse>(projectUser);
            return getProjectById;
        }

        public async Task<ProjectUser> UpdateProjectUser(ProjectUserUpdateRequest projectUserUpdateRequest)
        {

            var getProjects = _mapper.Map<ProjectUser>(projectUserUpdateRequest);
            var projects = await _projectUserRepository.GetByIdAsync(projectUserUpdateRequest.Id);
            if (projects != null)
            {
                getProjects.CreatedBy = projects.CreatedBy;
                getProjects.CreatedOn = projects.CreatedOn;
                var projectUser = await _projectUserRepository.UpdateAsync(getProjects);
                return projectUser;
            }


            return null;
        }
    }
}
