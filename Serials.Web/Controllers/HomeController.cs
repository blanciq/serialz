using System;
using System.Linq;
using System.Web.Mvc;
using Serials.DAL.Models;
using Serials.DAL.Repositories;
using Serials.Lib;
using Serials.Lib.Helpers;
using Serials.Web.Models;
using Serials.Web.Services;

namespace Serials.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MyPage()
        {
            var model = new MyPageViewMode();

            var serials = new string[] {"Breaking Bad", "The Big Bang Theory", "Mentalist", "Dexter", "Suits"};
            foreach (var serial in serials)
            {
                try
                {
                    var serialModel = GetSerialModelWithCache(serial);
                    if (serialModel == null)
                        continue;
                    model.Serials.Add(serial);
                    model.RecentEpisodes.AddRange(serialModel.GetAllEpisodes(true).Where(x => x.PremiereDate < DateTime.Now));
                    model.UpcommingEpisodes.AddRange(serialModel.GetAllEpisodes(true).Where(x => x.PremiereDate >= DateTime.Now));
                }
                catch
                {
                }
            }
            model.RecentEpisodes = model.RecentEpisodes.OrderBy(x => x.PremiereDate).ToList();
            model.UpcommingEpisodes = model.UpcommingEpisodes.OrderBy(x => x.PremiereDate).ToList();
            return View(model);
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            var model = GetSerialModelWithCache("The Big Bang Theory");
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string title, bool store = false, bool cache = true)
        {
            

            var model = GetSerialModelWithCache(title, cache);

            if (store)
            {
                new SerialRepository().Save(model);
            }

            return View(model);
        }

        private static Serial GetSerialModelWithCache(string title, bool useCache = true)
        {
            Serial model = null;
            if (useCache)
            {
                model = new SerialRepository().Get(title);
            }

            if (model == null)
            {
                var url = SerialsService.GetRageTvUrl(title);

                model = GetSerialViewModel(url);
            }
            return model;
        }

        private static Serial GetSerialViewModel(string detailsXml)
        {
            var doc =
                RequestHelper.FetchXmlFromUrlAsXDocument(detailsXml);
            var parser = new TvRageParser(doc);
            var model = parser.Parse();
            var checker = new Napisy24Checker();
            foreach (var episode in model.GetAllEpisodes(true))
            {
                episode.Subtitles = checker.Check(episode.SerialName, episode.Number);
            }
            return model;
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
