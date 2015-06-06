using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using JonDJones.com.Core.Blocks;
using JonDJones.com.Core.Entities;
using JonDJones.Com.Core;
using JonDJones.Com.Core.Pages;
using JonDJones.Com.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonDJones.com.Core.ViewModel.Blocks
{
    public class TopSearchViewModel : BlockViewModel<TopSearchBlock>
    {
        public TopSearchViewModel(TopSearchBlock currentBlock, IEpiServerDependencies epiServerDependencies)
            : base(currentBlock, epiServerDependencies)
        {
        }

        public SearchPage Link
        {
            get
            {
                return _epiServerDependencies.ContentRepository
                    .GetChildren<SearchPage>(_epiServerDependencies.ContextResolver.StartPage)
                    .FirstOrDefault();
            }
        }
    }
}
