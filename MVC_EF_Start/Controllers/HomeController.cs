using Microsoft.AspNetCore.Mvc;
using MVC_EF_Start.DataAccess;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_EF_Start.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MVC_EF_Start.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext dbContext;
        public const string SessionKeyName = "ElectionData";
        //private readonly AppSettings _appSettings;
        static string BASE_URL = "https://api.open.fec.gov/v1";
        static string API_KEY = "62dlTBl8X2ILVEGP1WQui09CHLRi7SDfRAaSytdu";
        HttpClient httpClient;

        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
            // _appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FinancialReport()
        {
            return View();
        }

        public IActionResult GetFinancialData(string committeID)
        {
            //string results = HttpContext.Session.GetString(SessionKeyName);
            //ElectionAPIHandler electionApiHandler = new ElectionAPIHandler();
            //FinancialDataCollection FinanceDataFromAPI = electionApiHandler.GetFinancialData(committeID);
            /*String FinancialData = JsonConvert.SerializeObject(FinanceDataFromAPI);

            FinancialDataCollection financialDataColl = new FinancialDataCollection();

            if (FinancialData != "")
            {
                financialDataColl = JsonConvert.DeserializeObject<FinancialDataCollection>(FinancialData);
            }

            if (financialDataColl.results.Length != 0)
            {
                foreach (Result Result in financialDataColl.results)
                {
                    if (dbContext.results.Where(c => c.committee_id.Equals(Result.committee_id)).Count() == 0)
                    {
                        dbContext.results.Add(Result);
                    }
                }
                dbContext.SaveChanges();
            }*/
            //return View("FinancialReport", financialDataColl);

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string FinancialData_API_PATH = BASE_URL + "/committee/" + committeID + "/totals/?sort_hide_null=false&sort_nulls_last=false&page=1&api_key="
              + API_KEY + "&per_page=20&sort_null_only=false&sort=-cycle";

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
                    financialDataColl.results = JsonConvert.DeserializeObject<List<Result>>(financialData);
                    Console.WriteLine(financialDataColl);
                    dbContext.FinancialDataCollection.Add(financialDataColl);
                    /*if (dbContext.results.ToList().Count == 0)
                    {
                        dbContext.FinancialDataCollection.Add(financialDataColl);
                    }*/
                    /*for (int i = 0; i < financialDataColl.results.Count; i++)
                    {
                        dbContext.results.Add(financialDataColl.results[i]);
                    }*/
                  //  dbContext.SaveChangesAsync();
                    //dbContext.SaveChanges();
                }

            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return View(financialDataColl.results);
        }
    }
}