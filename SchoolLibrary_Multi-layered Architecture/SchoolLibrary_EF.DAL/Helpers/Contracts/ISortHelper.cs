namespace SchoolLibrary_EF.DAL.Helper.Contracts
{
    public interface ISortHelper<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities, string? orderByString);
    }
}
