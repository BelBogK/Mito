using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mitto.Web.ServicesDTOs
{
    [Route("/SMS/Sent")]
    public class GetSentSMSDTOService
    {
        public DateTime dateTimeFrom { get; set; }
        public DateTime dateTimeTo { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
    }
}
