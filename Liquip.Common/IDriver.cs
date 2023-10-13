namespace Liquip.Common;

/// <summary>
/// driver interface
/// </summary>
public interface IDriver
{
    /// <summary>
    /// run at preboot stage
    /// </summary>
    public void PreBoot();

    /// <summary>
    /// main boot stage
    /// </summary>
    public void Boot();

    /// <summary>
    /// final boot stage
    /// </summary>
    public void PostBoot();

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public bool IsSupported();
}
