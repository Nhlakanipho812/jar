namespace kinetic.BusinessLogic
{
    public class Coin : ICoin
    {
        public decimal Amount { get; set; }
        public decimal Volume { get; set; }
    }
    public interface ICoin
    {
        decimal Amount { get; set; }
        decimal Volume { get; set; }
    }
}