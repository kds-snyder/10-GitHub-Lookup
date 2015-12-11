using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitHub_Lookup.Classes
{
    public class WebData
    {
        // Read from input webUrl
        // Return result of read as string
        // If read fails:
        //   If web exception ocurred, display webErrString & return empty string
        //   If other exception ocurred, display the exception & return empty string
        public static string getWebData(string webUrl,
                        string webErrString = "An error occurred reading the data")
        {
            try
            {
                // Read the data at the input URL
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                    string json = webClient.DownloadString(webUrl);
                    return json;
                }
            }
            catch (WebException)
            {
                Console.WriteLine(webErrString);
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine
                    ("An error occurred while reading URL {0}: {1}",
                    webUrl, e.Message);
                return "";
            }
        }
    }
}
