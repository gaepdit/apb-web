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

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    [Parameter]
    public string? Id { get; set; }

    private bool ValidFacilityId { get; set; }

    private ApbFacilityId? FacilityId { get; set; }

    private FacilityView? FacilityView { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrEmpty(Id))
        {
            NavigationManager.NavigateTo("/facility-search");
            return;
        }

        if (ApbFacilityId.IsValidFacilityIdFormat(Id))
        {
            ValidFacilityId = true;
            FacilityId = new ApbFacilityId(Id);
            FacilityView = await Repository.GetFacilityAsync(FacilityId);
        }
    }
}
