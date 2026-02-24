namespace SEP_Restaurant_management.DTO
{
    public class ActiveOrderDto
    {
        public int OrderId { get; set; }
        public string? OrderCode { get; set; }
        public int CustomerId { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string? OrderStatus { get; set; }
        public string? Status { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Note { get; set; }
    }
}
