namespace Zarlo.Cosmos;

public class TwoDArray<T>
{

    T[] data;

    public TwoDArray(uint x, uint y)
    {
        data = new T[x * y];
    }

    public T this[int x, int y]
    {
        set
        {
            data[x + (x * y)] = value;
        }
        get
        {
            return data[x + (x * y)];
        }
    }

}