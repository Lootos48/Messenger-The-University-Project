using System;
using System.Collections.Generic;
using System.Text;

namespace MessengerMobile.ServerModels
{
    public class Response
    {
        public int userId { get; set; }
        public int chatId { get; set; }
        public string Error { get; set; }
    }
}
