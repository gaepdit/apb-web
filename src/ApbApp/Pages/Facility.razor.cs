using Apb.Domain.Facilities.FacilityId;
using Apb.Domain.Facilities.Repositories;
using Apb.Domain.Facilities.Resources;
using Microsoft.AspNetCore.Components;

namespace Apb.ApbApp.Pages;

[UsedImplicitly]
public partial class Facility
{
    [Inject]
    private IFacilityRepository Repository { get; set; } = default!;

    // Facility ID parameter view
    [Parameter]
    public string? FacilityId { get; set; }

    private FacilityView? FacilityView { get; set; }
    private bool ShowSingleFacilityView { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrEmpty(FacilityId))
        {
            ShowSingleFacilityView = false;
            FacilityView = null;
        }
        else
        {
            ShowSingleFacilityView = true;
            FacilityView = await Repository.GetFacilityAsync(FacilityId);
        }

        await base.OnParametersSetAsync();
    }

    // Search form view
    private string? SearchText { get; set; }
    private SearchState State { get; set; }

    private enum SearchState
    {
        SearchTextEmpty,
        FacilityIdNotValid,
        FacilityNotFound,
        FacilityFound,
    }

    [UsedImplicitly]
    private async Task FindFacilityAsync(ChangeEventArgs args)
    {
        SearchText = args.Value?.ToString();

        if (string.IsNullOrEmpty(SearchText))
        {
            FacilityView = null;
            State = SearchState.SearchTextEmpty;
            return;
        }

        if (!ApbFacilityId.IsValidFacilityIdFormat(SearchText))
        {
            FacilityView = null;
            State = SearchState.FacilityIdNotValid;
            return;
        }

        var id = new ApbFacilityId(SearchText);

        FacilityView = await Repository.GetFacilityAsync(id);
        State = FacilityView is null ? SearchState.FacilityNotFound : SearchState.FacilityFound;
    }
}
