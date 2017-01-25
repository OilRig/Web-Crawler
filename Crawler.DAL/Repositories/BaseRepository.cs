using Crawler.DAL.Interfaces;
using Crawler.Logging.Intefaces;
using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace Crawler.DAL.Repositories
{
    public class BaseRepository<TCrawlerEntity> : IRepository<TCrawlerEntity> where TCrawlerEntity : class
    {
        private readonly IAppLogger logger;

        public BaseRepository(IAppLogger _logger)
        {
            logger = _logger;
        }

        public void Add(TCrawlerEntity entity)
        {
            using (var context = GetContext())
            {
                try
                {
                    context.Set<TCrawlerEntity>().Add(entity);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "'Error adding entity");
                    throw;
                }

            }
        }
        public void Remove(TCrawlerEntity entity)
        {
            using (var context = GetContext())
            {
                try
                {
                    context.Set<TCrawlerEntity>().Remove(entity);
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "'Error removing entity");
                    throw;
                }

            }
        }

        public IEnumerable GetAll(TCrawlerEntity entity)
        {
            using (var context = GetContext())
            {
                try
                {
                    var records = context.Set<TCrawlerEntity>().ToList();
                    return records;
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "'Error loading all entities");
                    throw;
                }

            }

        }
        public object Find(object identity)
        {

            using (var context = GetContext())
            {
                try
                {
                    return context.Set<TCrawlerEntity>().Find(identity);
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "'Entity not found");
                    throw;
                }

            }
        }

        protected DbContext GetContext(string connectionString = null)
        {
            return new CrawlerContext();
        }
    }
}
