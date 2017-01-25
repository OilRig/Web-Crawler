using Crawler.DAL.Entities;
using System.Collections.Generic;

namespace Crawler.DAL.Interfaces
{
    public interface IThreadRepository
    {
        IEnumerable<BoardThread> GetBoardThreadsByThemeId(int themeId);

        void AddList(List<BoardThread> themes);
        BoardThread GetThreadByThreadId(int threadId);
    }
}
