namespace DeliveryApp
{
    abstract class Delivery
    {
        internal string Address;
        internal DateTime DeliveryTime;
        internal List<string> Products;
       
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
                    Console.WriteLine("Enter quantity for " + product + ":");
                    int quantity = int.Parse(Console.ReadLine());
                    Products.Add($"{product} x {quantity}");
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
        internal bool IsToWarehouse { get; set; }
        internal string Department { get; set; }
        internal bool NeedsUnloading { get; set; }
        public override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Retail Delivery");
        }
        public override void GetSpecificDeliveryData()
        {
            Console.WriteLine("Is the delivery to a warehouse or a store? (warehouse/store)");
            string deliveryLocation = Console.ReadLine();
            if (deliveryLocation.ToLower() == "warehouse")
            {
                IsToWarehouse = true;
            }
            else if (deliveryLocation.ToLower() == "store")
            {
                IsToWarehouse = false;
                Console.WriteLine("Enter department:");
                Department = Console.ReadLine();
                Console.WriteLine("Does the delivery need unloading? (yes/no)");
                string unloadingAnswer = Console.ReadLine();
                NeedsUnloading = unloadingAnswer.ToLower() == "yes";
            }
            else
            {
                Console.WriteLine("Invalid delivery location.");
            }
        }
        public override void DisplaySpecificDeliveryData()
        {
            if (IsToWarehouse)
            {
                Console.WriteLine("Delivery Location: Warehouse");
            }
            else
            {
                Console.WriteLine("Delivery Location: Store");
                Console.WriteLine($"Department: {Department}");
                Console.WriteLine($"Unloading Required: {NeedsUnloading}");
            }
        }
    }

    class CorpDelivery : Delivery
    {
        internal bool IsWorkingHoursDelivery { get; set; }
        internal int WorkingHour { get; set; }
        internal bool NeedsPass { get; set; }
        public override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Corporate Delivery");
        }
        public override void GetSpecificDeliveryData()
        {
            Console.WriteLine("Enter working hour (24-hour format):");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int hour) && hour >= 0 && hour <= 23)
                {
                    WorkingHour = hour;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid working hour. Please enter a number between 0 and 23.");
                }
            }
            Console.WriteLine("Is the delivery required during working hours? (yes/no)");
            string workingHoursAnswer = Console.ReadLine();
            IsWorkingHoursDelivery = workingHoursAnswer.ToLower() == "yes";
            Console.WriteLine("Is a pass required to access the workspace? (yes/no)");
            string passAnswer = Console.ReadLine();
            NeedsPass = passAnswer.ToLower() == "yes";
        }
        public override void DisplaySpecificDeliveryData()
        {
            Console.WriteLine($"Working Hour: {WorkingHour:D2}");
            Console.WriteLine($"Pass Required: {NeedsPass}");
            if (IsWorkingHoursDelivery)
            {
                Console.WriteLine("Delivery required during working hours.");
            }
            else
            {
                Console.WriteLine("Delivery not required during working hours.");
            }
        }
    }

    class Order<TDelivery,
    TStruct> where TDelivery : Delivery
    {
        internal TDelivery Delivery;
        internal int Number;
        internal string Description;
        public void GetOrderData()
        {
            Console.WriteLine("Enter delivery address:");
            Delivery.Address = Console.ReadLine();
            Delivery.GetProducts();
            Console.WriteLine("Enter desired delivery time (YYYY-MM-DD HH:mm):");
            Delivery.DeliveryTime = DateTime.Parse(Console.ReadLine());
            Delivery.GetDeliveryTypeFromUser();
            Console.WriteLine("Enter order number:");
            Number = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter order description:");
            Description = Console.ReadLine();
        }


        public void DisplayOrderInfo()
        {
            Console.WriteLine($"Order Number: {Number}");
            Console.WriteLine($"Order Number: {Description}");
            Console.WriteLine($"Delivery Address: {Delivery.Address}");
            Console.WriteLine("Products ordered:");
            foreach (string productInfo in Delivery.Products)
            {
                string[] parts = productInfo.Split('x');
                string productName = parts[0].Trim();
                int quantity = int.Parse(parts[1].Trim());
                Console.WriteLine($"{productName}: {quantity}");
            }
            Console.WriteLine($"Delivery Time {Delivery.DeliveryTime.ToString("yyyy-MM-dd HH:mm")}");
            Delivery.GetDeliveryType();
            Delivery.DisplaySpecificDeliveryData();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Order<Delivery, string> Order = new Order<Delivery, string>();
            Order.GetOrderData();
            Order.DisplayOrderInfo();
        }
    }
}
