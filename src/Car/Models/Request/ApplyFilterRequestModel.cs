namespace Car.Models.Request
{
    public class ApplyFilterRequestModel
    {
        public int? YearFrom { get; set; }

        public int? YearTo { get; set; }

        public string Color { get; set; }

        public double? PriceFrom { get; set; }

        public double? PriceTo { get; set; }

        public string Access { get; set; }

        public string Brand { get; set; }

        public int? Page { get; set; }
    }
}
