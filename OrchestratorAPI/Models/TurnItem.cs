using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace OrchestratorAPI.Models
{
    public class TurnItem
    {
        public enum Status
        {
            New,
            Proccesing,
            Proccesed,
            Fail
        }

        public int TurnItemId { get; set; }
        public string TurnItemName { get; set;}
        public int? TurnId { get; set;}
        public Turn? Turn { get; set; }
        public Status Item_Status { get; set;}
        public DateTime Create_Time { get; set;}
        public DateTime? Update_Time { get; set;}
        public string InputDate { get; set; }

        public Dictionary<string, object> DeserialInDict(string value)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(value);
        }
    }
}
