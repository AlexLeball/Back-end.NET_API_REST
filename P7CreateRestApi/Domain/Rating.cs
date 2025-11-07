using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Moody's rating is required.")]
        [StringLength(50, ErrorMessage = "Moody's rating cannot exceed 50 characters.")]
        public string MoodysRating { get; set; }

        [Required(ErrorMessage = "Standard & Poor's rating is required.")]
        [StringLength(50, ErrorMessage = "S&P rating cannot exceed 50 characters.")]
        public string SandPRating { get; set; }

        [Required(ErrorMessage = "Fitch rating is required.")]
        [StringLength(50, ErrorMessage = "Fitch rating cannot exceed 50 characters.")]
        public string FitchRating { get; set; }

        [Range(1, 255, ErrorMessage = "Order number must be between 1 and 255.")]
        public byte? OrderNumber { get; set; }
    }
}
