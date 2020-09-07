using Mitto.Model.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mitto.Web.ServicesDTOs
{
    [Route("/SMS")]
    [Route("/SMS/{from}")]
    public class SMSDTOService:SMSDTO
    {
    }
}
