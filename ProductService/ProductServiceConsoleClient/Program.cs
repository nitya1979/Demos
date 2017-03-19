using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductServiceConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductServiceCallbackClient client = new ProductServiceCallbackClient();

            client.ProcessData();

            Console.WriteLine("Process Data Started");
            Console.ReadLine();   
        }
    }
}
