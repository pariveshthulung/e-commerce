using ebay.ViewModel;

namespace ebay.Manager.Interface;

public interface IAuthManager
{
    Task LogIn(string Username,string Password);
    Task LogOut ();
    Task Register(AuthRegisterVm vm);
}
