using umee.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umee.core.Interfaces.Infrastructure
{
    public interface IBaseRepository<UMEEEntity>
    {
        IEnumerable<object> Get();

        IEnumerable<object> GetViaFK(Guid foreignkey,string? foreignField);

        object Get(Guid entityId);

        int Delete(Guid entityId);

        int Update(UMEEEntity entity, Guid entityId);

        int Insert(UMEEEntity entity);

        Boolean CheckExist(string fieldName, object fieldValue);

        IEnumerable<object> Search(string keyword);
    }
}
