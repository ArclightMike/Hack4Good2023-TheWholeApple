using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CompanionApp.Utility
{
    internal class ServiceHelper
    {
        #region Post request
        /// <summary>
        /// Post request function
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="strSerializedData">String serialized data.</param>
        /// <param name="strMethod">String method.</param>
        /// <param name="isHeader">If set to <c>true</c> is header.</param>

        public async Task<string> PostRequest(string strSerializedData, string strMethod, bool isHeader, string token)
        {

            string serviceUrl = ConfigEntity.BaseURL + strMethod;
            string text = string.Empty;
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    HttpContent inputContent = new StringContent(strSerializedData, Encoding.UTF8, "application/json");
                    if (isHeader)
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token + "");
                    }
                    var response = await client.PostAsync(serviceUrl, inputContent).ConfigureAwait(true);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        text = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                    }
                    else
                    {
                        text = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                    }

                    return text;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("PostRequest --- " + ex.Message);
                return text;
            }


        }
        #endregion

        #region Get request
        /// <summary>
        /// Get request function
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="strSerializedData">String serialized data.</param>
        /// <param name="strMethod">String method.</param>
        /// <param name="isHeader">If set to <c>true</c> is header.</param>

        public async Task<string> GetRequest(string strSerializedData, string strMethod, bool isHeader, string token)
        {

            string serviceUrl = ConfigEntity.BaseURL + strMethod;
            string text = string.Empty;
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    if (isHeader)
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token + "");
                    }
                    var response = await client.GetAsync(serviceUrl).ConfigureAwait(true);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        text = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                    }
                    else
                    {
                        text = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                    }

                    return text;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetRequest --- " + ex.Message);
                return text;
            }


        }
        #endregion
    }
}
