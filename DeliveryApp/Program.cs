namespace DeliveryApp
{
    abstract class Delivery
    {
        public string Address;
        public string DeliveryTime;
        public List<string> Products;
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
        public override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Home Delivery");
        }
    }

    class PickPointDelivery : Delivery
    {
        public override void GetDeliveryType()
        {
            Console.WriteLine("Delivery Type: Pick Point Delivery");
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
            Console.WriteLine("Enter desired delivery time:");
            Delivery.DeliveryTime = Console.ReadLine();
            Delivery.GetDeliveryTypeFromUser();

        }


        public void DisplayOrderInfo()
        {
            Console.WriteLine($"Order Number: {Number}");
            Console.WriteLine($"Delivery Address: {Delivery.Address}");
            Console.WriteLine($"Products: {string.Join(", ", Delivery.Products)}");
            Console.WriteLine($"Delivery Time: {Delivery.DeliveryTime}");
            Delivery.GetDeliveryType();
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
