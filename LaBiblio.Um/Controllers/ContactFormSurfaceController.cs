using LaBiblio.Um.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace LaBiblio.Um.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        public ContactFormSurfaceController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        { }

        [HttpPost]
        public IActionResult Submit(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            TempData["success"] = true;
            var cs = Services.ContentService;
            if (cs != null)
            {
                IContent comment = cs.Create(model.Subject, CurrentPage.Id, "Comment"); // Put in the id of any page you like, to show the submissions there instead.
                comment.SetValue("username", model.Name);
                comment.SetValue("email", model.Email);
                comment.SetValue("subject", model.Subject);
                comment.SetValue("message", model.Message);
                cs.Save(comment);
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}
