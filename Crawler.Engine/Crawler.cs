using Crawler.Logging.Intefaces;
using Sgml;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace CrawlerEngine
{
    public class Crawler
    {
        private readonly IAppLogger logger;
        public Crawler(IAppLogger _logger, int themeLimitator = 1, int threadLimitator = 1)
        {
            ThemeLimitator = themeLimitator;
            ThreadLimitator = threadLimitator;
            logger = _logger;
        }

        private string MainBoardPage { get; set; }

        private XmlDocument Document { get; set; }

        private BoardTheme BoardTheme { get; set; }
        private BoardThread BoardThread { get; set; }
        private ThreadPost ThreadPost { get; set; }
        private ThreadUser ThreadUser { get; set; }
        private Encoding CurrentEncoding { get; set; }
        public int ThemeLimitator { get; set; }
        public int ThreadLimitator { get; set; }

        /// <summary>
        /// adds board instance to list
        /// </summary>
        /// <param name="board"></param>
        /// <summary>
        /// Gets html content
        /// </summary>
        /// <param name="url"></param>
        /// <returns>string HTML</returns>
        public string GetPageContent(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var encoding = response.CharacterSet;
                CurrentEncoding = Encoding.GetEncoding(encoding);

                if (response.ContentLength == 0) throw new ArgumentException("Response is empry!");
                using (var responseReader = new StreamReader(response.GetResponseStream()))
                {
                    try
                    {
                        var page = responseReader.ReadToEnd();
                        return page;
                    }
                    catch (Exception exception)
                    {
                        logger.LogError(exception, "Error reading response stream!");
                        throw;
                    }
                }
            }

        }

        /// <summary>
        /// Well-forms (parses) HTML to XHTML; returns Well-Formed document
        /// </summary>
        /// <param name="xpage"></param>
        /// <returns></returns>
        public XmlDocument WellForm(string xpage)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(xpage);
                    writer.Flush();
                    stream.Seek(0, SeekOrigin.Begin);

                    // setup SgmlReader
                    using (var sgmlReader = new SgmlReader())
                    {
                        sgmlReader.DocType = "HTML";
                        sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
                        sgmlReader.CaseFolding = CaseFolding.ToLower;
                        if (stream.CanRead && stream.Length > 0)
                        {
                            sgmlReader.InputStream = new StreamReader(stream);
                        }
                        else
                        {
                            throw new NullReferenceException();
                        }
                        // create document
                        var xmlDoc = new XmlDocument();
                        xmlDoc.PreserveWhitespace = true;
                        xmlDoc.XmlResolver = null;
                        try
                        {
                            xmlDoc.Load(sgmlReader);
                            return xmlDoc;
                        }
                        catch (Exception exception)
                        {
                            logger.LogError(exception, "Error loading XML Document");
                            throw;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Activate in-lib XSLT
        /// </summary>
        /// <param name="document" type="XmlDocument"></param>
        /// <param name="xslFilePath" type="XmlDocument"></param>
        /// <returns>Transformed document</returns>
        private XmlDocument XslTransform(XmlDocument document, string xslFilePath)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            if (xslFilePath == null) throw new ArgumentNullException(nameof(xslFilePath));

            var trans = new XslCompiledTransform();
            try
            {
                trans.Load(xslFilePath);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Couldn't load a slylesheet!");
                throw;
            }
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                using (var xmlWriter = new XmlTextWriter(stringWriter))
                {
                    try
                    {
                        trans.Transform(document, xmlWriter);
                    }
                    catch (Exception exception)
                    {
                        logger.LogError(exception, exception.Message);
                        throw;
                    }
                }
            }
            var transformedDoc = new XmlDocument();
            transformedDoc.InnerXml = sb.ToString();
            return transformedDoc;
        }

        /// <summary>
        /// Gets themes names and urls from main forum page
        /// </summary>
        /// <param name="board">Board created with ctor</param>
        /// <param name="themeSearcherXsl">Xsl for main page</param>
        public void CreateBoardThemesCollection(Board board, string xslFile)

        {
            MainBoardPage = GetPageContent(board.Url);
            Document = WellForm(MainBoardPage);
            Document = XslTransform(Document, xslFile);
            var counter = 0;
            using (var stream = new MemoryStream())
            {

                using (var streamWriter = new StreamWriter(stream,CurrentEncoding))
                {
                    try
                    {
                        streamWriter.Write(Document.InnerXml);
                        streamWriter.Flush();
                        stream.Seek(0, SeekOrigin.Begin);

                    }
                    catch (Exception exception)
                    {
                        logger.LogError(exception, "Error on writing stream!");
                        throw;
                    }
                    using (var reader = new XmlTextReader(stream))
                    {
                        try
                        {
                            while (reader.Read() && counter < ThemeLimitator)
                            {
                                if (!reader.HasAttributes) continue;
                                BoardTheme = new BoardTheme();
                                BoardTheme.Name = reader.GetAttribute("themeSubject");
                                BoardTheme.Url = reader.GetAttribute("themeUrl");
                                board.Themes.Add(BoardTheme);
                                counter++;
                            }
                        }
                        catch (Exception exception)
                        {
                            logger.LogError(exception, exception.Message);
                        }
                    }
                }
            }

        }

        public void CreateThemeThreadsCollection(Board board, string xslFile)
        {

            foreach (var theme in board.Themes)
            {
                var counter = 0;
                var page = GetPageContent(theme.Url);
                Document = WellForm(page);
                Document = XslTransform(Document, xslFile);

                using (var stream = new MemoryStream())
                {

                    using (var streamWriter = new StreamWriter(stream, CurrentEncoding))
                    {
                        try
                        {
                            streamWriter.Write(Document.InnerXml);
                            streamWriter.Flush();
                            stream.Seek(0, SeekOrigin.Begin);

                        }
                        catch (Exception exception)
                        {
                            logger.LogError(exception, "Error on writing stream!");
                            throw;
                        }
                        using (var reader = new XmlTextReader(stream))
                        {
                            try
                            {
                                while (reader.Read() && counter < ThreadLimitator)
                                {
                                    if (!reader.HasAttributes) continue;
                                    BoardThread = new BoardThread();
                                    BoardThread.Name = reader.GetAttribute("threadSubject");
                                    BoardThread.Url = reader.GetAttribute("threadUrl");
                                    theme.Threads.Add(BoardThread);

                                    using (var threadStream = new MemoryStream())
                                    {
                                        var threadPage = GetPageContent(BoardThread.Url);
                                        Document = WellForm(threadPage);
                                        Document = XslTransform(Document, xslFile);
                                        using (var threadStreamWriter = new StreamWriter(threadStream, CurrentEncoding))
                                        {
                                            try
                                            {
                                                threadStreamWriter.Write(Document.InnerXml);
                                                threadStreamWriter.Flush();
                                                threadStream.Seek(0, SeekOrigin.Begin);

                                            }
                                            catch (Exception exception)
                                            {
                                                logger.LogError(exception, "Error on writing stream!");
                                                throw;
                                            }
                                            using (var threadReader = new XmlTextReader(threadStream))
                                            {
                                                while (threadReader.Read())
                                                {
                                                    if (!threadReader.HasAttributes) continue;
                                                        ThreadPost = new ThreadPost();
                                                        ThreadPost.Content = threadReader.GetAttribute("postContent");
                                                        ThreadPost.AuthorName = threadReader.GetAttribute("postAuthor");
                                                        BoardThread.PostsList.Add(ThreadPost);
                                                }
                                            }
                                        }
                                    }
                                    counter++;
                                }
                            }
                            catch (Exception exception)
                            {
                                logger.LogError(exception, "Error reading stream!");
                            }
                        }
                    }
                }

            }
        }
        /// <summary>
        /// processes board creates board forums with urls
        /// </summary>
        /// <param name="board"></param>
        /// <param name="xslPath1"></param>
        /// <param name="xslPath2"></param>
        /// <returns>BoardForum</returns>
        public void Process(Board board, string xslFile)
        {
            CreateBoardThemesCollection(board, xslFile);
            CreateThemeThreadsCollection(board, xslFile);
        }
    }
}
