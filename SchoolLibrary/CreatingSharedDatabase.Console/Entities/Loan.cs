namespace CreatingSharedDatabase.Console.Entities
{
    public class Loan : BaseEntity
    {
        public Guid LoanId
        {
            get => Id;
            set => Id = value;
        }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set;}

        public Guid UserId { get; set; }
        public User User { get; set; } = default!; // many-to-one
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!; // many-to-one
    }
}
