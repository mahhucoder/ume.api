using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umee.core.Exceptions
{
    public class UMEEException : Exception
    {
        IDictionary UMEEData = new Dictionary<string, object>();

        public UMEEException(object data)
        {
            this.UMEEData.Add("data",data);
        }

        public override string Message { 
            get {
                return Properties.Resources.ValidateError;
            } 
        }

        public override IDictionary Data
        {
            get { return UMEEData; }
        }
    }
}
