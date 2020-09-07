using Mitto.Model.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mitto.Web.ServicesDTOs
{
    [Route("/Countries")]
    [Route("/Countries/{filter}")]
    public class CountriesDTOService: CountryDTO
    {
    }
}
