using System.ComponentModel.DataAnnotations;

namespace Raddar.Api.Models
{
    public class URLRequestModel
    {
        [Required]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "La URL corta debe tener entre 1 y 250 caracteres.")]
        [Url(ErrorMessage = "La URL corta no es una URL válida.")]
        public string Url { get; set; }
    }
}
