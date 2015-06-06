using System.Web.Mvc;
using JonDJones.Com.Core.Pages;

using EPiServer.Core;
using JonDJones.Com.Core.ViewModel;
using JonDJones.Com.Controllers.Base;
using JonDJones.Com.Core.ViewModel.Pages;

namespace JonDJones.Com.Controllers.Pages
{
    public class SearchPageController : BasePageController<SearchPage>
    {
        public ActionResult Index(SearchPage currentPage, string searchterm)
        {
            return View("Index", new SearchPageViewModel(searchterm, currentPage, EpiServerDependencies));
        }
    }
}