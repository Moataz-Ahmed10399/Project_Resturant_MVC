namespace Project_Resturant_MVC.Services
{
    public interface IPricingService
    {
        PricingResult Calculate(decimal subtotal, DateTime now);
    }

    public class PricingResult
    {
        public decimal HappyHourDiscount { get; set; }
        public decimal BulkDiscount { get; set; }
        public decimal TotalDiscount => HappyHourDiscount + BulkDiscount;

        public decimal Tax(decimal taxableAmount, decimal taxRate) => Math.Round(taxableAmount * taxRate, 2);
    }

    public class PricingService : IPricingService
    {
        private const decimal TaxRate = 0.085m;        
        private const decimal HappyHourRate = 0.20m;    
        private const decimal BulkRate = 0.10m;         
        private const decimal BulkThreshold = 100m;    

        public PricingResult Calculate(decimal subtotal, DateTime now)
        {
            var result = new PricingResult();

            if (now.Hour >= 15 && now.Hour < 17)
                result.HappyHourDiscount = Math.Round(subtotal * HappyHourRate, 2);

            if (subtotal >= BulkThreshold)
                result.BulkDiscount = Math.Round(subtotal * BulkRate, 2);

            return result;
        }
    }
}
