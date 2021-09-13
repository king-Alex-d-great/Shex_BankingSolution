using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Domain.Interfaces
{
     public interface IEntity
     {
        public DateTime? CreatedAt => DateTime.Now; 
        public DateTime? UpdatedAt => DateTime.Now;
        public string CreatedBy => "King Aleeex";
        public string UpdatedBy => "Shola nejo";
    }
}
