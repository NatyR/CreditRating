using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proposal.API.Common
{
    public class EnvironmentsBase
    {
        public EnvironmentsBase(IConfiguration _configuration)
        {
            MQ_CONNECTIONSTRING = _configuration["MQ_RABBIT:MQ_CONNECTIONSTRING"];
            MQ_USERNAME = _configuration["MQ_RABBIT:MQ_USERNAME"];
            MQ_PASSWORD = _configuration["MQ_RABBIT:MQ_PASSWORD"];
            MQ_QUEUE_CARD = _configuration["MQ_RABBIT:MQ_QUEUE_CARD"];
            MQ_QUEUE_CUSTOMER = _configuration["MQ_RABBIT:MQ_QUEUE_CUSTOMER"];
        }

        public string MQ_CONNECTIONSTRING { get; set; }
        public string MQ_USERNAME { get; set; }
        public string MQ_PASSWORD { get; set; }
        public string MQ_QUEUE_CARD { get; set; }
        public string MQ_QUEUE_CUSTOMER { get; set; }


        


    }
}
