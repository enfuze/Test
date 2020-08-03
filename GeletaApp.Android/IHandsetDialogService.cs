using System;
using System.Threading.Tasks;

namespace GeletaApp.Droid
{
    public interface IHandsetDialogService
    {
        Task<bool> ShowAlert(string message, string title, string okButton, Action callback);
        Task<bool> ShowAlertConfirm(string message, string title, string confirmButton, string cancelButton, Action<bool> callback);

    }
    
}