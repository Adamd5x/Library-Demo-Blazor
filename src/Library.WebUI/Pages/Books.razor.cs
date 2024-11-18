#nullable enable

using System.Text.Json;
using Library.Models.Dto;
using Library.WebUI.Abstracts;
using Library.WebUI.Components;
using Library.WebUI.Model;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Library.WebUI.Pages
{
    public partial class Books
    {
        [Inject]
        NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        IHttpRepository<BookDto> Repository { get; set; } = null!;

        [Inject]
        IHttpRepository<StatDto> StatRepository { get; set; } = null!;

        protected StatDto StatModel { get; set; } = new (0, 0, 0);

        [Inject]
        private ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        IDialogService DialogService { get; set; } = null!;

        IEnumerable<BookDto>? Model { get; set; }

        BookDto? SelectedItem = null;

        private MudTable<BookDto> table;
        private IEnumerable<BookDto> pagedData;

        protected void OnAddClick()
        {
            NavigationManager.NavigateTo ("/books/add");
        }

        private void OnEdit (int id)
        {
            NavigationManager.NavigateTo ($"/books/edit/{id}");
        }

        private void OnEditState(int id)
        {
            NavigationManager.NavigateTo ($"/book/edit/status/{id}");
        }

        private async Task<TableData<BookDto>> ServerReload(TableState state, CancellationToken token)
        {
            var stat = await StatRepository.GetAsync ("api/stat");
            Model = await Repository.GetAllAsync ($"api/book/books?sortBy={state.SortLabel}&sortOrder={state.SortDirection}&offset={state.Page}&size={state.PageSize}");
            await Task.Delay (300, token);
            return new TableData<BookDto> () {TotalItems = stat?.Total ?? 0 , Items = Model };
        }

        private async Task DeleteBookAsync (int id)
        {
            var parameters = new DialogParameters<AreYouSureDialog>
        {
            { x => x.ContentText, "Do you really want to delete these book? This process cannot be undone." },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = await DialogService.ShowAsync<AreYouSureDialog> ("Delete", parameters, options);
            var result = await dialog.Result;

            if (!result?.Canceled == true)
            {
                await DeleteAsync (id);
            }
        }

        private async Task DeleteAsync(int id)
        {
            var response = await Repository.DeleteAsync($"api/book/{id}");
            await NotifyError (response);
            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add ("The book was deleted.", Severity.Success);
                await table.ReloadServerData ();
            }
        }

        private async Task NotifyError (HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var responseContent = await response.Content.ReadAsStringAsync ();
                var details = JsonSerializer.Deserialize<ProblemDetails>(responseContent);
                if (details is not null)
                {
                    Snackbar.Add ($"{details.Detail}", Severity.Error);
                }
            }
        }
    }
}
