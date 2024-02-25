using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectX.Application.Contracts;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Domain.Entities;
using ProjectX.Infrastructure.Utility;
using System.Collections;
using System.Text;

namespace ProjectX.Application.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProject _projectRepository;
        private readonly IMapper _mapper;
        private readonly ICryptography _cryptography;
      
        public ProjectService(IProject projectRepository, IMapper mapper, ICryptography cryptography)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            _cryptography = cryptography;
           
        }
       
        public async Task<Project> UpdateProject(ProjectUpdateRequest projectUpdateRequest)
        {
            var getProjects = _mapper.Map<Project>(projectUpdateRequest);
            var projects = await _projectRepository.GetByIdAsync(projectUpdateRequest.Id);
            if(projects != null)
            {
                getProjects.CreatedBy = projects.CreatedBy;
                getProjects.CreatedOn = projects.CreatedOn;
                getProjects.SecretCode = projects.SecretCode;
            }
            var updateProject=await _projectRepository.UpdateAsync(getProjects);

            return updateProject;
        }
        
        public async Task<Project> AddProject(ProjectAddRequest projectAddRequest)
        {           
            var projectData = _mapper.Map<Project>(projectAddRequest);
            if (!string.IsNullOrEmpty(projectData.Title))
            {
               var secretKey = _cryptography.Encrypt(projectData.Title);
                projectData.SecretCode = secretKey; 
            }
            var project = await _projectRepository.AddAsync(projectData);            
            return project;
        }

        public async Task<Project> RemoveProject(Guid Id)
        {
            var project = await _projectRepository.GetByIdAsync(Id);
            if (project != null)
            {
                await _projectRepository.RemoveByIdAsync(project.Id);
                return project;
            }
            return null;
        }

        public async Task<IEnumerable<GetProjectResponse>> GetAll()
        {
            List<GetProjectResponse> listOfProject = new List<GetProjectResponse>();
            var project = await _projectRepository.GetAllAsync();
            if (project.Count() > 0)
            {
                listOfProject = _mapper.Map<List<GetProjectResponse>>(project);
            }
            return listOfProject;
        }

        public async Task<GetProjectResponse> GetByID(Guid ID)
        {
            var project = await _projectRepository.GetByIdAsync(ID);
            var getProjectById = _mapper.Map<GetProjectResponse>(project);
            return getProjectById;
        }
    }
}
