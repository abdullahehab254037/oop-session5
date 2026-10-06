namespace ConsoleApp15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
    public struct DeliveryAddress
    {
        public string city;
        public string street;
        public int BuildingNumber;
        public DeliveryAddress(string city, string street, int buildingNumber)
        {
            this.city = city;
            this.street = street;
            BuildingNumber = buildingNumber;
        }
        public string GetFullAddress()
        {
            return $"{BuildingNumber} {street}, {city}";
        }
    }
    interface ITrackable
    {
        string GetTrackingStatus();
    }
    interface IInsurable
    {
        decimal CalculateInsurance();
    }
}
