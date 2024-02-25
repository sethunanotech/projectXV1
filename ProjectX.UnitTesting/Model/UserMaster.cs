using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.UnitTesting.Model
{
    public class UserMaster
    {
        #region GetUser
        public static List<GetUserResponse> userListIsNotNull()
        {
            List<GetUserResponse> userList = new List<GetUserResponse>
            {
              new GetUserResponse
              {
               Id = new Guid("EB215EAA-0C91-408E-8424-92E07D823E5D"),
               ClientID= new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04"),
               Name="UserAdmin",
               UserName="Admin",
               Password ="6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=",
               CreatedOn = Convert.ToDateTime("2024-01-08"),
               CreatedBy = "Lakshmi",
               LastModifiedOn = Convert.ToDateTime("2024-01-08"),
               LastModifiedBy = "Preethi",
              },
            };
            return userList;
        }


        public static List<User> UserListNotNull()
        {
            List<User> userList = new List<User>
            {
              new User
              {
               Id = new Guid("EB215EAA-0C91-408E-8424-92E07D823E5D"),
               ClientID= new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04"),
               Name="UserAdmin",
               UserName="Admin",
               Password ="6G94qKPK8LYNjnTllCqm2G3BUM08AzOK7yW30tfjrMc=",
               CreatedOn = Convert.ToDateTime("2024-01-08"),
               CreatedBy = "Lakshmi",
               LastModifiedOn = Convert.ToDateTime("2024-01-08"),
               LastModifiedBy = "Preethi",
              },
            };
            return userList;
        }
        #endregion

        #region GetUserById
        public static GetUserResponse GetUserListById()
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
        #endregion

        #region AddUser
        public static Client ClientNotNull()
        {
            Client client = new Client();
            client.Id = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04");
            client.Name = "Client";
            client.Address = "Chennai";
            client.CreatedOn = Convert.ToDateTime("2024-01-08");
            client.CreatedBy = "Siva";
            client.LastModifiedOn = Convert.ToDateTime("2024-01-08");
            client.LastModifiedBy = "Lakshmi";
            return client;
        }
        public static UserAddRequest UserAddRequestNotNull()
        {
            UserAddRequest userAddRequest = new UserAddRequest();
            userAddRequest.UserName = "Admin";
            userAddRequest.Name = "UserAdmin";
            userAddRequest.ClientID = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04");
            userAddRequest.CreatedBy = "Lakshmi";
            return userAddRequest;
        }

        public static UserUpdateRequest UserUpdateRequest()
        {
            UserUpdateRequest userUpdateRequest = new UserUpdateRequest();
            userUpdateRequest.Id = new Guid("EB215EAA-0C91-408E-8424-92E07D823E5D");
            userUpdateRequest.UserName = "Admin";
            userUpdateRequest.Name = "UserAdmin";
            userUpdateRequest.ClientId= new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04");
            userUpdateRequest.LastModifiedBy = "Preethi";
            return userUpdateRequest;
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
        #endregion
    }
}
