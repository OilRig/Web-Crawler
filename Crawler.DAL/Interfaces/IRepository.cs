namespace Crawler.DAL.Interfaces
{
    public interface IRepository<TCrawlerEntity> where TCrawlerEntity : class
    {
        void Add(TCrawlerEntity entity);

        void Remove(TCrawlerEntity entity);

        object Find(object identity);
    }
}
