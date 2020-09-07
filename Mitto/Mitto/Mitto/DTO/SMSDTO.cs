using Mitto.Model.Enumns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.Model.DTO
{

    public class SMSDTO
    {
        public string Text { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public DateTime Sended { get; set; }
        public EMessageState State { get; set; }
    }
}
