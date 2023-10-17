namespace Liquip;

/// <summary>
/// a 2d array
/// </summary>
/// <typeparam name="T"></typeparam>
public class TwoDArray<T>
{
    /// <summary>
    /// the real array
    /// </summary>
    private readonly T[] data;

    /// <summary>
    ///
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public TwoDArray(uint x, uint y)
    {
        data = new T[x * y];
        _x = x;
    }

    private uint _x;

    /// <summary>
    ///
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public T this[int x, int y]
    {
        set => data[x + (_x * y)] = value;
        get => data[x + (_x * y)];
    }
}
