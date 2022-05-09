using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.UMEEAttribute;

namespace umee.core.Entities
{
    public class Account
    {
        [PrimaryKey]
        public Guid AccountId { get; set; }

        [Require]
        [NotMap]
        public string FirebaseUID { get; set; }

        [Require]
        public string Name { get; set; }

        [NotMap]
        public bool Admin { get; set; }

        [Require]
        [Email]
        [Unique]
        [NotMap]
        public string Email { get; set; }   

        [Require]
        public DateTime DateOfBirth { get; set; }

        [Require]
        public int Gender { get; set; }
    }
}
