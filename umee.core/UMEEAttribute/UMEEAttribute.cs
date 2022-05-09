using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umee.core.UMEEAttribute
{
    [AttributeUsage(AttributeTargets.All)]
    public class NotMap : Attribute{}

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute{}

    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKey : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class Require : Attribute{}

    [AttributeUsage(AttributeTargets.Property)]
    public class Unique : Attribute{}

    [AttributeUsage(AttributeTargets.Property)]
    public class Email : Attribute { }

    [AttributeUsage (AttributeTargets.Property)]
    public class Id : Attribute{}

    [AttributeUsage(AttributeTargets.Property)]
    public class Search : Attribute{}
}
