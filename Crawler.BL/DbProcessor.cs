using Crawler.DAL.Interfaces;
using System;
using System.Linq;
using CDALENT = Crawler.DAL.Entities;
using CE = CrawlerEngine;


namespace Crawler.BL
{
    public class DbProccesor
    {
        private IUserRepository userRepository;
        private IBoardRepository boardRepository;
        private IThemeRepository themeRepository;
        private IThreadRepository threadRepository;
        private IPostRepository postRepository;
        public DbProccesor(IUserRepository userRepo, IBoardRepository boardRepo, IThemeRepository themeRepo, IThreadRepository threadRepo, IPostRepository postRepo)
        {
            userRepository = userRepo;
            boardRepository = boardRepo;
            themeRepository = themeRepo;
            threadRepository = threadRepo;
            postRepository = postRepo;
        }

        public void PushBoardToDataBase(CE.Board board)
        {
            //Null-check!
            #region BoardMapping
            var dbBoard = new CDALENT.Board()
            {
                Name = board.Name,
                Url = board.Url,
            };

            boardRepository.InsertOrUpdate(dbBoard);
            #endregion BoardMapping

            #region ThemesMapping
            var dbThemes = board.Themes
                .Select(i => new CDALENT.BoardTheme()
                {
                    Subject = i.Name,
                    Url = i.Url,
                    BoardId = dbBoard.BoardId
                })
                .ToList();

            themeRepository.AddList(dbThemes);

            #endregion ThemesMapping

            #region ThreadsMapping
            var dbThreads = dbThemes
                .SelectMany(i => board.Themes
                    .Where(t => t.Url.Equals(i.Url, StringComparison.InvariantCultureIgnoreCase))
                    .SelectMany(t => t.Threads)
                    .Select(t => new CDALENT.BoardThread()
                    {
                        Subject = t.Name,
                        Url = t.Url,
                        ThemeId = i.ThemeId
                    }))
                .ToList();


            threadRepository.AddList(dbThreads);

            #endregion ThreadsMapping


            #region UserMapping
            var dbUsers = board.Themes
                  .SelectMany(i => i.Threads)
                  .SelectMany(i => i.PostsList)
                  .Select(i => i.AuthorName)
                  .Distinct()
                  .Select(i => new CDALENT.User()
                  {
                      Name = i.ToString()
                  })
                  .ToList();

            userRepository.AddList(dbUsers);
            #endregion UserMapping

            #region PostsMapping
            var dbPosts = dbThreads
                .SelectMany(i => board.Themes
                  .SelectMany(t => t.Threads)
                  .Where(t => t.Url == i.Url)
                  .SelectMany(p => p.PostsList)
                  .Select(p => new CDALENT.ThreadPost()
                  {
                      PostContent = p.Content,
                      ThreadId = i.ThreadId,
                      UserId = dbUsers.FirstOrDefault(u => u.Name == p.AuthorName).UserId,
                      AuthorName = dbUsers.FirstOrDefault(u => u.Name == p.AuthorName).Name
                  }))
                  .ToList();

            postRepository.AddList(dbPosts);

            #endregion PostsMapping

        }
    }
}
