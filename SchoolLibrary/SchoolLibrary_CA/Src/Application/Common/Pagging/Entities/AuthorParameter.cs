namespace Application.Common.Pagging.Entities
{
    public class AuthorParameter : BaseParameter
    {
        public uint MinYearOfBirth { get; set; } = default!;
        public uint MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;
        public bool ValidYearRand => MaxYearOfBirth > MinYearOfBirth;

        public string? FullName { get; set; } = default!;

        public AuthorParameter()
        {
            OrderBy = "AuthorId"; // default sorting
        }
    }
}