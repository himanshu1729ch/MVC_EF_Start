using MVC_EF_Start.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC_EF_Start.Infrastructure.ElectionHandler
{
    public class ElectionAPIHandler
    {
        HttpClient httpClient;

        static string BASE_URL = "https://api.open.fec.gov/v1";
        static string API_KEY = "62dlTBl8X2ILVEGP1WQui09CHLRi7SDfRAaSytdu";
        public ElectionAPIHandler()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

       /* public FinancialDataCollection GetFinancialData(string committeID)
        {
            string FinancialData_API_PATH = BASE_URL + "/committee/" + committeID + "/totals/?sort_hide_null=false&sort_nulls_last=false&page=1&api_key="
              + API_KEY + "&per_page=20&sort_null_only=false&sort=-cycle";
            //string FinancialData_API_PATH = "https://api.open.fec.gov/v1/committee/C00195065/totals/?sort_hide_null=false&sort_nulls_last=false&page=1&api_key=62dlTBl8X2ILVEGP1WQui09CHLRi7SDfRAaSytdu&per_page=20&sort_null_only=false&sort=-cycle";


            httpClient.BaseAddress = new Uri(FinancialData_API_PATH);
            string financialData = "";
            FinancialDataCollection financialDataColl = new FinancialDataCollection();

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(FinancialData_API_PATH)
                                                        .GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    financialData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!financialData.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    financialDataColl = JsonConvert.DeserializeObject<FinancialDataCollection>(financialData);
                    for (int i = 0; i < financialDataColl.results.Length; i++)
                    {

                    }
                }

                //dbContext.Parks.Add(parks);
                //await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return financialDataColl;
        }*/
    }
}
