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
using Newtonsoft.Json.Linq;

namespace MVC_EF_Start.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;
       
        //private readonly AppSettings _appSettings;
        static string BASE_URL = "https://api.open.fec.gov/v1";
        static string API_KEY = "62dlTBl8X2ILVEGP1WQui09CHLRi7SDfRAaSytdu";

        public ApplicationDbContext dbContext;
        public const string SessionKeyName = "ElectionData";
        public HomeController(ApplicationDbContext context)
        {
            dbContext = context;
            // _appSettings = appSettings.Value;
        }

        public async Task<ViewResult> Index()
        {
            string[] committeeIDs = { "C00555748", "C00239533", "C00358796", "C00724070", "C00429613", "C00163121", "C00195065" };

            foreach (string committeID in committeeIDs)
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string FinancialData_API_PATH = BASE_URL + "/committee/" + committeID + "/totals/?sort_hide_null=false&sort_nulls_last=false&page=1&api_key="
                  + API_KEY + "&per_page=20&sort_null_only=false&sort=-cycle";

                string financialData = null;

                httpClient.BaseAddress = new Uri(FinancialData_API_PATH);
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(FinancialData_API_PATH)
                                                        .GetAwaiter().GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        financialData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    }

                    JObject parsedResponse = JObject.Parse(financialData);
                    JArray fData = (JArray)parsedResponse["results"];

                    Result report = new Result();
                    report.committee_id = (string)parsedResponse["results"][0]["committee_id"];
                    report.cycle = (int)parsedResponse["results"][0]["cycle"];
                    report.committee_state = (string)parsedResponse["results"][0]["committee_state"];
                    report.committee_name = (string)parsedResponse["results"][0]["committee_name"];
                    report.cash_on_hand_beginning_period = (float)parsedResponse["results"][0]["cash_on_hand_beginning_period"];
                    report.net_contributions = (float)parsedResponse["results"][0]["net_contributions"];
                    report.all_loans_received = (float)parsedResponse["results"][0]["all_loans_received"];
                    report.net_operating_expenditures = (float)parsedResponse["results"][0]["net_operating_expenditures"];
                    report.disbursements = (float)parsedResponse["results"][0]["disbursements"];
                    report.last_cash_on_hand_end_period = (float)parsedResponse["results"][0]["last_cash_on_hand_end_period"];
                    report.treasurer_name = (string)parsedResponse["results"][0]["treasurer_name"];

                    if (dbContext.results.Where(c => c.committee_id == report.committee_id).ToList().Count == 0)
                    {
                        dbContext.results.Add(report);
                    }

                }
                catch (Exception e)
                {
                    // This is a useful place to insert a breakpoint and observe the error message
                    Console.WriteLine(e.Message);
                }
            }
            dbContext.SaveChanges();
            await dbContext.SaveChangesAsync();
            return View();
        }

        public async Task<ViewResult> FinancialReport(string committeID)
        {
            Result reportDetails = dbContext.results.Where(c => c.committee_id == committeID).FirstOrDefault();
            await dbContext.SaveChangesAsync();
            return View(reportDetails);
        }

        public async Task<ViewResult> CandOrCommittee(string committeID)
        {
            await dbContext.SaveChangesAsync();
            return View();
        }

        public async Task<ViewResult> Committee(string committeID)
        {
            await dbContext.SaveChangesAsync();
            return View();
        }
        public ViewResult FinancialReportChart(string committeID)
        {
            
            return View();
        }
    }
}