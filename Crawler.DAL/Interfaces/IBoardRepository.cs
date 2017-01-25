using Crawler.DAL.BusinessEntities;
using Crawler.DAL.Entities;
using System.Collections.Generic;
namespace Crawler.DAL.Interfaces
{
    public interface IBoardRepository : IRepository<Board>
    {
        IEnumerable<Board> GetAllBoards();
        void InsertOrUpdate(Board board);
        Board GetFullBoardById(int boardId);
        IList<BoardFoundRow> GetFullBoardByName(string boardName);

    }
}
