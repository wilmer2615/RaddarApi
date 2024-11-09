using System.ComponentModel.DataAnnotations;

namespace Raddar.Api.Models
{
    public class LongUrlRequestModel
    {
        [Required]
        [Url(ErrorMessage = "La URL corta no es una URL válida.")]
        public string Url { get; set; }
    }
}
