using System;
using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class BidList
    {
        [Key]
        public int BidListId { get; set; }

        [Required(ErrorMessage = "Account is required.")]
        [StringLength(50, ErrorMessage = "Account cannot exceed 50 characters.")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Bid type is required.")]
        [StringLength(30, ErrorMessage = "Bid type cannot exceed 30 characters.")]
        public string BidType { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Bid quantity must be a positive number.")]
        public double? BidQuantity { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Ask quantity must be a positive number.")]
        public double? AskQuantity { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Bid must be a positive number.")]
        public double? Bid { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Ask must be a positive number.")]
        public double? Ask { get; set; }

        [StringLength(50, ErrorMessage = "Benchmark cannot exceed 50 characters.")]
        public string Benchmark { get; set; }

        public DateTime? BidListDate { get; set; }

        [StringLength(255, ErrorMessage = "Commentary cannot exceed 255 characters.")]
        public string Commentary { get; set; }

        [StringLength(100, ErrorMessage = "Bid security cannot exceed 100 characters.")]
        public string BidSecurity { get; set; }

        [StringLength(50, ErrorMessage = "Bid status cannot exceed 50 characters.")]
        public string BidStatus { get; set; }

        [Required(ErrorMessage = "Trader is required.")]
        [StringLength(50, ErrorMessage = "Trader name cannot exceed 50 characters.")]
        public string Trader { get; set; }

        [StringLength(50, ErrorMessage = "Book name cannot exceed 50 characters.")]
        public string Book { get; set; }

        [StringLength(50, ErrorMessage = "Creation name cannot exceed 50 characters.")]
        public string CreationName { get; set; }

        public DateTime? CreationDate { get; set; }

        [StringLength(50, ErrorMessage = "Revision name cannot exceed 50 characters.")]
        public string RevisionName { get; set; }

        public DateTime? RevisionDate { get; set; }

        [StringLength(50, ErrorMessage = "Deal name cannot exceed 50 characters.")]
        public string DealName { get; set; }

        [StringLength(50, ErrorMessage = "Deal type cannot exceed 50 characters.")]
        public string DealType { get; set; }

        [StringLength(50, ErrorMessage = "Source list ID cannot exceed 50 characters.")]
        public string SourceListId { get; set; }

        [StringLength(20, ErrorMessage = "Side cannot exceed 20 characters.")]
        [RegularExpression(@"^(Buy|Sell|Other)?$", ErrorMessage = "Side must be 'Buy', 'Sell', or 'Other'.")]
        public string Side { get; set; }
        
    }
}
