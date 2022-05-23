using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyBookStore.Services.Payment;
using Ninject;
using System.Reflection;

namespace TrickyBookStore.ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            IPaymentService paymentService = kernel.Get<IPaymentService>();

            
            Console.WriteLine(paymentService.GetPaymentAmount(8, 5, 2022));
        }

    }
}
