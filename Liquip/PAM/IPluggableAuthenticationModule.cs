namespace Liquip.PAM;

/// <summary>
/// PluggableAuthenticationModule
/// </summary>
public interface IPluggableAuthenticationModule
{
    /// <summary>
    /// change a password based on userId
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    public void ChangePassword(uint userId, string? password);
    /// <summary>
    /// adds a user to the backend
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="username"></param>
    /// <param name="groups"></param>
    public void MakeUser(uint userId, string username, uint[]? groups = null);

    /// <summary>
    /// checks users password
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool CheckPassword(uint userId, string? password);
}
