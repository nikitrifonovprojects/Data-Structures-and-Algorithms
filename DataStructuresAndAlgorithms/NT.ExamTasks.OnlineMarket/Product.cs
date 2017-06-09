using System;

namespace NT.ExamTasks.OnlineMarket
{
    public class Product : IComparable<Product>
    {
        public Product(string name , string type, double price)
        {
            this.Name = name;
            this.Type = type;
            this.Price = price;
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public double Price { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Product;
            if (other == null)
            {
                return false;
            }

            return this.Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                ulong c = 0xDEADBEEFDEADBEEF + (ulong)this.Name.GetHashCode();
                ulong d = 0xE2ADBEEFDEADBEEF ^ c;
                ulong a = d += c = c << 15 | c >> -15;
                ulong b = a += d = d << 52 | d >> -52;
                c ^= b += a = a << 26 | a >> -26;
                d ^= c += b = b << 51 | b >> -51;
                a ^= d += c = c << 28 | c >> -28;
                b ^= a += d = d << 9 | d >> -9;
                c ^= b += a = a << 47 | a >> -47;
                d ^= c += b << 54 | b >> -54;
                a ^= d += c << 32 | c >> 32;
                a += d << 25 | d >> -25;
                return (int)(a >> 1) & 0x7FFFFFFF;
            }
        }

        public int CompareTo(Product other)
        {
            var priceCompare = this.Price.CompareTo(other.Price);
            if (priceCompare == 0)
            {
                var nameCompare = this.Name.CompareTo(other.Name);
                if (nameCompare == 0)
                {
                    return this.Type.CompareTo(other.Type);
                }
                else
                {
                    return nameCompare;
                }
            }
            else
            {
                return priceCompare;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", this.Name, this.Price);
        }
    }
}
