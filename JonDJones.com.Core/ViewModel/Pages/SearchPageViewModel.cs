using EPiServer.Core;
using JonDJones.Com.Core.Pages;
using JonDJones.Com.Core;
using JonDJones.Com.Core.ViewModel.Base;
using System.Collections.Generic;
using System.Linq;

using EPiServer;
using EPiServer.Filters;
using EPiServer.ServiceLocation;

namespace JonDJones.Com.Core.ViewModel.Pages
{
    public class SearchPageViewModel : BaseViewModel<SearchPage>
    {
        private readonly IEpiServerDependencies _epiServerDependencies;

        private string _searchTerm;

        public SearchPageViewModel(SearchPage currentPage, IEpiServerDependencies epiServerDependencies)
            : base(currentPage)
        {
            _epiServerDependencies = epiServerDependencies;
        }


        public SearchPageViewModel(string searchterm, SearchPage currentPage, IEpiServerDependencies epiServerDependencies)
            : this(currentPage, epiServerDependencies)
        {
            _searchTerm = searchterm;

            if (!string.IsNullOrEmpty(_searchTerm))
            {
                var criterias = new PropertyCriteriaCollection                    
                {                                              
                    new PropertyCriteria()                            
                    {                                
                        Name = "PageName",
                        Type = PropertyDataType.String,
                        Condition = EPiServer.Filters.CompareCondition.Equal,
                        Value = _searchTerm                  
                    }                    
                };

                var repository = ServiceLocator.Current.GetInstance<IPageCriteriaQueryService>();

                SearchResults = repository.FindPagesWithCriteria(
                    PageReference.StartPage,
                    criterias);
            }

            HasSearchResult = SearchResults != null;

        }

        public bool HasSearchResult { get; set; }

        public IEnumerable<PageData> SearchResults { get; set; }

    }
}
