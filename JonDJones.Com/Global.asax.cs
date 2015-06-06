using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using System.Web.Http;
using EPiServer.Web.Routing;
using System.Collections.Generic;
using JonDJones.com.Core.Segment;
using EPiServer.Core;
using EPiServer.Web.Routing.Segments;
using EPiServer;
using EPiServer.ServiceLocation;

namespace JonDJones.Com
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Insert(0, new CustomViewEngine());

            AreaRegistration.RegisterAllAreas();
        }


        protected override void RegisterRoutes(RouteCollection routes)
        {
            base.RegisterRoutes(routes);

            IContentLoader contentLoader;
            ServiceLocator.Current.TryGetExistingInstance(out contentLoader);

            IUrlSegmentRouter segmentRouter;
            ServiceLocator.Current.TryGetExistingInstance(out segmentRouter);

            if (contentLoader != null && segmentRouter != null)
            {
                segmentRouter.RootResolver = sd => sd.StartPage;
                var parameters = new MapContentRouteParameters
                {
                    UrlSegmentRouter = segmentRouter,
                    BasePathResolver = EPiServer.Web.Routing.RouteCollectionExtensions.ResolveBasePath,
                    Direction = SupportedDirection.Both
                };
                var segment = new SearchSegment("searchterm", contentLoader);
                var segmentMappings = new Dictionary<string, ISegment> { { "searchterm", segment } };
                parameters.SegmentMappings = segmentMappings;

                routes.MapContentRoute(
                    name: "optionalaction",
                    url: "{language}/{node}/{partial}/{action}/{searchterm}",
                    defaults: new { action = "index" },
                    parameters: parameters);
            }
        }
    }
}