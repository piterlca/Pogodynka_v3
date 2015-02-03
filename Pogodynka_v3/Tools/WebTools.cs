using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Pogodynka_v3.Tools
{
    class WebTools
    {
        public static WebResponse RequestConnection(string path)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            request.Timeout = 5000;
            WebResponse response = null;
            try
            {
                return response = request.GetResponse();
            }
            catch (System.Net.WebException)
            {
                return null;
            }
        }
    }
}
