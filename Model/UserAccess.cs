using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserAccess
    {
        public int Uaid { get; set; }
        public string UserId { get; set; }
        public string AccessName { get; set; }
        public char Allow { get; set; }
        public string CreatedBy { get; set; }
        public char ActInactInd { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
