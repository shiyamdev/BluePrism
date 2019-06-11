using System;
using System.Diagnostics;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;

namespace BluePrism
{
    class Program
    {
        static void Main(string[] args)
        {

            // Autofac DI
            var serviceCollection = new ServiceCollection();
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<WordHandler>().As<IWordHandler>();
            containerBuilder.RegisterType<FileHandler>().As<IFileHandler>().WithParameter("wordsLength",4);
            
            containerBuilder.Populate(serviceCollection);
            var appContainer = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(appContainer);


            // Console UI
            Console.WriteLine("Enter 4 letter Start Word:");
            string startWord = Console.ReadLine();
            Console.WriteLine("Enter 4 letter End Word:");
            string endWord = Console.ReadLine();
            Console.WriteLine("Output file name:");
            string resultFile = Console.ReadLine();

            // Monitor the performance
            var watch = Stopwatch.StartNew();

            var fileHandler = serviceProvider.GetService<IFileHandler>();

            // Function required to build
            //fileHandler.ProcessFourLetterWords("words-english", "spin", "spot", "LastFile");
            fileHandler.ProcessWords("words-english", startWord, endWord, resultFile);

            watch.Stop();
            Console.WriteLine($"Result file created in: {watch.ElapsedMilliseconds} ms");
            Console.ReadLine();
        }
    }
}
