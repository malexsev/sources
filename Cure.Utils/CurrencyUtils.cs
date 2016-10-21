using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates; //for ssl use

namespace Cure.Utils
{
    using System;

    public abstract class CurrencyUtils
    {
        internal static DailyInfoServ.DailyInfo DailyInfoServer;
        internal static string BaseCurrency = "RUR";
        public static bool TrustAllCertificateCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public static DataTable GetRates(List<string> currencyList)
        {
            DataTable resTable = PrepareTable();
            DataTable originTable = GetOriginalRates(DateTime.Today).Tables[0];

            foreach (DataRow dr in originTable.Rows)
            {
                if (currencyList.Contains(dr["VchCode"].ToString().Trim()))
                {
                    DataRow nr = resTable.NewRow();
                    nr["from"] = dr["VchCode"].ToString().Trim();
                    nr["to"] = BaseCurrency;
                    nr["rate"] = (decimal)dr["Vcurs"] / (decimal)dr["Vnom"];
                    resTable.Rows.Add(nr);
                }
            }

            return resTable;
        }

        protected static DataTable PrepareTable()
        {
            DataTable table = new DataTable("data");
            table.Columns.Add("from", typeof(string));
            table.Columns.Add("to", typeof(string));
            table.Columns.Add("rate", typeof(Decimal));
            return table;
        }

        private static DataSet GetOriginalRates(DateTime date)
        {
            try
            {
                DataSet data;

                if (DailyInfoServer == null)
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(TrustAllCertificateCallback);
                    var webProxy = WebRequest.DefaultWebProxy;


                    if ((webProxy != null) && (webProxy.Credentials == null))
                    {

                        Uri uriDest = webProxy.GetProxy(new Uri("http://www.cbr.ru/DailyInfoWebServ/DailyInfo.asmx"));

                        ConfigSaver sc = new ConfigSaver();
                        ConfigurationData configuration = sc.LoadData();
                        webProxy.Credentials = new System.Net.NetworkCredential(configuration.UserName, configuration.Pwd);
                    }

                    DailyInfoServer = new DailyInfoServ.DailyInfo();

                    if (webProxy != null)
                    {
                        DailyInfoServer.AllowAutoRedirect = true;
                        DailyInfoServer.PreAuthenticate = true;
                        DailyInfoServer.Proxy = webProxy;
                    }
                }

                data = DailyInfoServer.GetCursOnDate(DateTime.Today);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

    [Serializable()]
    internal struct ConfigurationData
    {
        public string UserName;
        public string Pwd;
        public string Domain;
        public string AuthType;
    }

    internal class ConfigSaver
    {
        public ConfigurationData LoadData()
        {
            ConfigurationData configurationData = new ConfigurationData { UserName = "Maksim", AuthType = "Basic" };
            return configurationData;
        }
    }
}
