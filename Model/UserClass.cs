using System;
using System.Collections.Generic;
using System.Text;

namespace student.Model
{
    public class UserClass
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!; // IdentityUser Id

        public int ClassId { get; set; }

        public Class? Class { get; set; }
    }
}

