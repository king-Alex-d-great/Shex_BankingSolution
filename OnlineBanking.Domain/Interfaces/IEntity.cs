using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Domain.Interfaces
{
     public interface IEntity
     {
         public DateTime? CreatedAt { get; set; }
         public DateTime? UpdatedAt { get; set; }
         public string CreatedBy { get; set; }
         public string UpdatedBy { get; set; }
     }
}
