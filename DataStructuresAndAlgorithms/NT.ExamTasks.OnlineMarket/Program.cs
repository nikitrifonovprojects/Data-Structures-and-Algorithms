using System;

namespace NT.ExamTasks.OnlineMarket
{
    public class Program
    {
        const string ProductAddedSuccessFormat = "Ok: Product {0} added successfully";
        const string ProductAddedErrorFormat = "Error: Product {0} already exists";
        const string FilterSuccessFormat = "Ok: {0}";
        const string InvalidTypeErrorFormat = "Error: Type {0} does not exists";

        public static void Main()
        {
            var market = new Market();

            while (true)
            {
                var input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                switch (input[0])
                {
                    case "add":
                        var product = new Product(input[1], input[3], double.Parse(input[2]));
                        var addProduct = market.AddProduct(product);
                        string format;
                        if (addProduct)
                        {
                            format = ProductAddedSuccessFormat;
                        }
                        else
                        {
                            format = ProductAddedErrorFormat;
                        }
                        Console.WriteLine(format, product.Name);
                        break;
                    case "filter":
                        if (input[2] == "type")
                        {
                            var filterByType = market.FilterProducts(input[3]);
                            if (filterByType == null)
                            {
                                Console.WriteLine(InvalidTypeErrorFormat, input[3]);
                            }
                            else
                            {
                                Console.WriteLine(FilterSuccessFormat, string.Join(", ", filterByType));
                                break;
                            }
                        }
                        else if (input[2] == "price")
                        {
                            double from = 0;
                            double to = double.MaxValue;
                            int index = 3;
                            if (input[index] == "from")
                            {
                                from = double.Parse(input[index + 1]);
                                index += 2;
                            }

                            if (index < input.Length && input[index] == "to")
                            {
                                to = double.Parse(input[index + 1]);
                            }

                            var result = market.FilterProducts(from, to);
                            Console.WriteLine(FilterSuccessFormat, string.Join(", ", result));
                            break;
                        }

                        break;
                    case "end":
                        return;
                    default:
                        throw new ArgumentException("Invalid command!");
                }
            }
        }
    }
}
