using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Microsoft.AspNetCore.Components;

namespace Apb.ApbApp.Pages;

[UsedImplicitly]
public partial class FacilitySearch
{
    [Inject]
    private IFacilityRepository Repository { get; set; } = default!;

    private string? SearchText { get; set; }
    private SearchState State { get; set; }

    private enum SearchState
    {
        SearchTextEmpty,
        SearchTextShort,
        SearchTextReady,
    }

    private List<FacilityView> SearchResults { get; set; } = new();

    [UsedImplicitly]
    private async Task FindFacilityAsync(ChangeEventArgs args)
    {
        SearchText = args.Value?.ToString();

        if (string.IsNullOrEmpty(SearchText))
        {
            State = SearchState.SearchTextEmpty;
            SearchResults = new List<FacilityView>();
            return;
        }

        if (SearchText.Length < 3)
        {
            State = SearchState.SearchTextShort;
            SearchResults = new List<FacilityView>();
            return;
        }

        State = SearchState.SearchTextReady;
        SearchResults = await Repository.SearchFacilitiesById(SearchText);
    }
}
