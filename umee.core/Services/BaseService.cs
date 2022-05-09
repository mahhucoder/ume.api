using umee.core.Exceptions;
using umee.core.Interfaces.Infrastructure;
using umee.core.Interfaces.Service;
using umee.core.UMEEAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace umee.core.Services
{
    public class BaseService<UMEEEntity> : IBaseService<UMEEEntity>
    {
        IBaseRepository<UMEEEntity> _baseRepository;

        public BaseService(IBaseRepository<UMEEEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public int? DeleteService(Guid entityId)
        {
            var isDuplicated = false;
            var props = typeof(UMEEEntity).GetProperties().Where(prop => Attribute.IsDefined(prop,typeof(PrimaryKey)));
            
            foreach(var prop in props)
            {
                isDuplicated = _baseRepository.CheckExist(prop.Name,entityId);
            }

            if (isDuplicated)
            {
                var res = _baseRepository.Delete(entityId);

                return res;
        }
            else
            {
                var result = new
                {
                    userMsg = "Người dùng này không tồn tại!",
                    data = ""
                };

                throw new UMEEException(result);
            }
        }

        public int? InsertService(UMEEEntity entity)
        {
            var isValid = GeneralValidation(entity,false);

            if (isValid)
            {
                var res = _baseRepository.Insert(entity);

                return res;
            }

            return null;

        }

        public int? UpdateService(UMEEEntity entity, Guid entityId)
        {
            var isValid = GeneralValidation(entity,true);
            if (isValid)
            {
                var res = _baseRepository.Update(entity, entityId);

                return res;
            }

            return null;
        }

        public Boolean GeneralValidation(UMEEEntity entity,bool isUpdate)
        {
            var isValid = true;
            List<String> errorMsgs = new List<string>();

            //Validate du lieu require
            var propRequries = typeof(UMEEEntity).GetProperties().Where((prop) => Attribute.IsDefined(prop,typeof(Require)));
            foreach(var prop in propRequries)
            {
                var propValue = prop.GetValue(entity);

                if(propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                {
                    errorMsgs.Add($"Thông tin {prop.Name} không được để trống !");
                }
            }

            if (!isUpdate)
            {
                var propUniques = typeof(UMEEEntity).GetProperties().Where((prop) => Attribute.IsDefined(prop, typeof(Unique)));
                foreach(var prop in propUniques)
                {
                    var propValue = prop.GetValue(entity);

                    var isDuplicate = _baseRepository.CheckExist(prop.Name, propValue);

                    if (isDuplicate)
                        errorMsgs.Add($"Thông tin {prop.Name} không được phép trùng !");
                }
            }
            //Validate du lieu unique

            //Validate di lieu email
            var propEmail = typeof(UMEEEntity).GetProperties().Where((prop) => Attribute.IsDefined(prop, typeof(Email)));
            foreach(var prop in propEmail)
            {
                var propValue = prop.GetValue(entity);

                if (!(String.IsNullOrEmpty(propValue.ToString())))
                {
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(propValue.ToString());

                    if (!(match.Success))
                        errorMsgs.Add("Email không hợp lệ !");
                }
            }

            var errorCustomes = ValidationCustome(entity);

            errorMsgs = errorMsgs.Concat(errorCustomes).ToList();

            if(errorMsgs.Count > 0)
            {
                isValid = false;

                var result = new
                {
                    userMsg = Properties.Resources.ValidateError,
                    data = errorMsgs
                };

                throw new UMEEException(result);
            }

            return isValid;
        }

        protected virtual List<string> ValidationCustome(UMEEEntity entity) { 
            var errorMsgs = new List<string>();

            return errorMsgs;
        }

        public object GetViaFKService(Guid foreignkey)
        {
            var foreignField = typeof(UMEEEntity).GetProperties().Where((prop) => Attribute.IsDefined(prop, typeof(ForeignKey))).ToList();

            if(foreignField.Count == 0)
            {
                var result = new
                {
                    devMsg = "Table này không có khóa ngoài !",
                    data = ""
                };

                throw new UMEEException(result);
            }

            var res = _baseRepository.GetViaFK(foreignkey,foreignField[0].Name);

            return res;
        }
    }
}