namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class Loan : BaseEntity
    {
        public Guid LoanId
        {
            get => id;
            set => id = value;
        }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set;}
    }
}
