using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;

namespace localHost
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        gettestdata GetInfo(string phoneNumber, string dashNumber);
        [OperationContract]
        string Update(UpdateUser u);

    }
    [DataContract]
    public class GetERP
    {

        string TelNumber = string.Empty;
        int CustSeq = 0;
        string CustName = string.Empty;
        int PJTSeq = 0;
        string PJTName = string.Empty;
        string SLALevel = string.Empty;

        [DataMember(Order = 0)]
        public string telNumber
        {
            get { return TelNumber; }
            set { TelNumber = value; }
        }
        [DataMember(Order = 1)]
        public int custSeq
        {
            get { return CustSeq; }
            set { CustSeq = value; }
        }
        [DataMember(Order = 2)]
        public string custName
        {
            get { return CustName; }
            set { CustName = value; }
        }
        [DataMember(Order = 3)]
        public int pjtSeq
        {
            get { return PJTSeq; }
            set { PJTSeq = value; }
        }
        [DataMember(Order = 4)]
        public string pjtName
        {
            get { return PJTName; }
            set { PJTName = value; }
        }
       [DataMember (Order = 5 )]
       public string slaLevel
        {
            get { return SLALevel; }
            set { SLALevel = value; }
        }
    }
 
    [DataContract]
    public class UpdateUser
    {
        string phoneNumber = string.Empty;
        int CustSeq = 0;
        string CustName = string.Empty;
        string PJTSeq = string.Empty;
        string PTJName = string.Empty;
        string SLALevel = string.Empty;


        [DataMember(Order = 0)]
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        [DataMember(Order = 1)]
        public int CustSEQ
        {
            get { return CustSeq; }
            set { CustSeq = value; }
        }

        [DataMember(Order = 2)]
        public string CUSTName
        {
            get { return CUSTName; }
            set { CUSTName = value; }
        }
        [DataMember(Order = 3)]
        public string PTJSeq
        {
            get { return PTJSeq; }
            set { PTJSeq = value; }
        }
        [DataMember(Order = 4)]
        public string PTJNAME
        {
            get { return PTJName; }
            set { PTJName = value; }
        }
        [DataMember (Order =5)]
        public string ServiceLevel
        {
            get { return SLALevel; }
            set { SLALevel = value; }
        }

    }
    [DataContract]
    public class gettestdata
    {
        [DataMember (Order = 1)] 
        public DataSet GettingData
        {
            get;
            set;
        }
    }
 

}
