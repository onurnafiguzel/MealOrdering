using Blazored.Modal;
using Blazored.Modal.Services;
using MealOrdering.Client.CustomComponents.ModalComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Client.Utils
{
    public class ModalManager
    {
        private readonly IModalService modalService;

        public ModalManager(IModalService modalService)
        {
            this.modalService = modalService;
        }
        public async Task ShowMessageAsync(string title, string message, int duration = 0)
        {
            ModalParameters mParams = new ModalParameters();
            mParams.Add("Message", message);

            var modalRef = modalService.Show<ShowMessagePopupComponent>(title, mParams);

            if (duration > 0)
            {
                await Task.Delay(duration);
                modalRef.Close();
            }
        }

        public async Task<bool> ConfirmationAsync(string title, string message)
        {
            ModalParameters mParams = new ModalParameters();
            mParams.Add("Message", message);

            var modalRef = modalService.Show<ConfirmationPopupComponent>(title, mParams);
            var modalResult = await modalRef.Result;

            return !modalResult.Cancelled;
        }
    }
}
