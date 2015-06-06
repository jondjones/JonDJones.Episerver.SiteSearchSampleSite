using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing.Segments;
using JonDJones.Com.Core.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace JonDJones.com.Core.Segment
{
    public class SearchSegment : ParameterSegment
    {
        IContentLoader _contentLoader;

        public SearchSegment(string name, IContentLoader contentLoader)
            : base(name)
        {
            _contentLoader = contentLoader;
        }

        public override bool RouteDataMatch(SegmentContext context)
        {

            var segmentPair = context.GetNextValue(context.RemainingPath);
            var searchTerm = segmentPair.Next;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                return ProcessExtraSegment(context, segmentPair);
            }
            
            if (context.Defaults.ContainsKey(Name))
            {
                context.RouteData.Values[Name] = context.Defaults[Name];
                return true;
            }

            return false;
        }

        private bool ProcessExtraSegment(SegmentContext context, SegmentPair segmentPair)
        {
            var content = _contentLoader.Get<IContent>(context.RoutedContentLink);
            if (content is SearchPage)
            {
                context.RouteData.Values[Name] = context.Defaults[Name];
                context.RouteData.Values["searchterm"] = segmentPair.Next;
            }
            else
            {
                context.RouteData.Values[Name] = segmentPair.Next;
                context.RemainingPath = segmentPair.Remaining;
            }

            return true;
        }
    }
}