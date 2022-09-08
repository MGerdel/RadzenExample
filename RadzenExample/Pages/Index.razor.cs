using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using RadzenExample.Models;

namespace RadzenExample.Pages;

public class IndexBase : ComponentBase
{
    protected const int PageSizeDraft = 5;
    protected RadzenDataGrid<Model>? DataGridDraft { get; set; }
    protected List<Model> Models { get; set; } = new();
    protected int CountDraft { get; set; }
    protected int FromElementDraft { get; set; } = 1;
    protected int ToElementDraft { get; set; }
    protected int CurrentDraftPage { get; set; }

    private static IEnumerable<Model> CreateModels()
    {
        for (int i = 0; i < 40; i++)
        {
            yield return new Model
            {
                Author = Random.Shared.Next().ToString(),
                Name = Random.Shared.Next().ToString(),
                Status = Status.Draft,
                CreatedAt = DateTime.UtcNow
            };
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        Models = CreateModels().ToList();

        CountDraft = Models.Count(x => x.Status is Status.Draft);

        CurrentDraftPage = Math.Min(Math.Max((CountDraft + 4) / PageSizeDraft - 1, 0), CurrentDraftPage);

        FromElementDraft = Math.Min(CurrentDraftPage * PageSizeDraft + 1, CountDraft);
        ToElementDraft = Math.Min((CurrentDraftPage + 1) * PageSizeDraft, CountDraft);

        if (DataGridDraft != null) await InvokeAsync(() => DataGridDraft.GoToPage(CurrentDraftPage)).ConfigureAwait(false);

        StateHasChanged();
    }

    protected void OnPageChangedDraft(PagerEventArgs args)
    {
        CurrentDraftPage = args.PageIndex;
        FromElementDraft = 1 + args.Skip;
        ToElementDraft = Math.Min(FromElementDraft - 1 + args.Top, CountDraft);
    }
}