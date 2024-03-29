﻿using EcommerceBlazorWebApp_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace EcommerceBlazorWebApp_Client.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [CascadingParameter]
        public Task<AuthenticationState> _authState { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        bool notAuthorized { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            var authState = await _authState;

            if(authState.User?.Identity is null || !authState.User.Identity.IsAuthenticated)
            {
                var returnUrl = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    _navigationManager.NavigateTo("login");
                }
                else
                {
                    _navigationManager.NavigateTo($"login?returnUrl={returnUrl}");
                }
            }
            else
            {
                notAuthorized = true;
            }

        }

    }
}
