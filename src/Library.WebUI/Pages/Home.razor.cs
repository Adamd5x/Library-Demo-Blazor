using Library.Models.Dto;
using Library.WebUI.Abstracts;
using Microsoft.AspNetCore.Components;

namespace Library.WebUI.Pages;

public partial class Home
{
    private string currentTime;

    [Inject]
    IHttpRepository<StatDto> Repository { get; set; }

    protected StatDto Model { get; set; } = new (0,0,0);

    protected override void OnInitialized ()
    {
        currentTime = DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss");

        var timer = new System.Timers.Timer(1000); // 1000 ms = 1 sekunda
        timer.Elapsed += UpdateTime;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    protected override async Task OnInitializedAsync ()
    {
        await LoadData ();
    }

    private void UpdateTime (object sender, System.Timers.ElapsedEventArgs e)
    {
        InvokeAsync (() =>
        {
            currentTime = DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss");
            StateHasChanged (); 
        });
    }

    private async Task LoadData()
    {
        var result = await Repository.GetAsync ("api/stat");
        Model = result;
    }

}
