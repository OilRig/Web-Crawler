using Crawler.Ninject;
using Ninject;
using System;



namespace CrawltestingFinal
{
    public class Program
    {

        private static void Main()
        {
            try
            {
                var kernel = new StandardKernel();
                kernel.Load(new NinjectConfigure());

                var runner = kernel.Get<AppRunner>();
                runner.Run();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
            }
        }
    }
}

