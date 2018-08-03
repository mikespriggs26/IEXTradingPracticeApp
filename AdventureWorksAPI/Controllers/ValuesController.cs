using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdventureWorksAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //GET /stock/{symbol}/price
        //https://api.iextrading.com/1.0

        // GET api/values
        [HttpGet]
        public string /*IEnumerable<string>*/ Get()
        {
            var symbol = "unh";
            var IEXTrading_API_PATH = "https://api.iextrading.com/1.0/stock/{0}/company";

            IEXTrading_API_PATH = string.Format(IEXTrading_API_PATH, symbol);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                //For IP-API
                client.BaseAddress = new Uri(IEXTrading_API_PATH);
                HttpResponseMessage response = client.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();
                //string myResponse = response;

                //if (response.IsSuccessStatusCode)
                //{
                    var historicalDataList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    
                    string myResult = historicalDataList.ToString();
               
                    CompanyResponse item = Newtonsoft.Json.JsonConvert.DeserializeObject<CompanyResponse>(myResult);
                
                    string result = "Company name: " + item.companyName + "\nSymbol: " + item.symbol + "\nExchange: "
                    + item.exchange + "\nIndustry: " + item.industry + "\nWebsite: " + item.website + "\nCEO: " +
                    item.CEO + "\nIssue Type: " + item.issueType + "\nSector: " + item.sector + "\nDescription: " +
                    item.description;  

                //}

                return result;
                
            }
            
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "valueinde";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
