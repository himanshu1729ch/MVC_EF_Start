using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_EF_Start.Models
{
    public class FinancialDataCollection
    {
        [Key] public int FinanceCollectionID { get; set; }
        public string api_version { get; set; }
        public Pagination pagination { get; set; }
        public List<Result> results { get; set; }
    }

    public class Pagination
    {
        [Key] public int PageID { get; set; }
        public int count { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
        public int per_page { get; set; }
    }

    public class Result
    {
        [Key] public int FinanceReportID { get; set; }
        public string committee_id { get; set; }
        public int cycle { get; set; }
        public string committee_state { get; set; }
        public string committee_name { get; set; }
        public float cash_on_hand_beginning_period { get; set; }
        public float net_contributions { get; set; }
        public float all_loans_received { get; set; }
        public float net_operating_expenditures { get; set; }
        public float disbursements { get; set; }
        public float last_cash_on_hand_end_period { get; set; }
        public string treasurer_name { get; set; }
        public FinancialDataCollection FinanceCollectionID { get; set; }
        /*
                public Result(string committee_id, int cycle, string committee_state, string committee_name, float cash_on_hand_beginning_period, float net_contributions, float all_loans_received, float net_operating_expenditures, float disbursements, float last_cash_on_hand_end_period, string treasurer_name)
                {
                    this.committee_id = committee_id;
                    this.cycle = cycle;
                    this.committee_state = committee_state;
                    this.committee_name = committee_name;
                    this.cash_on_hand_beginning_period = cash_on_hand_beginning_period;
                    this.net_contributions = net_contributions;
                    this.all_loans_received = all_loans_received;
                    this.net_operating_expenditures = net_operating_expenditures;
                    this.disbursements = disbursements;
                    this.last_cash_on_hand_end_period = last_cash_on_hand_end_period;
                    this.treasurer_name = treasurer_name;
                }*/
    }
}