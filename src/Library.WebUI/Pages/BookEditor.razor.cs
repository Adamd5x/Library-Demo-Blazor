using System.ComponentModel.DataAnnotations;
using Library.Models.Dto;
using Microsoft.AspNetCore.Components;

namespace Library.WebUI.Pages;

public partial class BookEditor
{
    [Inject]
    NavigationManager NavigationManager { get; set; }

    [Parameter]
    [Required]
    public BookDto Model { get; set; }

    [Parameter]
    public EventCallback OnValidSubmit { get; set; }

    [Parameter]
    public string ButtonTitle { get; set; }

    [Parameter]
    public bool IsReadOnly { get; set; } = true;
    
    protected void OnBackClick()
    {
        NavigationManager.NavigateTo ("/books");
    }

    protected IEnumerable<string> Statuses { get; set; } = [
        "OnTheShelf",
        "Borrowed",
        "Returned",
        "Damaged"
        ];
}
