using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Domain.Helpers.EmailManager
{
    public class EmailSenderOptions
    {
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderUserName { get; set; }
    }
}
