using Crawler.DAL.Interfaces;
using Crawler.DAL.Repositories;
using Crawler.Logging.Intefaces;
using Crawler.Logging.Loggers;
using Ninject.Modules;




namespace Crawler.Ninject
{
    public class NinjectConfigure : NinjectModule
    {
        public override void Load()
        {
            //Repositories
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IBoardRepository>().To<BoardRepository>();
            Bind<IThemeRepository>().To<ThemeRepository>();
            Bind<IThreadRepository>().To<ThreadRopository>();
            Bind<IPostRepository>().To<PostRepository>();

            //Loggers
            Bind<IAppLogger>().To<NlogLogger>();
        }
    }
}
