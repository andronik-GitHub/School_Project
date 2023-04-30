namespace Application.Common.Pagging.Entities
{
    public class AuthorParameter : BaseParameter
    {
        public uint MinYearOfBirth { get; set; }
        public uint MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;

        public bool ValidYearRand => MaxYearOfBirth > MinYearOfBirth;
    }
}