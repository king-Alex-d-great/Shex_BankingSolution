using System;

namespace WebUI.domain.Model
{
    public class ReadOnlyCustomerProps
    {
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}