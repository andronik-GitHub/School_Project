namespace SchoolLibrary_EF.DAL.Pagging.Entities
{
    public abstract class BaseParameters
    {
        const int maxPageSize = 150;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
