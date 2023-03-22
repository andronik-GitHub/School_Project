namespace SchoolLibrary_EF.DAL.Pagging.Entities
{
    public class AuthorParameters : BaseParameters
    {
        public uint MinYearOfBirth { get; set; }
        public uint MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;

        public bool ValidYearRand => MaxYearOfBirth > MinYearOfBirth;
    }
}
