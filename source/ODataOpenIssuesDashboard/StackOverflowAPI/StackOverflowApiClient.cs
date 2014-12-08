using Newtonsoft.Json;
using ODataOpenIssuesDashboard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace ODataOpenIssuesDashboard.StackOverflowAPI
{
    public class StackOverflowApiClient : IDisposable
    {

        private const string DefaultBaseAddress = "https://api.stackexchange.com/2.2";

        #region Constructor

        public StackOverflowApiClient(string baseAddress)
        {
            apiClient = new Lazy<HttpClient>(() =>
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                client.BaseAddress = new Uri(baseAddress);
                return client;
            });
        }

        public StackOverflowApiClient()
            : this(DefaultBaseAddress)
        {
            // do nothing
        }

        #endregion

        #region Properties

        private Lazy<HttpClient> apiClient;

        public Uri BaseAddress
        {
            get
            {
                return apiClient.Value.BaseAddress;
            }
        }

        #endregion

        #region IDisposable Methods

        public void Dispose()
        {
            if (apiClient.IsValueCreated)
            {
                apiClient.Value.Dispose();
            }
        }

        #endregion

        #region Public Methods

        public Task<ResourcesList<StackQuestion>> GetQuestionsAsync(DateTime from, DateTime to, string tag)
        {

            string fromDate = ((Int32)(from - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds).ToString();
            string toDate = ((Int32)(to - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds).ToString();

            string url = "questions?pagesize=100&fromdate=" + fromDate + "&todate=" + toDate + "&order=desc&tagged=" + tag + "&site=stackoverflow&filter=!LZg2mkNIf180KABtw)4fd_";
            return this.GetAsync<ResourcesList<StackQuestion>>(url);
        }

        #endregion

        #region Supporting Methods

        public async Task<T> GetAsync<T>(string url)
        {
            var client = apiClient.Value;
            var response = await client.GetAsync(url).ConfigureAwait(false);

            response.EnsureSuccessStatusCode(); // will throw if the request failed

            using (var gzipStream = new GZipStream(await response.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
            using (var reader = new StreamReader(gzipStream))
            {
                var responseString = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(responseString, new JsonSerializerSettings()
                {
                    ContractResolver = new CStylePropertyNameContractResolver()
                });
            }
        }

        #endregion
    }
}