using Ecommerce_Models;
using EcommerceBlazorWebApp_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using System;

namespace EcommerceBlazorWebApp_Client.Pages.Authentication
{
    public partial class Register
    {
      
    private SignUpRequestDTO SignUpRequest = new();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthenticationService _authSerivce { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;

            //Blazor web assembly runs on the single threaded environment that is why any blocking will results in deadlock of the application so Getawaiter().Getresult() will block the call
            var result = await _authSerivce.RegisterUser(SignUpRequest);
            if (result.IsRegisterationSuccessful)
            {
                //regiration is successful
                _navigationManager.NavigateTo("/login");
            }
            else
            {
                //failure
                Errors = result.Errors;
                ShowRegistrationErrors = true;

            }
            IsProcessing = false;
        }
    }
}

