﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Usecases.VersionManagement
{
    public class GetUpdatesRequest
    {
        public Guid ProjectId { get; set; }
        public int Version { get; set; }
    }
}
