using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2023
{
    public class Program
    {
        public static DataClasses2DataContext context = new DataClasses2DataContext();
        static void Main(string[] args)
        {
            DataSource();
            Console.Read();
        }
        static void IntoLINQ()
        {
            // The Three Parts of a LINQ Query:
            // 1. Data source.
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            // 2. Query creation.
            // numQuery is an IEnumerable<int>
            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            // 3. Query execution.
            foreach (int num in numQuery)
            {
                Console.WriteLine("{0,1}", num);
            }


        }
        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }
        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }
        static void Ordering()
        {
            var QueryLondonCustomers3 =
                 from cust in context.clientes
                 where cust.Ciudad == "London"
                 orderby cust.NombreCompañia ascending
                 select cust;

            foreach (var item in QueryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Grouping()
        {
            var queryCustomersByCity =
                from cust in context.clientes
                group cust by cust.Ciudad;
            // customerGroup is an IGrouping<string, Customer>
            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("   {0}", customer.NombreCompañia);
                }
            }
        }
        static void Grouping2()
        {
            var custQuery =
                from cust in context.clientes
                group cust by cust.Ciudad into custGroup
                where custGroup.Count() > 2
                orderby custGroup.Key
                select custGroup;
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Joining()
        {
            var innerJoinQuery =
                from cust in context.clientes
                join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }

        static void introLambda()
        {
            // The 3 parts of a LINQ Query:
            // 1. Data Source.
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            // 2. Query creation.
            // numQuery is an IEnumerable<int>
            var numQuery = numbers.Where(num => num % 2 == 0);

            // 3. Query execution
            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }
        }

        static void dataSourceLambda()
        {
            var queryAllCustomers = context.clientes.Select(cust => cust);

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void FilteringLambda()
        {
            var queryLondonCustomers = context.clientes.Where(cust => cust.Ciudad == "Londres");
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void OrderingLambda()
        {
            var queryLondonCustomers = context.clientes
                .Where(cust => cust.Ciudad == "London")
                .OrderBy(cust => cust.NombreCompañia);
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }

        static void GroupingLambda()
        {
            var queryCustomersByCity = context.clientes.GroupBy(cust => cust.Ciudad);
            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                {
                    Console.WriteLine(customerGroup.Key);
                    foreach (clientes customer in customerGroup)
                    {
                        Console.WriteLine("   {0}", customer.NombreCompañia);
                    }
                }
            }

        }

        static void Grouping2Lambda()
        {
            var custQuery = context.clientes
                .GroupBy(cust => cust.Ciudad)
                .Where(custGroup => custGroup.Count() > 2)
                .OrderBy(custGroup => custGroup.Key)
                .Select(custGroup => custGroup);
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }

        }

        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes
                .Join(context.Pedidos, cust => cust.idCliente, dist =>
                dist.IdCliente, (cust, dist) => new {
                    CustomerName = cust.NombreCompañia,
                    DistributorName = dist.PaisDestinatario
                });
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
    }
}
