using ProjectX.Application.Usecases.Projects;
using ProjectX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.UnitTesting.Model
{
    public class ProjectMaster
    {
        public static List<GetProjectResponse> ProjectListIsNotNull()
        {
            List<GetProjectResponse> projectList = new List<GetProjectResponse> {
           new GetProjectResponse
           {
               ID = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
               ClientID =new Guid("5E256BB5-44A2-400B-6A68-08DC11BA5F0F"),
               Title="Mahindra",
               SecretCode="5hvnNKv2Txs=",
               Active=true,
               CreatedOn = Convert.ToDateTime("2024-01-08"),
               CreatedBy = "2",
               LastModifiedOn = Convert.ToDateTime("2024-01-08"),
               LastModifiedBy = "2",
           },
        };
            return projectList;
        }
        public static GetProjectResponse ProjectIsNotNull()
        {
            GetProjectResponse project = new GetProjectResponse();

            project.ID = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA");
            project.ClientID = new Guid("5E256BB5-44A2-400B-6A68-08DC11BA5F0F");
            project.Title = "Mahindra";
            project.SecretCode = "5hvnNKv2Txs=";
            project.Active = true;
            project.CreatedOn = Convert.ToDateTime("2024-01-08");
            project.CreatedBy = "2";
            project.LastModifiedOn = Convert.ToDateTime("2024-01-08");
            project.LastModifiedBy = "2";
            return project;
        }

        public static ProjectUpdateRequest ProjectUpdateRequestNotNull()
        {
            var UpdateProjectRequest = new ProjectUpdateRequest()
            {
                Id = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                ClientID = new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"),
                Title = "New",
                Active = true,
                SecretCode = "JF34CTDE",
                LastModifiedBy = "1",
            };
            return UpdateProjectRequest;
        }
        public static Client ClientNotNull()
        {
            Client client = new Client();

            client.Id = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA");
            client.Name = "Client";
            client.Address = "Chennai";
            client.CreatedOn = Convert.ToDateTime("2024-01-08");
            client.CreatedBy = "2";
            client.LastModifiedOn = Convert.ToDateTime("2024-01-08");
            client.LastModifiedBy = "2";
            return client;
        }

        public static Project ProjectNotNull()
        {
            var project = new Project()
            {
                Id = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                ClientID = new Guid("5E256BB5-44A2-400B-6A68-08DC11BA5F0F"),
                Title = "New Project",
                SecretCode = "5hvnNKv2Txs=",
                Active = true,
                CreatedOn = Convert.ToDateTime("2024-01-08"),
                CreatedBy = "2",
                LastModifiedOn = Convert.ToDateTime("2024-01-08"),
                LastModifiedBy = "2"
            };
            return project;
        }

        public static List<Project> ListOfProjects()
        {
            List<Project> projects = new List<Project>()
         {
             new Project()
             {
                 ClientID = new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"),
                Title = "Mahindra",
                SecretCode = "5hvnNKv2Txs=",
                CreatedBy = "1",
                CreatedOn = DateTime.Now,
                LastModifiedBy = "1",
                LastModifiedOn = DateTime.Now,
                Active = true,
             },
         }; return projects;
        }

        public static ProjectAddRequest AddProjectRequestNotNull()
        {
            var AddProjectRequest = new ProjectAddRequest()
            {
                ClientID = new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"),
                Active = true,
                Title = "NewProject",
                CreatedBy = "1"
            };
            return AddProjectRequest;
        }
        public static Project ProjectSecretCodeNull()
        {
            var project = new Project()
            {
                Id = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                ClientID = new Guid("5E256BB5-44A2-400B-6A68-08DC11BA5F0F"),
                Title = "New Project",
                SecretCode = "5hvnNKv2Txs=",
                Active = true,
                CreatedOn = Convert.ToDateTime("2024-01-08"),
                CreatedBy = "2",
                LastModifiedOn = Convert.ToDateTime("2024-01-08"),
                LastModifiedBy = "2"
            };
            return project;
        }
    }
}
