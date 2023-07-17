namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class BookParameters : BaseParameters
    {
        public BookParameters()
        {
            OrderBy = "Title, PublishingYear desc"; // default sorting
            Fields = "BookId, Title, PublishingYear, ISBN, DateCreated, UpdateDate, DeleteDate"; // default fields (DataShaping)
        }
    }
}
