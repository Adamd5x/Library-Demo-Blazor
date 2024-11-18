using System.Text.Json;
using Library.Models.Dto;
using Library.WebUI.Abstracts;
using Library.WebUI.Model;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Library.WebUI.Pages
{
    public partial class BookEdit
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IHttpRepository<BookDto> Repository { get; set; } = null!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = null!;

        [Parameter]
        public string id { get; set; }

        BookDto Model { get; set; } = new ();

        protected async Task OnClickSubmitAsync()
        {

            var response = await Repository.UpdateAsync ("api/book", Model, id);

            await NotifyError (response);

            if (response.IsSuccessStatusCode)
            {
                Snackbar.Add ("The book was updated.", Severity.Success);
                NavigationManager.NavigateTo ("/books");
            }
        }

        protected async override Task OnParametersSetAsync()
        {
            var tmp = await Repository.GetAsync($"api/book/{id}");
            Model = tmp!;
        }

        private async Task NotifyError (HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var responseContent = await response.Content.ReadAsStringAsync ();
                var details = JsonSerializer.Deserialize<ProblemDetails>(responseContent);
                Snackbar.Add ($"{details.Detail}", Severity.Error);
            }
        }
    }
}
