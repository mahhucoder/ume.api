using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.UMEEAttribute;

namespace umee.core.Entities
{
    public class Request
    {
        [PrimaryKey]
        public Guid RequestId { get; set; }

        [Require]
        public string Name { get; set; }

        [Require]
        public string PhoneNumber{ get; set; }

        [Require]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
