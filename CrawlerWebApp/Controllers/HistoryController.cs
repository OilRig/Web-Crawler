using Crawler.DAL.Interfaces;
using Crawler.Logging.Intefaces;
using CrawlerWebApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CrawlerWebApp.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IBoardRepository boardRepository;
        private readonly IUserRepository userRepository;
        private readonly IPostRepository postRepository;
        private readonly IThreadRepository threadRepository;
        private readonly IAppLogger logger;

        public HistoryController(IBoardRepository _boardRepository, IUserRepository _userRepository, IPostRepository _postRepository, IThreadRepository _threadRepository, IAppLogger _logger)
        {
            boardRepository = _boardRepository;
            userRepository = _userRepository;
            logger = _logger;
            postRepository = _postRepository;
            threadRepository = _threadRepository;
        }
        // GET: History
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SchemaByBoardName(string boardName)
        {
            try
            {
                var schema = boardRepository
                .GetFullBoardByName(boardName)
                .GroupBy(i => i.BoardName)
                .Select(i => new DbBoard
                {
                    Name = i.Key,
                    Themes = i
                        .GroupBy(j => j.ThemeUrl)
                        .Select(j => new DbTheme
                        {
                            Name = j.First().ThemeName,
                            Threads = j
                                .GroupBy(t => t.ThreadUrl)
                                .Select(t => new DbThread
                                {
                                    Name = t.First().ThreadName,
                                    Url = t.Key
                                }).ToList()
                        }).ToList()
                }).Single();

                return View(schema);


            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error getting board with name: " + boardName);
                return RedirectToAction("DbGetDataError", new { parameter = boardName, fromBoard = true, fromUser = false });
            }

        }
        [HttpPost]
        public ActionResult UserActivityInfo(string userName)
        {
            try
            {
                var activity = userRepository
                .GetUserInfoByName(userName)
                .OrderBy(i => i.Name)
                .GroupBy(i => i.Name)
                .Select(i => new DbUser
                {
                    Name = i.Key,
                    Posts = i
                        .Select(j => new DbPost
                        {
                            Content = j.PostContent,
                            ThemeName = j.ThemeName,
                            ThreadName = j.ThreadName,
                            PostId = j.PostId,
                            UserId=j.UserId
                        }).ToList()
                }).ToList();

                if (activity.Count != 0)
                {
                    ViewBag.UserName = userName;
                    return View(activity);
                }
                else
                {
                    return RedirectToAction("DbGetDataError", new { parameter = userName, fromBoard = false, fromUser = true });
                }
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error getting user with name: " + userName);
                return null;
            }

        }
        public ActionResult DbGetDataError(string parameter, bool fromBoard, bool fromUser)
        {
            var mdlError = new DbErrorModel();
            if (fromBoard)
            {
                mdlError.ErrorMessage = "There is no board named '" + parameter + "' in database";
            }
            else if (fromUser)
            {
                mdlError.ErrorMessage = "There is no user whose name contains '" + parameter + "' in database";
            }

            return View(mdlError);
        }

        [HttpGet]
        public JsonResult ViewPost(int userId,int postId)
        {
            var uploadedPost = new PostJSON();
            var post = postRepository.GetPostByAuthorIdAndPostId(userId, postId);
            var user = userRepository.GetUserById(userId);
            uploadedPost.Content = post;
            uploadedPost.Author = user.Name;
            return Json(uploadedPost,JsonRequestBehavior.AllowGet);
        }
    }
}