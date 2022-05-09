using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umee.core.UMEEAttribute;

namespace umee.core.Entities
{
    public class Category
    {
        [PrimaryKey]
        public Guid CategoryId { get; set; }

        [Require]
        [Search]
        public string CategoryName { get; set; }

        public string CategoryImage { get; set; }

        public Boolean ForProduct { get; set; }
    }
}
