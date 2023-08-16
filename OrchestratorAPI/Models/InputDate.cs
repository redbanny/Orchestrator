namespace OrchestratorAPI.Models
{
    public class InputDate
    {
        protected string _budget;
        public int Id { get; set; }
        public string Name { get; set; }        
        public CustomerModel? Customer { get; set; }

        public string? CustomerCategory { get; set; }

        public int CustomerCategoryId { get; set; }

        public string IsKeyCustomer { get; set; }

        public string Budget
        {
            get => this._budget;
            set => this._budget = value;
        }

        public string Currency { get; set; }

        public string InfSource { get; set; }

        public int InfSourceId { get; set; }

        public string OfficialSite { get; set; }

        public string SourceNumber { get; set; }

        public DateTime OutputDate { get; set; }
        public int TurnItemId { get; set; }
        public TurnItem? TurnItem { get; set; }
    }
}
