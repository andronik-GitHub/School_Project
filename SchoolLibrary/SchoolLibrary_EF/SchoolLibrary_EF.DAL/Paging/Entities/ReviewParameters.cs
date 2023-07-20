namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class ReviewParameters : BaseParameters
    {
        public ReviewParameters()
        {
            OrderBy = "ReviewId"; // default sorting
            Fields = "ReviewId, Rating, ReviewText, UserId, BookId, DateCreated, DateUpdated, DateDeleted"; // default fields (DataShaping)
        }
    }
}
