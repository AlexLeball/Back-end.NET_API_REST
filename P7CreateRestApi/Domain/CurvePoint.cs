using System;
using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class CurvePoint
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Curve ID is required.")]
        [Range(1, 255, ErrorMessage = "Curve ID must be between 1 and 255.")]
        public byte? CurveId { get; set; }

        [Required(ErrorMessage = "As-of date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime? AsOfDate { get; set; }

        [Required(ErrorMessage = "Term is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Term must be a positive number.")]
        public double? Term { get; set; }

        [Required(ErrorMessage = "Curve point value is required.")]
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "Curve point value must be a valid number.")]
        public double? CurvePointValue { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreationDate { get; set; }
    }
}
