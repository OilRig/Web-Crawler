using Crawler.DAL.Interfaces;
using System;


namespace Crawler.ConsoleApp
{
    public class ConsoleDataVisualizer
    {
        private IUserRepository userRepository;
        private IBoardRepository boardRepository;
        private IThemeRepository themeRepository;
        private IThreadRepository threadRepository;
        private IPostRepository postRepository;

        public ConsoleDataVisualizer(IUserRepository userRepo, IBoardRepository boardRepo, IThemeRepository themeRepo, IThreadRepository threadRepo, IPostRepository postRepo)
        {
            userRepository = userRepo;
            boardRepository = boardRepo;
            themeRepository = themeRepo;
            threadRepository = threadRepo;
            postRepository = postRepo;
        }

        public void ShowUserActivity(string userName)
        {

            var userPosts = postRepository.GetPostsByAuthorName(userName);
            Console.WriteLine("Posts of user '{0}' :", userName);
            foreach (var post in userPosts)
            {
                var thread = threadRepository.GetThreadByThreadId(post.ThreadId);
                var theme = themeRepository.GetThemeByThemeId(thread.ThemeId);
                Console.WriteLine("-Theme: {2} \n--Thread: {1} \n---Content: {0} \n", post.PostContent, thread.Subject, theme.Subject);
            }

        }

        public void ShowBoardSchema(int boardId)
        {
            var board = boardRepository.GetFullBoardById(boardId);

            Console.WriteLine("Schema of '{0}' ({1}): \n", board.Name, board.Url);
            var themes = themeRepository.GetThemesByBoardId(boardId);
            foreach (var theme in themes)
            {
                Console.WriteLine("-Theme: {0}", theme.Subject);
                var threads = threadRepository.GetBoardThreadsByThemeId(theme.ThemeId);
                foreach (var thread in threads)
                {
                    Console.WriteLine("---Thread : {0}", thread.Subject);
                }
            }
        }
    }
}
