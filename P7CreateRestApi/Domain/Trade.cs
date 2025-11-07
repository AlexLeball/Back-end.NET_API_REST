using System;
using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Domain
{
    public class Trade
    {
        [Key]
        public int TradeId { get; set; }

        [Required(ErrorMessage = "Account is required.")]
        [StringLength(50, ErrorMessage = "Account cannot exceed 50 characters.")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Account type is required.")]
        [StringLength(30, ErrorMessage = "Account type cannot exceed 30 characters.")]
        public string AccountType { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Buy quantity must be a positive number.")]
        public double? BuyQuantity { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Sell quantity must be a positive number.")]
        public double? SellQuantity { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Buy price must be a positive number.")]
        public double? BuyPrice { get; set; }

        [Range(0.0, double.MaxValue, ErrorMessage = "Sell price must be a positive number.")]
        public double? SellPrice { get; set; }

        [StringLength(50, ErrorMessage = "Benchmark cannot exceed 50 characters.")]
        public string Benchmark { get; set; }

        public DateTime? TradeDate { get; set; }

        [StringLength(100, ErrorMessage = "Trade security cannot exceed 100 characters.")]
        public string TradeSecurity { get; set; }

        [StringLength(50, ErrorMessage = "Trade status cannot exceed 50 characters.")]
        public string TradeStatus { get; set; }

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

