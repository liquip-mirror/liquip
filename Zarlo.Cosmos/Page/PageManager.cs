using Zarlo.XSharp;

namespace Zarlo.Cosmos.Page;

public static class Paging
{

    public static void EnablePaging() => throw new ImplementedInPlugException();

    public static void AddPage(ref PagingDirectory pagingDirectory, PageSize pageSize)
    {
    }
    //
    // public static PagingDirectory MakePagingDirectory()
    // {
    // }

}

public enum PageSize
{
    MB1,
    MB10,
    MB100,
    GB1
}
