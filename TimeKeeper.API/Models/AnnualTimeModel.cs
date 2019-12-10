namespace TimeKeeper.API.Models
{
    public class AnnualRawModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Month { get; set; }
        public decimal Hours { get; set; }
    }

    public class AnnualTimeModel
    {
        public AnnualTimeModel()
        {
            Hours = new decimal[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }
        public MasterModel Project { get; set; }
        public decimal[] Hours { get; set; }
        public decimal Total { get; set; }
    }
}
