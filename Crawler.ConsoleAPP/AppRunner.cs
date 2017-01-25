using Crawler.BL;
using Crawler.ConsoleApp;
using System;
using CE = CrawlerEngine;
namespace CrawltestingFinal
{
    public class AppRunner
    {
        private readonly CE.Crawler _crawler;
        private readonly DbProccesor _proccesor;
        private readonly ConsoleDataVisualizer _visualizer;

        public AppRunner(CE.Crawler crawler, DbProccesor proccesor, ConsoleDataVisualizer visualizer)
        {
            _crawler = crawler;
            _proccesor = proccesor;
            _visualizer = visualizer;
        }

        public void Run()
        {


            var path = @"D:\filesdebug\Staroe_tv_complete";

            Console.WriteLine("Start Crawling? Y/N");


            var answer = Console.ReadLine();

            if (answer == "y" || answer == "Y")
            {
                Console.WriteLine("Enter board Url:");
                var url = Console.ReadLine();

                Console.WriteLine("Enter board name:");
                var name = Console.ReadLine();

                var board = new CE.Board(name, url);

                Console.WriteLine("How many themes?");
                var themes = Convert.ToInt32(Console.ReadLine());
                _crawler.ThemeLimitator = themes;

                Console.WriteLine("How many threads in each theme?");
                var threads = Convert.ToInt32(Console.ReadLine());
                _crawler.ThreadLimitator = threads;

                Console.WriteLine("Processing...");

                _crawler.Process(board, path);

                Console.WriteLine("Crawling complete. Update database? Y/N");
                var answerForDb = Console.ReadLine();

                if (answerForDb == "y" || answerForDb == "Y")
                {
                    Console.WriteLine("Updating...");
                    _proccesor.PushBoardToDataBase(board);

                    Console.WriteLine("Done! Check DB? Y/N");
                    var answerForCheck = Console.ReadLine();

                    if (answerForCheck == "y" || answerForCheck == "Y")
                    {
                        Console.WriteLine("Enter user name:");
                        var predicate = Console.ReadLine();
                        _visualizer.ShowUserActivity(predicate);
                        Console.ReadKey();

                    }
                    else if (answerForCheck == "n" || answerForCheck == "N")
                    {
                        Console.WriteLine("Press any key to exit");
                        Console.ReadKey();
                    }
                }
                else if (answerForDb == "n" || answerForDb == "N")
                {
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                }
            }
            else if (answer == "n" || answer == "N")
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();

            }




        }

    }
}
