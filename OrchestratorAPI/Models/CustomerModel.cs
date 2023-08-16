using System.ComponentModel.DataAnnotations;

namespace OrchestratorAPI.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Title { get; set; }

        public string ActualTitle { get; set; }

        public string ShortTitle { get; set; }

        /*
        [MaxLength(4000)]
        public string Contacts { get; set; }
        */
        /// <summary>
        /// ИНН
        /// Для резидента допускаются только цифры. Количество цифр может быть либо 10, либо 12.
        /// Для нерезидента количество символов может быть любое, символы тоже могут быть любые.
        /// </summary>
        //[Required]
        //[MaxLength(12)]
        public string Inn { get; set; }
        public string ActualInn { get; set; }

        public Int64? Ogrn { get; set; }

        public bool? Nonresident { get; set; }
    }
}
