using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_WEB_API.DTO
{
    public class StudentDTO
    {
        public long StudentId { get; set; }
        public string StudentName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public DateTime InsertDateTime { get; set; }

    }
}
