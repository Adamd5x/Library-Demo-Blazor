using System.Text.Json;
using Library.Models.Dto;
using Library.WebUI.Abstracts;
using Library.WebUI.Model;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Library.WebUI.Pages
{
    public partial class BookStatus
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        IHttpRepository<BookDto> Repository { get; set; } = null!;

        [Parameter]
        public string id { get; set; }

        BookDto Model { get; set; } = new ();

        protected async Task OnClickSubmitAsync ()
        {
            var response = await Repository.SetStateAsync ($"api/book/chstate/{id}/{Model.State}", Model.State);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) 
            { 
                var responseContent = await response.Content.ReadAsStringAsync ();
                var details = JsonSerializer.Deserialize<ProblemDetails>(responseContent);
                Snackbar.Add ($"{details.Detail}", Severity.Error);
            }
            else
            {
                NavigationManager.NavigateTo ("/books");
                Snackbar.Add ("The state was updated.", Severity.Success);

            }
        }

        protected async override Task OnParametersSetAsync ()
        {
            var tmp = await Repository.GetAsync($"api/book/{id}");
            Model = tmp!;
        }
    }
}
