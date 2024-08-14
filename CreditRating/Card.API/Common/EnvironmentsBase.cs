using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Card.API.Common
{
    public class EnvironmentsBase
    {
        public EnvironmentsBase(IConfiguration _configuration)
        {
            MQ_CONNECTIONSTRING = _configuration["MQ_RABBIT:MQ_CONNECTIONSTRING"];
            MQ_USERNAME = _configuration["MQ_RABBIT:MQ_USERNAME"];
            MQ_PASSWORD = _configuration["MQ_RABBIT:MQ_PASSWORD"];
            MQ_QUEUE = _configuration["MQ_RABBIT:MQ_QUEUE"];
        }

        public string MQ_CONNECTIONSTRING { get; set; }
        public string MQ_USERNAME { get; set; }
        public string MQ_PASSWORD { get; set; }
        public string MQ_QUEUE { get; set; }
    }
}
