namespace DeliveryApp
{
    abstract class Delivery
    {
        public string Address;
        public DateTime DeliveryTime;
        public List<string> Products;
        public abstract void GetSpecificDeliveryData();
        public abstract void DisplaySpecificDeliveryData();
        public abstract void GetDeliveryType();
        public void GetProducts()
        {
            Console.WriteLine("Enter products (enter 'done' to finish):");
            Products = new List<string>();
            string product = "";
            while (product != "done")
            {
                product = Console.ReadLine();
                if (product != "done")
                {
                    Products.Add(product);
                }
            }
        }
        public void GetDeliveryTypeFromUser()
        {
            Console.WriteLine("Is the delivery personal or corporate? (personal/corporate)");
            string deliveryType = Console.ReadLine();
            if (deliveryType == "personal")
            {
                Console.WriteLine("Is the delivery to a home or a pickpoint? (home/pickpoint)");
                string personalDeliveryType = Console.ReadLine();
                if (personalDeliveryType == "home")
                {
                    this.GetDeliveryType();
                }
                else if (personalDeliveryType == "pickpoint")
                {
                    this.GetDeliveryType();
                }
                else
                {
                    Console.WriteLine("Invalid delivery type.");
                }
            }
            else if (deliveryType == "corporate")
            {
                Console.WriteLine("Is the delivery for retail or not? (retail/corporate)");
                string corporateDeliveryType = Console.ReadLine();
                if (corporateDeliveryType == "retail")
                {
                    this.GetDeliveryType();
                }
                else if (corporateDeliveryType == "corporate")
                {
                    this.GetDeliveryType();
                }
                else
                {
                    Console.WriteLine("Invalid delivery type.");
                }
            }
            else
            {
                Console.WriteLine("Invalid delivery type.");
            }
        }
    }

    class HomeDelivery : Delivery
    {
        internal int EntranceNumber { get; set; }
        internal string EntranceCode { get; set; }
        internal int FloorNumber { get; set; }
        internal int DoorNumber { get; set; }
        public override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Home Delivery");
        }
        public override void GetSpecificDeliveryData()
        {
            Console.WriteLine("Enter entrance number:");
            EntranceNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter entrance code:");
            EntranceCode = Console.ReadLine();
            Console.WriteLine("Enter floor number:");
            FloorNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter door number:");
            DoorNumber = int.Parse(Console.ReadLine());
        }
        public override void DisplaySpecificDeliveryData()
        {
            Console.WriteLine($"Entrance Number: {EntranceNumber}");
            Console.WriteLine($"Entrance Code: {EntranceCode}");
            Console.WriteLine($"Floor Number: {FloorNumber}");
            Console.WriteLine($"Door Number: {DoorNumber}");
        }
    }

    class PickPointDelivery : Delivery
    {
        internal string PickPointCode { get; set; }
        public override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Pick Point Delivery");
        }
        public override void GetSpecificDeliveryData()
        {
            Console.WriteLine("Choose a pick-up point (a, b, c, d):");
            PickPointCode = Console.ReadLine();
            if (!"abcd".Contains(PickPointCode.ToLower()))
            {
                Console.WriteLine("Invalid pick-up point code.");
            }  
        }
        public override void DisplaySpecificDeliveryData()
        {
            Console.WriteLine($"Pick-up Point: {PickPointCode}");
        }
    }

    class RetailDelivery : Delivery
    {
        public override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Retail Delivery");
        }
    }

    class CorpDelivery : Delivery
    {
        public override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Corporate Delivery");
        }
    }

    class Order<TDelivery,
    TStruct> where TDelivery : Delivery
    {
        public TDelivery Delivery;
        public int Number;
        public string Description;
        private void GetOrderData()
        {
            Console.WriteLine("Enter delivery address:");
            Delivery.Address = Console.ReadLine();
            Delivery.GetProducts();
            Console.WriteLine("Enter desired delivery time (YYYY-MM-DD HH:mm):");
            Delivery.DeliveryTime = DateTime.Parse(Console.ReadLine());
            Delivery.GetDeliveryTypeFromUser();

        }


        public void DisplayOrderInfo()
        {
            Console.WriteLine($"Order Number: {Number}");
            Console.WriteLine($"Delivery Address: {Delivery.Address}");
            Console.WriteLine($"Products: {string.Join(", ", Delivery.Products)}");
            Console.WriteLine($"Delivery Time {Delivery.DeliveryTime.ToString("yyyy-MM-dd HH:mm")}");
            Delivery.GetDeliveryType();
            Delivery.DisplaySpecificDeliveryData();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
