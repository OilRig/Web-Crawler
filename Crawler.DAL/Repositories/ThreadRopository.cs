using Crawler.DAL.Entities;
using Crawler.DAL.Interfaces;
using Crawler.Logging.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.DAL.Repositories
{
    public class ThreadRopository : CrawlerRopository<BoardThread>, IThreadRepository
    {
        private readonly IAppLogger logger;

        public ThreadRopository(IAppLogger _logger) : base(_logger)
        {
            logger = _logger;
        }
        public void AddList(List<BoardThread> threads)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    foreach (var thread in threads)
                    {

                        var dbThread = context.BoardThreads.FirstOrDefault(i => i.Url.Equals(thread.Url, StringComparison.InvariantCultureIgnoreCase));
                        if (dbThread == null) Add(thread);
                        else
                        {
                            dbThread.Subject = thread.Subject;
                            thread.ThemeId = dbThread.ThemeId;
                            thread.ThreadId = dbThread.ThreadId;
                        }
                    }
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Error saving threads");
                    throw;
                }

            }
        }

        public IEnumerable<BoardThread> GetBoardThreadsByThemeId(int themeId)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var dbThreads = from t in context.BoardThreads
                                    where t.ThemeId == themeId
                                    select t;

                    return dbThreads.ToList();
                }
                catch (Exception exception)
                {
                    var stringId = themeId.ToString();
                    logger.LogError(exception, "Error getting threads from theme with id = " + stringId);
                    throw;
                }

            }
        }

        public BoardThread GetThreadByThreadId(int threadId)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var thread = context.BoardThreads.First(i => i.ThreadId == threadId);

                    return thread;
                }
                catch (Exception exception)
                {
                    var stringId = threadId.ToString();
                    logger.LogError(exception, "Error recognizing thread with id = " + stringId);
                    throw;
                }

            }
        }
    }
}
