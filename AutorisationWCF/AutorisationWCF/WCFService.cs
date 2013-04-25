using System;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
namespace WCFSecurite
{
    public class WCFService : IWCFService
    {
        [PrincipalPermission(SecurityAction.Demand, Role = "user")]
        public String getInfoPublique()
        {
            return "Info publique";
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "VIP")]
        public String getInfoLouche()
        {
            return "Info louche";
        }
    }
}