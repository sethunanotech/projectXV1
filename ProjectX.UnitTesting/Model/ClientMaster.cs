using ProjectX.Application.Usecases.Clients;
using ProjectX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.UnitTesting.Model
{
    public class ClientMaster
    {
        public static List<GetClientResponse> ClientListIsNotNull()
        {
            List<GetClientResponse> clientList = new List<GetClientResponse> {
           new GetClientResponse
           {
               ID = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04"),
               Name = "Client",
               Address = "Chennai",
               CreatedOn = Convert.ToDateTime("2024-01-08"),
               CreatedBy = "2",
               LastModifiedOn = Convert.ToDateTime("2024-01-08"),
               LastModifiedBy = "2",
           },
        };
            return clientList;
        }
        public static List<Client> GetAllClient()
        {
            List<Client> clientList = new List<Client> {
           new Client
           {
               Id = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04"),
               Name = "Client",
               Address = "Chennai",
               CreatedOn = Convert.ToDateTime("2024-01-08"),
               CreatedBy = "2",
               LastModifiedOn = Convert.ToDateTime("2024-01-08"),
               LastModifiedBy = "2",
           },
        };
            return clientList;
        }
        public static Client ClientNotNull()
        {
            Client client = new Client();

            client.Id = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04");
            client.Name = "Client";
            client.Address = "Chennai";
            client.CreatedOn = Convert.ToDateTime("2024-01-08");
            client.CreatedBy = "2";
            client.LastModifiedOn = Convert.ToDateTime("2024-01-08");
            client.LastModifiedBy = "2";
            return client;
        }
        public static GetClientResponse GetClientResponseNotNull()
        {
            GetClientResponse client = new GetClientResponse();

            client.ID = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04");
            client.Name = "Client";
            client.Address = "Chennai";
            client.CreatedOn = Convert.ToDateTime("2024-01-08");
            client.CreatedBy = "2";
            client.LastModifiedOn = Convert.ToDateTime("2024-01-08");
            client.LastModifiedBy = "2";
            return client;
        }
        public static ClientAddRequest ClientAddRequestNotNull()
        {
            ClientAddRequest client = new ClientAddRequest();
            client.Name = "Client";
            client.Address = "Chennai";
            client.CreatedBy = "2";
            return client;
        }
        public static ClientUpdateRequest ClientUpdateRequestNotNull()
        {
            ClientUpdateRequest client = new ClientUpdateRequest();

            client.Id = new Guid("EA47FEBF-569E-4BFF-81C8-08DC11B1CD04");
            client.Name = "Client";
            client.Address = "Chennai";
            client.LastModifiedBy = "2";
            return client;
        }
    }
}
