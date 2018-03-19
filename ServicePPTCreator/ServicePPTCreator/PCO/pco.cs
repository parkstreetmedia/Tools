using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace ServicePPTCreator
{
    public static class PCO
    {
        public static pcoItems.Items GetNextPlanItems(string nextServiceURL, string apiKey, string apiSecret) {

            pcoPlans.Plans sundayPlans = new pcoPlans.Plans();
            sundayPlans = JSonHelper.ConvertJSonToObject<pcoPlans.Plans>(GetHttpWebResponse(nextServiceURL, apiKey, apiSecret));
            string nextPlanURL = "";
            //hey look, IDs aren't in order of when they were created..that's... odd
            if (sundayPlans.data.Count > 0) {
               var nextService = sundayPlans.data.OrderBy(c => c.attributes.sort_date).First();
                nextPlanURL = nextService.links.self;
            }

            pcoPlan.Plan currentPlan = new pcoPlan.Plan();
            currentPlan = JSonHelper.ConvertJSonToObject<pcoPlan.Plan>(GetHttpWebResponse(nextPlanURL, apiKey, apiSecret));

            pcoItems.Items items = new pcoItems.Items();
            items = JSonHelper.ConvertJSonToObject<pcoItems.Items>(GetHttpWebResponse(currentPlan.data.links.items, apiKey, apiSecret));
            return items;
        }

        private static string GetHttpWebResponse(string url, string apiKey, string apiSecret) {
            string response = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            //Set values for the request back
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            //Greg's Readonly API Key
            req.Headers.Add("Authorization", "Basic " + Base64Encode(apiKey + ":" + apiSecret));

            using (HttpWebResponse res = (HttpWebResponse)req.GetResponse()) {
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                using (StreamReader readStream = new StreamReader(res.GetResponseStream(), Encoding.UTF8)) {
                    response = readStream.ReadToEnd();
                }
            }

            return response;
        }

        private static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}