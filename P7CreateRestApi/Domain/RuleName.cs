using System;
using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class RuleName
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Rule name is required.")]
        [StringLength(100, ErrorMessage = "Rule name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "JSON field is required.")]
        [StringLength(500, ErrorMessage = "JSON field cannot exceed 500 characters.")]
        public string? Json { get; set; }

        [Required(ErrorMessage = "Template is required.")]
        [StringLength(255, ErrorMessage = "Template cannot exceed 255 characters.")]
        public string? Template { get; set; }

        [Required(ErrorMessage = "SQL string is required.")]
        [StringLength(1000, ErrorMessage = "SQL string cannot exceed 1000 characters.")]
        public string? SqlStr { get; set; }

        [StringLength(1000, ErrorMessage = "SQL part cannot exceed 1000 characters.")]
        public string? SqlPart { get; set; }
    }
}
