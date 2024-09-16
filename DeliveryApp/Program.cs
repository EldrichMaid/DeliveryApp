using System.Runtime.InteropServices.ComTypes;

namespace DeliveryApp
{        
    internal class HomeDelivery : Order
    {
        private int EntranceNumber;
        private string EntranceCode;
        private int FloorNumber;
        private int DoorNumber;
        internal override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Home Delivery");
        }
        internal override void GetSpecificDeliveryData()
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
        internal override void DisplaySpecificDeliveryData()
        {
            Console.WriteLine($"Entrance Number: {EntranceNumber}");
            Console.WriteLine($"Entrance Code: {EntranceCode}");
            Console.WriteLine($"Floor Number: {FloorNumber}");
            Console.WriteLine($"Door Number: {DoorNumber}");
        }
    }

    internal class PickPointDelivery : Order
    {
        private string PickPointCode;
        internal override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Pick Point Delivery");
        }
        internal override void GetSpecificDeliveryData()
        {
            Console.WriteLine("Choose a pick-up point (a, b, c, d):");
            PickPointCode = Console.ReadLine();
            if (!"abcd".Contains(PickPointCode.ToLower()))
            {
                Console.WriteLine("Invalid pick-up point code.");
            }  
        }
        internal override void DisplaySpecificDeliveryData()
        {
            Console.WriteLine($"Pick-up Point: {PickPointCode}");
        }
    }

    internal class RetailDelivery : Order
    {
        private bool IsToWarehouse;
        private string Department;
        private bool NeedsUnloading;
        internal override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Retail Delivery");
        }
        internal override void GetSpecificDeliveryData()
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
        internal override void DisplaySpecificDeliveryData()
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

    internal class CorpDelivery : Order
    {
        private bool IsWorkingHoursDelivery;
        private int WorkingHour;
        private bool NeedsPass;
        internal override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Corporate Delivery");
        }
        internal override void GetSpecificDeliveryData()
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
        internal override void DisplaySpecificDeliveryData()
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

    abstract class Order
    {
        private int Number;
        private string Description;
        private string Address;
        private List<string> Products;
        private DateTime DeliveryTime;
        internal abstract void GetSpecificDeliveryData();
        internal abstract void DisplaySpecificDeliveryData();
        internal abstract void GetDeliveryType();
        internal void GetOrderData()
        {
            
            Console.WriteLine("Enter delivery address:");
            Address = Console.ReadLine();
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
            Console.WriteLine("Enter desired delivery time (YYYY-MM-DD HH:mm):");
            DeliveryTime = DateTime.Parse(Console.ReadLine());            
            Console.WriteLine("Enter order number:");
            Number = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter order description:");
            Description = Console.ReadLine();
                        
        }

        internal void DisplayOrderInfo()
        {
            Console.WriteLine($"Order Number: {Number}");
            Console.WriteLine($"Order Number: {Description}");
            Console.WriteLine($"Delivery Address: {Address}");
            Console.WriteLine("Products ordered:");
            foreach (string productInfo in Products)
            {
                string[] parts = productInfo.Split('x');
                string productName = parts[0].Trim();
                int quantity = int.Parse(parts[1].Trim());
                Console.WriteLine($"{productName}: {quantity}");
            }
            Console.WriteLine($"Delivery Time {DeliveryTime.ToString("yyyy-MM-dd HH:mm")}");           
        }
    }    

    internal class Program
    {
        internal static void GetDeliveryTypeFromUser()
        {
            Console.WriteLine("Is the delivery personal or corporate? (personal/corporate)");
            string deliveryType = Console.ReadLine();
            if (deliveryType == "personal")
            {
                Console.WriteLine("Is the delivery to a home or a pickpoint? (home/pickpoint)");
                string personalDeliveryType = Console.ReadLine();
                if (personalDeliveryType == "home")
                {
                    HomeDelivery order = new HomeDelivery();
                    order.GetOrderData();
                    order.GetSpecificDeliveryData();
                    order.DisplayOrderInfo();
                    order.DisplaySpecificDeliveryData();
                }
                else if (personalDeliveryType == "pickpoint")
                {
                    PickPointDelivery order = new PickPointDelivery();
                    order.GetOrderData();
                    order.GetSpecificDeliveryData();
                    order.DisplayOrderInfo();
                    order.DisplaySpecificDeliveryData();
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
                    RetailDelivery order = new RetailDelivery();
                    order.GetOrderData();
                    order.GetSpecificDeliveryData();
                    order.DisplayOrderInfo();
                    order.DisplaySpecificDeliveryData();
                }
                else if (corporateDeliveryType == "corporate")
                {
                    CorpDelivery order = new CorpDelivery();
                    order.GetOrderData();
                    order.GetSpecificDeliveryData();
                    order.DisplayOrderInfo();
                    order.DisplaySpecificDeliveryData();
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

        static void Main(string[] args)
        {
            GetDeliveryTypeFromUser();            
        }
    }
}
