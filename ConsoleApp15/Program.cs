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
    public abstract class Shipment : ITrackable, IInsurable
    {
        private string TrackingCode;
        private string Description;
        private int Weight;
        private int DeliveryFee;
        public DeliveryAddress Destination;

        public Shipment(string trackingCode, string description, int weight, int deliveryFee, DeliveryAddress destination)
        {
            TrackingCode = trackingCode;
            Description = description;
            Weight = weight;
            DeliveryFee = deliveryFee;
            Destination = destination;
        }
        public string code
        {
            get
            {
                return TrackingCode;
            }
            set { if (value != null) { TrackingCode = value; } }
        }



        public string description
        {
            get { return Description; }
            set
            {
                if (value != null) { Description = value; }
            }
        }
        public int weight
        {
            get { return Weight; }
            set { if (value > 0) { Weight = value; } }
        }
        public int deliveryFee
        {
            get { return DeliveryFee; }
             set { if (value > 0) { DeliveryFee = value; } }
        }
        public DeliveryAddress deliveryAddress { get; set; }
        public abstract int EstimatedCost
        {
            get;
        }
        public void UpdateDeliveryFee(int newFee)
        {
            if (newFee > 0)
            {
                DeliveryFee = newFee;
            }
        }
        public virtual void PrintShipment()
        {
            Console.WriteLine($"tracking code:{TrackingCode},,,,description:{Description},,,,weight:{Weight},,,,delivery fee:{DeliveryFee}");
        }

        public abstract string GetTrackingStatus();

        public abstract decimal CalculateInsurance();
        public Shipment CopyShipment(Shipment copy)
        {
            return this;
        }
        public Shipment ShallowCopy()
        {
            return (Shipment)this.MemberwiseClone();
        }
        public Shipment DeepCopy(Shipment copy)
        {
            copy.deliveryFee = this.deliveryFee;
            copy.code = this.code;
            copy.description = this.description;
            copy.weight = this.weight;
            copy.Destination = new DeliveryAddress(this.Destination.city,this.Destination.street,this.Destination.BuildingNumber);
            return copy;
        }




        public Shipment(string trackingCode)
        {
            this.TrackingCode = trackingCode;
            Description = "Unknown";
            Weight = 1;
            DeliveryFee = 50;
            Destination = default;
        }



    }
    public class StandardShipment : Shipment, IInsurable, ITrackable
    {
        public StandardShipment(string trackingCode, string description, int weight, int deliveryFee, DeliveryAddress destination) : base(trackingCode, description, weight, deliveryFee, destination)
        {

        }

        public override int EstimatedCost
        {
            get { return base.deliveryFee + (base.weight * 5); }
        }

        public override decimal CalculateInsurance()
        {
            return (this.EstimatedCost * (5 / 10));
        }

        public override string GetTrackingStatus()
        {
            return (this.code + "is ready");
        }

        public override void PrintShipment()
        {
            base.PrintShipment();
            Console.WriteLine($"estimated:{this.EstimatedCost}");
        }
       

    }
    public class ExpressShipment : Shipment, IInsurable, ITrackable
    {
        private int ExtraFee;
        public ExpressShipment(string trackingCode, string description, int weight, int deliveryFee, DeliveryAddress destination, int extrafee) : base(trackingCode, description, weight, deliveryFee, destination)
        {
            ExtraFee = extrafee;
        }
        public int extrafee
        {
            get { return ExtraFee; }
            set { if (extrafee > 0) { ExtraFee = value; } }
        }
        //public decimal EstimatedCost
        //{
        //    get { return this.EstimatedCost + ExtraFee; }
        //}
        public override int EstimatedCost
        {
            get { return base.deliveryFee + (base.weight * 5) + ExtraFee; }
        }
        public override decimal CalculateInsurance()
        {
            return (this.EstimatedCost * (8 / 10));
        }

        public override string GetTrackingStatus()
        {
            return (this.code + "out of delivery");
        }
        public override void PrintShipment()
        {
            base.PrintShipment();
            Console.WriteLine($"estimated:{this.EstimatedCost},,,,esxtrafee:{ExtraFee}");
        }
    }


    public class InternationalShipment : Shipment, IInsurable, ITrackable
    {
        private int CustomsFee;
        private string DestinationCountry;
        public InternationalShipment(string trackingCode, string description, int weight, int deliveryFee, DeliveryAddress destination, string destinationCountry,
            int customsFee) : base(trackingCode, description, weight, deliveryFee, destination)
        {
            DestinationCountry = destinationCountry;
            CustomsFee = customsFee;
        }
        public string destinationCountry
        {
            get { return DestinationCountry; }
            set
            {
                if (value != null) { DestinationCountry = value; }
            }
        }
        public override decimal CalculateInsurance()
        {
            return (this.EstimatedCost * (12 / 10));
        }

        public override string GetTrackingStatus()
        {
            return (this.code + "has been Delivered.");
        }
        public int customsFee
        {
            get { return CustomsFee; }
            set
            {
                if (value > 0)
                {
                    CustomsFee = value;
                }
            }
        }
        //public decimal EstimatedCost
        //{
        //    get { return this.EstimatedCost + CustomsFee; }
        //}
        public override int EstimatedCost
        {
            get { return base.deliveryFee + (base.weight * 5); }
        }
        public override void PrintShipment()
        {
            base.PrintShipment();
            Console.WriteLine($"estimated:{this.EstimatedCost},,,,customfee:{CustomsFee},,,,country:{DestinationCountry}");
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
