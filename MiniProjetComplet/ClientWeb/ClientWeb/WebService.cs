using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientWeb.WebServices;

namespace ClientWeb
{
    public class WebService
    {
        private static ServiceClient service = new ServiceClient();

        public static ServiceClient Service
        {
            get { return WebService.service; }
            set { WebService.service = value; }
        }



    }
}