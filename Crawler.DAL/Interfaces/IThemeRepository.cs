using Crawler.DAL.Entities;
using System.Collections.Generic;

namespace Crawler.DAL.Interfaces
{
    public interface IThemeRepository
    {
        IEnumerable<BoardTheme> GetThemesByBoardId(int boardId);

        void AddList(List<BoardTheme> themes);

        BoardTheme GetThemeByThemeId(int themeId);
    }
}
