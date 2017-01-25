using Crawler.DAL.BusinessEntities;
using Crawler.DAL.Entities;
using Crawler.DAL.Interfaces;
using Crawler.Logging.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.DAL.Repositories
{
    public class BoardRepository : CrawlerRopository<Board>, IBoardRepository
    {

        private readonly IAppLogger logger;

        public BoardRepository(IAppLogger _logger) : base(_logger)
        {
            logger = _logger;
        }

        public IEnumerable<Board> GetAllBoards()
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    return context.Set<Board>().ToList();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Error loading all boards");
                    throw;
                }

            }
        }

        public void InsertOrUpdate(Board board)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var dbBoard = context.Boards.FirstOrDefault(i => i.Url.Equals(board.Url, StringComparison.InvariantCultureIgnoreCase));

                    if (dbBoard == null)
                    {
                        Add(board);
                    }
                    else
                    {
                        dbBoard.Name = board.Name;
                        board.BoardId = dbBoard.BoardId;
                    }
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Error inserting board to db");
                    throw;
                }
            }
        }

        public Board GetFullBoardById(int boardId)
        {
            try
            {
                using (var context = GetCrawlerContext())
                {

                    var board = context.Boards.FirstOrDefault(i => i.BoardId == boardId);

                    return board;
                }
            }
            catch (Exception exception)
            {
                var stringId = boardId.ToString();
                logger.LogError(exception, " Error loading  board:" + stringId);
                throw;
            }
        }

        public IList<BoardFoundRow> GetFullBoardByName(string boardName)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var query = from b in context.Boards
                                join t in context.BoardThemes on b.BoardId equals t.BoardId
                                join tr in context.BoardThreads on t.ThemeId equals tr.ThemeId
                                where b.Name.Equals(boardName)
                                select new BoardFoundRow
                                {
                                    BoardName = b.Name,
                                    ThemeName = t.Subject,
                                    ThreadName = tr.Subject,
                                    ThemeUrl = t.Url,
                                    ThreadUrl = tr.Url
                                };

                    return query.ToList();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Error  loading  board :" + boardName);
                    throw;
                }

            }
        }


    }
}
