using System.ComponentModel.DataAnnotations;

namespace CzSharp.DB.Entities
{
    public class Code: IIdentifiable
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Titulek")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Popis")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Ukázka kódu")]
        public string CodeSample { get; set; }
    }
}