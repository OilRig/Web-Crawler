using Crawler.DAL.Entities;
using Crawler.DAL.Interfaces;
using Crawler.Logging.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.DAL.Repositories
{
    public class ThemeRepository : CrawlerRopository<BoardTheme>, IThemeRepository
    {
        private readonly IAppLogger logger;

        public ThemeRepository(IAppLogger _logger) : base(_logger)
        {
            logger = _logger;
        }
        public void AddList(List<BoardTheme> themes)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    foreach (var theme in themes)
                    {
                        var dbTheme = context.BoardThemes.FirstOrDefault(i => i.Url.Equals(theme.Url, StringComparison.InvariantCultureIgnoreCase));
                        if (dbTheme == null)
                        {
                            Add(theme);
                        }
                        else
                        {
                            dbTheme.Subject = theme.Subject;
                            theme.BoardId = dbTheme.BoardId;
                            theme.ThemeId = dbTheme.ThemeId;
                        }
                    }
                    context.SaveChanges();
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Error saving themes");
                    throw;
                }

            }
        }

        public IEnumerable<BoardTheme> GetThemesByBoardId(int boardId)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var boardThemes = from t in context.BoardThemes
                                      where t.BoardId == boardId
                                      select t;

                    return boardThemes.ToList();
                }
                catch (Exception exception)
                {
                    var stringId = boardId.ToString();
                    logger.LogError(exception, "Error getting themes of board with id = " + stringId);
                    throw;
                }

            }

        }
        public BoardTheme GetThemeByThemeId(int themeId)
        {
            using (var context = GetCrawlerContext())
            {
                try
                {
                    var theme = context.BoardThemes.First(i => i.ThemeId == themeId);

                    return theme;
                }
                catch (Exception exception)
                {
                    var stringId = themeId.ToString();
                    logger.LogError(exception, "Error getting theme with id = " + stringId);
                    throw;
                }

            }
        }
    }
}
