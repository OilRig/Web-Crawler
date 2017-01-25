using Crawler.DAL.BusinessEntities;
using Crawler.DAL.Entities;
using Crawler.DAL.Interfaces;
using Crawler.Logging.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.DAL.Repositories
{
    public class UserRepository : CrawlerRopository<User>, IUserRepository
    {
        private readonly IAppLogger logger;

        public UserRepository(IAppLogger _logger) : base(_logger)
        {
            logger = _logger;
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var context = GetCrawlerContext())
            {
                return context.Set<User>().ToList();
            }
        }

        public void AddList(List<User> users)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    foreach (var user in users)
                    {

                        var dbUser = context.Users.FirstOrDefault(i => i.Name.Equals(user.Name, StringComparison.InvariantCultureIgnoreCase));

                        if (dbUser == null)
                        {
                            Add(user);
                        }
                        else
                        {
                            user.UserId = dbUser.UserId;
                        }
                    }
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "'Error saving users");
                    throw;
                }

            }
        }
        public User GetUserById(int id)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var user = context.Users.FirstOrDefault(i => i.UserId == id);

                    return user;
                }
                catch (Exception exception)
                {
                    var stringId = id.ToString();
                    logger.LogError(exception, "'Error getting user with id = " + stringId);
                    throw;
                }

            }
        }
        public IList<UserFoundRow> GetUserInfoByName(string userName)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var query = from u in context.Users
                                join p in context.ThreadPosts on u.Name equals p.AuthorName
                                join tr in context.BoardThreads on p.ThreadId equals tr.ThreadId
                                join t in context.BoardThemes on tr.ThemeId equals t.ThemeId
                                where u.Name.ToLower().Contains(userName.ToLower())
                                select (new UserFoundRow
                                {
                                    Name = u.Name,
                                    PostContent = p.PostContent,
                                    PostId = p.PostId,
                                    UserId = p.UserId,
                                    ThemeName = t.Subject,
                                    ThreadName = tr.Subject
                                });
                    return query.ToList();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "'Error getting user with name contains = " + userName);
                    throw;
                }

            }
        }
    }
}
