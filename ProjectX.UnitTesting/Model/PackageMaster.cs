using Microsoft.AspNetCore.Http;
using ProjectX.Application.Usecases.Package;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.UnitTesting.Model
{
    public class PackageMaster
    {
        public static List<GetPackageResponse> GetPackageResponseList()
        {
            List<GetPackageResponse> userList = new List<GetPackageResponse>
            {
              new GetPackageResponse
              {
               ID = new Guid("B620DC9F-5033-4225-B39D-3D3075930762"),
               ProjectID= new Guid("AFB944EE-36DF-443A-2765-08DC11C019BA"),
               Version=2,
               Active=true,
               Url ="Resource\\2_background.jpg",
               CreatedOn = Convert.ToDateTime("2024-01-08"),
               CreatedBy = "Preethi",
               LastModifiedOn = Convert.ToDateTime("2024-01-08"),
               LastModifiedBy = "Preethi",
              },
            };
            return userList;
        }
        public static GetPackageResponse GetPackageResponseNotNull()
        {
            GetPackageResponse package = new GetPackageResponse();
            package.ID = new Guid("B620DC9F-5033-4225-B39D-3D3075930762");
            package.ProjectID = new Guid("AFB944EE-36DF-443A-2765-08DC11C019BA");
            package.Version = 2;
            package.Active = true;
            package.Url = "Resource\\2_background.jpg";
            package.CreatedOn = Convert.ToDateTime("2024-01-08");
            package.CreatedBy = "Preethi";
            package.LastModifiedOn = Convert.ToDateTime("2024-01-08");
            package.LastModifiedBy = "Preethi";
            return package;
        }
        public static Package PackageNotNull()
        {
            Package package = new Package();
            package.Id = new Guid("B620DC9F-5033-4225-B39D-3D3075930762");
            package.ProjectID = new Guid("AFB944EE-36DF-443A-2765-08DC11C019BA");
            package.Version = 2;
            package.Active = true;
            package.Url = "Resource\\2_background.jpg";
            package.CreatedOn = Convert.ToDateTime("2024-01-08");
            package.CreatedBy = "Preethi";
            package.LastModifiedOn = Convert.ToDateTime("2024-01-08");
            package.LastModifiedBy = "Preethi";
            return package;
        }

        public static PackageAddRequest PackageAddRequestNotNull()
        {
            PackageAddRequest package = new PackageAddRequest();
            package.ProjectID = new Guid("AFB944EE-36DF-443A-2765-08DC11C019BA");
            package.Version = 2;
            package.Active = true;
            package.File = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Hello, World!")), 0, 13, "form-data", "hello.txt");
            package.CreatedBy = "Preethi";
            return package;
        }
        public static Project ProjectNotNull()
        {
            var project = new Project()
            {
                Id = new Guid("6B29FC40-CA47-1067-B31D-00DD010662DA"),
                ClientID = new Guid("5E256BB5-44A2-400B-6A68-08DC11BA5F0F"),
                Title = "New Project",
                SecretCode = "Br67XCR",
                Active = true,
                CreatedOn = Convert.ToDateTime("2024-01-08"),
                CreatedBy = "2",
                LastModifiedOn = Convert.ToDateTime("2024-01-08"),
                LastModifiedBy = "2"
            };
            return project;
        }
        public static GetProjectResponse GetProjectResponseNotNull()
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
        public static PackageUpdateRequest PackageUpdateRequestNotNull()
        {
            PackageUpdateRequest package = new PackageUpdateRequest();
            package.Id = new Guid("B620DC9F-5033-4225-B39D-3D3075930762");
            package.ProjectID = new Guid("AFB944EE-36DF-443A-2765-08DC11C019BA");
            package.Version = 2;
            package.Active = true;
            package.File = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Hello, World!")), 0, 13, "form-data", "hello.txt");
            package.LastModifiedBy = "Preethi";
            return package;
        }

         public static List<Package> PackageList()
         {
            List<Package> package = new List<Package> {
              new Package
              {
                  Id = new Guid("B620DC9F-5033-4225-B39D-3D3075930762"),
                  ProjectID=new Guid("AFB944EE-36DF-443A-2765-08DC11C019BA"),
                  Version=2,
                  Active=true,
                  Url="Resource\\2_background.jpg",
                  CreatedOn = Convert.ToDateTime("2024-01-08"),
                  CreatedBy = "Preethi",
                  LastModifiedOn = Convert.ToDateTime("2024-01-08"),
                  LastModifiedBy = "Preethi",
              },
           };
         return package;
         }
    }
}
