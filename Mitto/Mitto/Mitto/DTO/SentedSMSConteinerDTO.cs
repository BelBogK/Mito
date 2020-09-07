using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model.DTO
{
    public class SentedSMSConteinerDTO
    {
        public List<SentedSMSDTO> Items{ get; set; }
        public int Count { get; set; }
    }
}
