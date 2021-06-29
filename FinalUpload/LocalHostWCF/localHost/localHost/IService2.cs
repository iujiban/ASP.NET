using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Configuration;

namespace localHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface IService2
    {
        [OperationContract]
        HanbiroDemo HanbiroGetInfo(string email);

    }
    [DataContract]
    public class HanbiroDemo
    {
        [DataMember (Order = 1)]
        public DataSet HanbiroGetDemo
        {
            get;
            set;
        }
    }
}
