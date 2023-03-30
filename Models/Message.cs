using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase_RTD.Models
{
    internal class Message
    {
        public int MessageId { get; set; }
        public string MessageContent { get; set; }
        public DateTime Sended { get; set; }
        public DateTime Readed { get; set; }
    }
}
