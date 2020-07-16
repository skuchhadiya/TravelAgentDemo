using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentAPI.Enums;

namespace TravelAgentAPI.DataModels
{
    public class Client :Common
    {

        public string Name { get; set; }

        public ClientTypes Type { get; set; }

    }
}
