using System.Text.Json;
using Library.Models.Dto;
using Library.WebUI.Abstracts;
using Library.WebUI.Model;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Library.WebUI.Pages;

public partial class BookCreate
{
    [Inject]
    NavigationManager NavigationManager { get; set; }

    [Inject]
    IHttpRepository<BookDto> Repository { get; set; } = null!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    IDialogService DialogService { get; set; } = null!;

    BookDto Model { get; set; } = new ()
    {
        Isbn = string.Empty,
        Title = string.Empty,
        Author = string.Empty
    };

    protected async Task OnClickSubmitAsync()
    {
        var response = await Repository.CreateAsync ("api/book", Model);

        await NotifyError (response);

        if (response.IsSuccessStatusCode)
        {
            Snackbar.Add ("The book was created.", Severity.Success);
            NavigationManager.NavigateTo ("/books");
        }
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
