using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_EF_Start.Models
{
    /*    public class FinancialDataCollection
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ID { get; set; }
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
    */
    public class Result
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ID { get; set; }
        public string committee_id { get; set; }
        public int cycle { get; set; }
        public string committee_state { get; set; }
        public string committee_name { get; set; }
        public string treasurer_name { get; set; }
        public float cash_on_hand_beginning_period { get; set; }
        public float net_contributions { get; set; }
        public float all_loans_received { get; set; }
        public float net_operating_expenditures { get; set; }
        public float disbursements { get; set; }
        public float last_cash_on_hand_end_period { get; set; }
        // public FinancialDataCollection FinancialDataCollection { get; set; }
        // public FinancialDataCollection FinanceCollectionID { get; set; }
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

    public class ChartModel
    {
        public string ChartType { get; set; }
        public string Labels { get; set; }
        public string Data { get; set; }
        public string Title { get; set; }
    }

    public class Filing
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ID { get; set; }
        public string candidate_id { get; set; }
        public string form_type { get; set; }
        public string form_category { get; set; }
        public string document_description { get; set; }
        public string means_filed { get; set; }
        public int cycle { get; set; }
        public string pdf_url { get; set; }
    }

    public class CandOrCommittee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ID { get; set; }
        public string candidate_id { get; set; }
        public string candidate_name { get; set; }
        public string office { get; set; }
        public string state { get; set; }
        public string party_aff { get; set; }
        public string cand_status { get; set; }
        public int active_thr { get; set; }

    }
    public class Committee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ID { get; set; }
        public string committee_id { get; set; }
        public string committee_name { get; set; }
        public string committee_type { get; set; }
        public string treasurer_name { get; set; }
        public string party_aff { get; set; }
        public string state { get; set; }
        public string filing_freq { get; set; }
    }
}
