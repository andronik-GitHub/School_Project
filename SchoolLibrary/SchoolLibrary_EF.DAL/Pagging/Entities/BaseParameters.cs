namespace SchoolLibrary_EF.DAL.Pagging.Entities
{
    public abstract class BaseParameters
    {
        private const int MaxPageSize = 150;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public string? OrderBy { get; set; } = default!;
    }
}
