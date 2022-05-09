using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umee.core.Interfaces.Service
{
    public interface IBaseService<MHGEntity>
    {
        int? InsertService(MHGEntity entity);

        int? UpdateService(MHGEntity entity, Guid entityId);

        int? DeleteService(Guid entityId);

        object GetViaFKService(Guid foreignkey);
    }
}
