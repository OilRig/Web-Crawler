using Crawler.BL;
using Crawler.Logging.Intefaces;
using CrawlerWebApp.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Xsl;
using CE = CrawlerEngine;

namespace CrawlerWebApp.Controllers
{
    public class CrawlerController : Controller
    {
        private readonly CE.Crawler crawler;
        private readonly DbProccesor proccesor;
        private readonly IAppLogger logger;
        private CE.Board _board;


        private string StreamToFile(Stream inputStream, HttpPostedFileBase file)
        {
            var path = Server.MapPath("~/App_Data/" + (Guid.NewGuid()) + file.FileName);
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                inputStream.CopyTo(fileStream);
                fileStream.Flush();
            }
            var trans = new XslCompiledTransform();
            try
            {
                trans.Load(path);
            }
            catch (Exception)
            {
                return null;
            }
            return path;
        }

        public CrawlerController(CE.Crawler _crawler, DbProccesor _proccesor, IAppLogger _logger)
        {
            crawler = _crawler;
            proccesor = _proccesor;
            logger = _logger;
        }
        // GET: Crawler
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrawlBoard(CrawlerValidationModel crawlerModel)
        {
            try
            {
                UpdateModel(crawlerModel);

                if (ModelState.IsValid)
                {
                    _board = new CE.Board(crawlerModel.BoardName, crawlerModel.BoardUrl);

                    var path = StreamToFile(crawlerModel.XslFile.InputStream, crawlerModel.XslFile);

                    if (path == null)
                        return View("Index", crawlerModel);

                    crawler.ThemeLimitator = crawlerModel.ThemeLimitator;
                    crawler.ThreadLimitator = crawlerModel.ThreadLimitator;
                    crawler.Process(_board, path);
                    var mdlBoard = new Board()
                    {
                        Name = _board.Name,
                        Url = _board.Url
                    };

                    mdlBoard.Themes = _board.Themes
                        .Select(i => new Theme()
                        {
                            Name = i.Name,
                            Url = i.Url,
                            Threads = i.Threads
                                        .Select(j => new Thread()
                                        {
                                            Name = j.Name,
                                            Url = j.Url,
                                            Posts = j.PostsList
                                                    .Select(k => new Post()
                                                    {
                                                        AuthorName = k.AuthorName,
                                                        Content = k.Content,
                                                    })
                                                    .ToList()
                                        })
                                        .ToList()
                        })
                        .ToList();
                    TempData["board"] = _board;
                    return View(mdlBoard);
                }
                return View("Index", crawlerModel);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error crawling board with name:" + crawlerModel.BoardName);
                return View("Index", crawlerModel);
            }

        }

        [HttpPost]
        public ActionResult PushToDatabase()
        {
            try
            {
                //TryUpdateModel<CE.Board>(board);
                CE.Board updBoard = TempData["board"] as CE.Board;
               
                proccesor.PushBoardToDataBase(updBoard);

                TempData.Clear();


                return View();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Error saving board with name:");
                return View("ErrorView");
            }

        }
    }
}