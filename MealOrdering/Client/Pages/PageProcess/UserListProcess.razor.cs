using MealOrdering.Client.Utils;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MealOrdering.Client.Pages.Users
{
    public class UserListProcess : ComponentBase
    {
        [Inject]
        public HttpClient client { get; set; }

        [Inject]
        ModalManager modalManager { get; set; }

        protected List<UserDTO> UserList = new List<UserDTO>();

        protected async override Task OnInitializedAsync()
        {
            await LoadList();
        }

        protected async Task LoadList()
        {
            //var serviceResponse = await client.GetFromJsonAsync<ServiceResponse<List<UserDTO>>>("api/User/Users");

            try
            {
                UserList = await client.GetServiceResponse<List<UserDTO>>("api/User/Users", true);
            }
            catch (ApiException ex)
            {
                await modalManager.ShowMessageAsync("Api Exception", ex.Message);
            }
        }
    }
}
