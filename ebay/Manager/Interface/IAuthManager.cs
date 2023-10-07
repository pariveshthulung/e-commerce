namespace ebay.Manager.Interface;

public interface IAuthManager
{
    Task LogIn(string Username,string Password);
    Task LogOut ();
}
