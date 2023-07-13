namespace Liquip;

public class TwoDArray<T>
{
    private readonly T[] data;

    public TwoDArray(uint x, uint y)
    {
        data = new T[x * y];
    }

    public T this[int x, int y]
    {
        set => data[x + x * y] = value;
        get => data[x + x * y];
    }
}
