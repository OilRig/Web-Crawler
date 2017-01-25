using Crawler.DAL.Entities;
using Crawler.DAL.Interfaces;
using Crawler.Logging.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.DAL.Repositories
{
    public class PostRepository : CrawlerRopository<ThreadPost>, IPostRepository
    {
        private readonly IAppLogger logger;

        public PostRepository(IAppLogger _logger) : base(_logger)
        {
            logger = _logger;
        }
        public void AddList(List<ThreadPost> posts)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    foreach (var post in posts)
                    {
                        var dbPost = context.ThreadPosts.FirstOrDefault(i => i.PostContent.Equals(post.PostContent, StringComparison.InvariantCultureIgnoreCase)
                        && i.ThreadId == post.ThreadId
                        && i.UserId == post.UserId);
                        if (dbPost == null)
                        {
                            Add(post);
                        }
                        else
                        {
                            dbPost.PostContent = post.PostContent;
                            dbPost.AuthorName = post.AuthorName;
                            post.ThreadId = dbPost.ThreadId;
                        }
                    }
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Error saving posts");
                    throw;
                }
            }
        }

        public string GetPostByAuthorIdAndPostId(int authorId, int postId)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var authorPost = from p in context.ThreadPosts
                                      where p.UserId == authorId && p.PostId == postId
                                      select p.PostContent;

                    return authorPost.FirstOrDefault();
                }
                catch (Exception exception)
                {
                    var stringId = authorId.ToString();
                    logger.LogError(exception, "Error getting posts of author whith id =" + stringId);
                    throw;
                }
            }

        }

        public IEnumerable<ThreadPost> GetPostsByThreadId(int threadId)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var threadPosts = from p in context.ThreadPosts
                                      where p.ThreadId == threadId
                                      select p;

                    return threadPosts.ToList();
                }
                catch (Exception exception)
                {
                    var stringId = threadId.ToString();
                    logger.LogError(exception, "Error Getting posts of thread id = " + stringId);
                    throw;
                }
            }

        }
        public IEnumerable<ThreadPost> GetPostsByAuthorName(string userName)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var authorPosts = from p in context.ThreadPosts
                                      where p.AuthorName.ToLower().Contains(userName.ToLower())
                                      select p;

                    return authorPosts.ToList();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Error getting posts of author: " + userName);
                    throw;
                }

            }

        }
    }
}
