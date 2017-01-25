using Crawler.DAL.Entities;
using System.Collections.Generic;

namespace Crawler.DAL.Interfaces
{
    public interface IPostRepository
    {
        void AddList(List<ThreadPost> posts);

        string GetPostByAuthorIdAndPostId(int authorId, int postId);
        IEnumerable<ThreadPost> GetPostsByAuthorName(string userName);
        IEnumerable<ThreadPost> GetPostsByThreadId(int threadId);
    }
}
