using ProjectX.Application.Usecases.VersionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.UnitTesting.Model
{
    public class VersionManagementMaster
    {
        public static GetUpdatesRequest GetUpdatesRequestNotNull()
        {
            GetUpdatesRequest getUpdates = new GetUpdatesRequest();
            getUpdates.ProjectId = new Guid("AFB944EE-36DF-443A-2765-08DC11C019BA");
            getUpdates.Version = 1;
            return getUpdates;
        }
        public static GetUpdatesRequest GetUpdatesRequestProjectIdEmpty()
        {
            GetUpdatesRequest getUpdates = new GetUpdatesRequest();
            getUpdates.ProjectId = Guid.Empty;
            getUpdates.Version = 1;
            return getUpdates;
        }
        public static GetUpdatesRequest GetUpdatesRequestProjectIdVersionZero()
        {
            GetUpdatesRequest getUpdates = new GetUpdatesRequest();
            getUpdates.ProjectId = new Guid("AFB944EE-36DF-443A-2765-08DC11C019BA");
            getUpdates.Version = 0;
            return getUpdates;
        }
        public static GetUpdatesRequest GetUpdatesRequestProjectIdVersionNegative()
        {
            GetUpdatesRequest getUpdates = new GetUpdatesRequest();
            getUpdates.ProjectId = new Guid("AFB944EE-36DF-443A-2765-08DC11C019BA");
            getUpdates.Version = -1;
            return getUpdates;
        }

        public static List<GetUpdateResponse> GetUpdateResponseList()
        {
            List<GetUpdateResponse> getUpdatesList = new List<GetUpdateResponse>()
            {
                new GetUpdateResponse()
                {
                    Version=3,
                    Url="Resource\\Package\\3_bio.jpg"
                },
            };
            return getUpdatesList;
        }
    }
}
