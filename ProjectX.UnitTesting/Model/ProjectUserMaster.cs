using ProjectX.Application.Usecases.ProjectUsers;
using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;

namespace ProjectX.UnitTesting.Model
{
    public class ProjectUserMaster
    {
        public static List<GetProjectUserResponse> ListOfProjectUsers()
        {
            List<GetProjectUserResponse> projects = new List<GetProjectUserResponse>()
         {
             new GetProjectUserResponse()
             {
                 ID = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                 ProjectID= new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                 UserID = new Guid("389F7367-68A6-4B6C-8A99-08DC119A422A"),
                 CreatedOn = Convert.ToDateTime("2024-01-08"),
                 CreatedBy = "2",
                 LastModifiedOn = Convert.ToDateTime("2024-01-08"),
                 LastModifiedBy = "2",
             },
         };
            return projects;
        }

        public static List<ProjectUser> ProjectUserListNotNull()
        {
            List<ProjectUser> projectUser = new List<ProjectUser>()
            {
             new ProjectUser()
            {
            ProjectID = new Guid("24D92485-0C99-4FCD-7F58-08DC06E1D710"),
            UserID = new Guid("389F7367-68A6-4B6C-8A99-08DC119A422A"),
            CreatedOn = Convert.ToDateTime("2024-01-08"),
            CreatedBy = "2",
            LastModifiedOn = Convert.ToDateTime("2024-01-08"),
            LastModifiedBy = "2",
            },

        };
            return projectUser;
        }
        public static ProjectUser ProjectUserNotNull()
        {
            ProjectUser projectUser = new ProjectUser();
            projectUser.Id = new Guid("24D92485-0C99-4FCD-7F58-08DC06E1D710");
            projectUser.ProjectID = new Guid("60D0347F-312A-43C3-AB60-08DC0C2703A7");
            projectUser.UserID = new Guid("389F7367-68A6-4B6C-8A99-08DC119A422A");
            projectUser.CreatedOn = Convert.ToDateTime("2024-01-08");
            projectUser.CreatedBy = "2";
            projectUser.LastModifiedOn = Convert.ToDateTime("2024-01-08");
            projectUser.LastModifiedBy = "2";
            return projectUser;
        }

       
        public static GetProjectUserResponse GetProjectUsers()
        {

            var GetProjectUserResponse = new GetProjectUserResponse()
            {
                ID = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                ProjectID = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                UserID = new Guid("389F7367-68A6-4B6C-8A99-08DC119A422A"),
                CreatedOn = Convert.ToDateTime("2024-01-08"),
                CreatedBy = "2",
                LastModifiedOn = Convert.ToDateTime("2024-01-08"),
                LastModifiedBy = "2",
            };


            return GetProjectUserResponse;
        }
        public static ProjectUserAddRequest AddProjectUser()
        {
            ProjectUserAddRequest projectUser = new ProjectUserAddRequest();
            projectUser.ProjectID = new Guid("6626D2EE-BF9D-4FF3-800F-A8F8B368EC88");
            projectUser.UserID = new Guid("E09A780B-1734-4872-5092-08DC23C82CDA");
            projectUser.CreatedBy = "2";
            return projectUser;
        }

        public static ProjectUserAddRequest AddProjectUserProjectIDEmpty()
        {
            ProjectUserAddRequest projectUser = new ProjectUserAddRequest();
            projectUser.ProjectID = Guid.Empty;
            projectUser.UserID = new Guid("389F7367-68A6-4B6C-8A99-08DC119A422A");
            projectUser.CreatedBy = "2";
            return projectUser;
        }
        public static ProjectUserAddRequest AddProjectUserUserIDEmpty()
        {
            ProjectUserAddRequest projectUser = new ProjectUserAddRequest();
            projectUser.ProjectID = new Guid("60D0347F-312A-43C3-AB60-08DC0C2703A7");
            projectUser.UserID = Guid.Empty;
            projectUser.CreatedBy = "2";
            return projectUser;
        }

        public static GetUserResponse GetUserData()
        {
            var userList = new GetUserResponse()
            {
                Id = new Guid("EB215EAA-0C91-408E-8424-92E07D823E5D"),
                ClientID = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04"),
                Name = "UserAdmin",
                UserName = "Admin",
                Password = "6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=",
                CreatedOn = Convert.ToDateTime("2024-01-08"),
                CreatedBy = "Lakshmi",
                LastModifiedOn = Convert.ToDateTime("2024-01-08"),
                LastModifiedBy = "Preethi",
            };
            return userList;
        }


        public static ProjectUserUpdateRequest UpdateProjectUser()
        {
            ProjectUserUpdateRequest projectUser = new ProjectUserUpdateRequest();
            projectUser.Id = new Guid("B72C329C-73A4-4548-9D65-4FE142BD0AC3");
            projectUser.UserID = new Guid("E09A780B-1734-4872-5092-08DC23C82CDA");
            projectUser.ProjectID = new Guid("6626D2EE-BF9D-4FF3-800F-A8F8B368EC88");
            projectUser.LastModifiedBy = "2";
            return projectUser;
        }
        public static User UserNotNull()
        {
            var UserExist = new User()
            {
                Id = new Guid("EB215EAA-0C91-408E-8424-92E07D823E5D"),
                ClientID = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04"),
                Name = "UserAdmin",
                UserName = "Admin",
                Password = "6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=",
                CreatedOn = Convert.ToDateTime("2024-01-08"),
                CreatedBy = "Lakshmi",
                LastModifiedOn = Convert.ToDateTime("2024-01-08"),
                LastModifiedBy = "Preethi",
            };
            return UserExist;
        }
    }
}
