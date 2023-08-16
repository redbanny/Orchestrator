namespace OrchestratorAPI.Models
{
    public class Turn
    {
        public int TurnId { get; set; }
        public string TurnName { get; set;}
        public ICollection<TurnItem>? TurnItems { get; set; }

        public Turn()
        {
            TurnItems = new List<TurnItem>();
        }
    }
}
