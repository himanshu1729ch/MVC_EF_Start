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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            string[] candidateIDs = { "P40006033", "P00011569", "P40002172", "S2CO00175", "H0UT03227" };

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

                    if (dbContext.results.Where(r => r.committee_id == committeID).ToList().Count == 0)
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

            //Fetching data from Filing API
            foreach (string candidateID in candidateIDs)
            {
                httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string FilingData_API_PATH = BASE_URL + "/candidate/" + candidateID + "/filings/?sort=-receipt_date&sort_null_only=false&page=1&sort_nulls_last=false&per_page=20&api_key="
                  + API_KEY + "&sort_hide_null=false";

                string filingData = null;

                httpClient.BaseAddress = new Uri(FilingData_API_PATH);
                try
                {
                    HttpResponseMessage response = httpClient.GetAsync(FilingData_API_PATH)
                                                        .GetAwaiter().GetResult();

                    if (response.IsSuccessStatusCode)
                    {
                        filingData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    }

                    JObject parsedResponse = JObject.Parse(filingData);
                    JArray fData = (JArray)parsedResponse["results"];

                    Filing filingReport = new Filing();
                    filingReport.candidate_id = (string)parsedResponse["results"][0]["candidate_id"];
                    filingReport.form_type = (string)parsedResponse["results"][0]["form_type"];
                    filingReport.form_category = (string)parsedResponse["results"][0]["form_category"];
                    filingReport.document_description = (string)parsedResponse["results"][0]["document_description"];
                    filingReport.means_filed = (string)parsedResponse["results"][0]["means_filed"];
                    filingReport.cycle = (int)parsedResponse["results"][0]["cycle"];
                    filingReport.pdf_url = (string)parsedResponse["results"][0]["pdf_url"];
                    if (dbContext.filings.Where(r => r.candidate_id == candidateID).ToList().Count == 0)
                    {
                        dbContext.filings.Add(filingReport);
                    }
                }
                catch (Exception e)
                {
                    // This is a useful place to insert a breakpoint and observe the error message
                    Console.WriteLine(e.Message);
                }
            }

            CandOrCommittee cand = new CandOrCommittee();
            cand.candidate_id = "S2TX00460";
            cand.candidate_name = "AGRIS, JOE";
            cand.office = "Senate";
            cand.state = "TX";
            cand.party_aff = "REPUBLICAN PARTY";
            cand.cand_status = "Not yet a candidate";
            cand.active_thr = 2012;

            CandOrCommittee cand1 = new CandOrCommittee();
            cand1.candidate_id = "H0MA01024";
            cand1.candidate_name = "ABAIR, PETER JON";
            cand1.office = "House";
            cand1.state = "MA";
            cand1.party_aff = "REPUBLICAN PARTY";
            cand1.cand_status = "Prior Candidate";
            cand1.active_thr = 2000;

            CandOrCommittee cand2 = new CandOrCommittee();
            cand2.candidate_id = "H2MT00039";
            cand2.candidate_name = "JOHN ALLEN ABARR";
            cand2.office = "House";
            cand2.state = "MT";
            cand2.party_aff = "REPUBLICAN PARTY";
            cand2.cand_status = "Not yet a candidate";
            cand2.active_thr = 2012;

            dbContext.cand.Add(cand);
            dbContext.cand.Add(cand1);
            dbContext.cand.Add(cand2);

            Committee com = new Committee();
            com.committee_id = "C00353375";
            com.committee_name = "Principal campaign committee";
            com.committee_type = "House";
            com.treasurer_name = "PETER J ABAIR";
            com.party_aff = "REPUBLICAN PARTY";
            com.state = "MA";
            com.filing_freq = "Terminated";

            Committee com1 = new Committee();
            com1.committee_id = "C00492579";
            com1.committee_name = "AMERICAN PRINCIPLES";
            com1.committee_type = "PAC - Qualified";
            com1.treasurer_name = "LAOR EYTAN";
            com1.party_aff = "NONE";
            com1.state = "FL";
            com1.filing_freq = "Quarterly Filer";

            Committee com2 = new Committee();
            com2.committee_id = "C00628529";
            com2.committee_name = "AMERICANS FOR PRINCIPLED LEADERSHIP";
            com2.committee_type = "PAC - NonQualified";
            com2.treasurer_name = "CAROLYN RANDS TAYLOR";
            com2.party_aff = "NONE";
            com2.state = "TX";
            com2.filing_freq = "Administratively terminated";

            Committee com3 = new Committee();
            com3.committee_id = "C00220624";
            com3.committee_name = "BEA MOONEY FOR PRESIDENT PRINCIPAL CAMPAIGN COMMITTEE";
            com3.committee_type = "Presidential";
            com3.treasurer_name = "JEAN PETERS";
            com3.party_aff = "REPUBLICAN PARTY";
            com3.state = "MN";
            com3.filing_freq = "Terminated";

            Committee com4 = new Committee();
            com4.committee_id = "C00780585";
            com4.committee_name = "CARSON 4 CONGRESS PRINCIPAL COMMITTEE";
            com4.committee_type = "House";
            com4.treasurer_name = "PAULETTE CARSON";
            com4.party_aff = "REPUBLICAN PARTY";
            com4.state = "TX";
            com4.filing_freq = "Quarterly Filer";

            dbContext.com.Add(com);
            dbContext.com.Add(com1);
            dbContext.com.Add(com2);
            dbContext.com.Add(com3);
            dbContext.com.Add(com4);

            dbContext.SaveChanges();
            await dbContext.SaveChangesAsync();
            return View();
        }

        public async Task<ViewResult> FinancialReport(string committeID)
        {
            
            Result reportDetails = dbContext.results.Where(c => c.committee_id == committeID).FirstOrDefault();

            string[] ChartLabels = new string[] { "Cash At The Start", "Net Contribution", "Loan Received", "Net Operating Expenditure", "Disbursements", "Cash Ath The End" };
            if (reportDetails != null)
            {
                float[] ChartData = new float[] { reportDetails.cash_on_hand_beginning_period, reportDetails.net_contributions, reportDetails.all_loans_received, reportDetails.net_operating_expenditures,
                reportDetails.disbursements, reportDetails.last_cash_on_hand_end_period };

                ChartModel Model = new ChartModel
                {
                    ChartType = "bar",
                    Labels = String.Join(",", ChartLabels.Select(d => "'" + d + "'")),
                    Data = String.Join(",", ChartData.Select(d => d)),
                    Title = "Financial Status Graph (amount in millions)"
                };
                ViewBag.chartModel = Model;
            }
            await dbContext.SaveChangesAsync();
            return View(reportDetails);
        }

        public async Task<ViewResult> CandOrCommittee(string candidateID)
        {
            CandOrCommittee candDetails = dbContext.cand.Where(c => c.candidate_id == candidateID).FirstOrDefault();

            await dbContext.SaveChangesAsync();
            return View(candDetails);
        }

        public async Task<ViewResult> Committee(string committeeID)
        {
            Committee comDetails = dbContext.com.Where(c => c.committee_id == committeeID).FirstOrDefault();
            await dbContext.SaveChangesAsync();
            return View(comDetails);
        }

        public IActionResult Delete(string cond)
        {
            var rec = dbContext.results.Where(c => c.committee_id == cond).FirstOrDefault();
            if (rec != null)
            {
                dbContext.results.Remove(rec);
                dbContext.SaveChanges();
                TempData["shortMessage"] = "Deleted Successfully";
            }

            return RedirectToAction("FinancialReport", new { val = cond });
        }
        public async Task<ViewResult> filing(string candidateID)
        {
            Filing filingDetails = dbContext.filings.Where(c => c.candidate_id == candidateID).FirstOrDefault();
              
            await dbContext.SaveChangesAsync();
            return View(filingDetails);
        }

        public async Task<ViewResult> aboutus()
        {
            await dbContext.SaveChangesAsync();
            return View();
        }
        
    }
}