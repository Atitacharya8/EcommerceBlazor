using EcommerceBlazorWebApp_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;

namespace EcommerceBlazorWebApp_Client.Pages.Authentication
{
    public partial class Logout
    {

        [Inject]
        public IAuthenticationService _authSerivce { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await _authSerivce.Logout();
            _navigationManager.NavigateTo("/");

        }


    }
}
