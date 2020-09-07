using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mitto.Web.ServicesDTOs
{
    [Route("/statistics")]
    [Route("/statistics/{parameters}")]
    public class StatisticDTOService
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string MCCList { get; set; }

    }
}
