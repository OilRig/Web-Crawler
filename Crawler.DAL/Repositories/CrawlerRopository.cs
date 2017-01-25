using Crawler.Logging.Intefaces;

namespace Crawler.DAL.Repositories
{
    public class CrawlerRopository<T> : BaseRepository<T> where T : class
    {
        private readonly IAppLogger logger;
        public CrawlerRopository(IAppLogger _logger) : base(_logger)
        {
            logger = _logger;
        }
        public CrawlerContext GetCrawlerContext()
        {
            var context = GetContext();
            return context as CrawlerContext;
        }
    }
}
