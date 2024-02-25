using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using ProjectX.Application.Usecases.Entity;
using ProjectX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.UnitTesting.Model
{
    public class EntityMaster
    {
        public static List<GetEntityResponse> GetEntityResponseListIsNotNull()
        {
            List<GetEntityResponse> entityList = new List<GetEntityResponse>
             {
               new GetEntityResponse
                {
                 Id = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04"),
                 ProjectID= new Guid("0BB01FA1-A8B3-46F4-8CBB-32EC2755EFF0"),
                 Title="Mahindra",
                 Active=true,
                 DisplayName="M&M",
                 ThumbnailUrl="Resource\\Thumbnails\\XUV-Car-11-PNG-Image.png",
                 Argument1="Test1",
                 Argument2="Test2",
                 Argument3="Test3",
                 CreatedBy="Admin",
                 CreatedOn=Convert.ToDateTime("2024-01-08"),
                 LastModifiedBy="Lakshmi",
                 LastModifiedOn=Convert.ToDateTime("2024-01-08"),
               }
            };
            return entityList;
        }

        public static GetEntityResponse EntityResponseNotNull()
        {
            GetEntityResponse getEntityResponse = new GetEntityResponse();
            getEntityResponse.Id = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04");
            getEntityResponse.ProjectID = new Guid("0BB01FA1-A8B3-46F4-8CBB-32EC2755EFF0");
            getEntityResponse.Title = "Mahindra";
            getEntityResponse.Active = true;
            getEntityResponse.DisplayName = "M&M";
            getEntityResponse.ThumbnailUrl = "Resource\\Thumbnails\\XUV-Car-11-PNG-Image.png";
            getEntityResponse.Argument1 = "Test1";
            getEntityResponse.Argument2 = "Test2";
            getEntityResponse.Argument3 = "Test3";
            getEntityResponse.CreatedBy = "Admin";
            getEntityResponse.CreatedOn = Convert.ToDateTime("2024-01-08");
            getEntityResponse.LastModifiedBy = "Lakshmi";
            getEntityResponse.LastModifiedOn = Convert.ToDateTime("2024-01-08");
            return getEntityResponse;
        }

        public static Entity EntityNotNull()
        {
            Entity entity = new Entity();
            entity.Id = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04");
            entity.ProjectID = new Guid("0BB01FA1-A8B3-46F4-8CBB-32EC2755EFF0");
            entity.Title = "Mahindra";
            entity.Active = true;
            entity.DisplayName = "M&M";
            entity.ThumbnailUrl = "Resource\\Thumbnails\\XUV-Car-11-PNG-Image.png";
            entity.Argument1 = "Test1";
            entity.Argument2 = "Test2";
            entity.Argument3 = "Test3";
            entity.CreatedBy = "Admin";
            entity.CreatedOn = Convert.ToDateTime("2024-01-08");
            entity.LastModifiedBy = "Lakshmi";
            entity.LastModifiedOn = Convert.ToDateTime("2024-01-08");
            return entity;
        }

        public static List<Entity> EntityListNotNull()
        {
            List<Entity> entityList = new List<Entity>
            {
                new Entity
                {
                 Id = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04"),
                 ProjectID= new Guid("0BB01FA1-A8B3-46F4-8CBB-32EC2755EFF0"),
                 Title="Mahindra",
                 Active=true,
                 DisplayName="M&M",
                 ThumbnailUrl="Resource\\Thumbnails\\XUV-Car-11-PNG-Image.png",
                 Argument1="Test1",
                 Argument2="Test2",
                 Argument3="Test3",
                 CreatedBy="Admin",
                 CreatedOn=Convert.ToDateTime("2024-01-08"),
                 LastModifiedBy="Lakshmi",
                 LastModifiedOn=Convert.ToDateTime("2024-01-08"),
                }                
            };
            return entityList;
        }
        public static EntityAddRequest EntityAddNotNull()
        {
            EntityAddRequest entityAddRequest = new EntityAddRequest();
            entityAddRequest.ProjectID= new Guid("0BB01FA1-A8B3-46F4-8CBB-32EC2755EFF0");
            entityAddRequest.Title = "Mahindra";
            entityAddRequest.Active = true;
            entityAddRequest.DisplayName = "M&M";
            //entityAddRequest.File = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Hello, World!")), 0, 13, "form-data", "hello.txt");
            entityAddRequest.CreatedBy = "Admin";
            entityAddRequest.Argument1="Test1";
            entityAddRequest.Argument2="Test2";
            entityAddRequest.Argument3="Test3";
            return entityAddRequest;
        }

        public static EntityUpdateRequest EntityUpdateNotNull()
        {
            EntityUpdateRequest entityUpdateRequest = new EntityUpdateRequest();
            entityUpdateRequest.Id= new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04");
            entityUpdateRequest.ProjectID = new Guid("0BB01FA1-A8B3-46F4-8CBB-32EC2755EFF0");
            entityUpdateRequest.Title = "Mahindra";
            entityUpdateRequest.Active = true;
            entityUpdateRequest.DisplayName = "M&M";
            entityUpdateRequest.File = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Hello, World!")), 0, 13, "form-data", "hello.txt");
            entityUpdateRequest.LastModifiedBy = "Admin";
            entityUpdateRequest.Argument1 = "Test1";
            entityUpdateRequest.Argument2 = "Test2";
            entityUpdateRequest.Argument3 = "Test3";
            return entityUpdateRequest;
        }
    }
}
