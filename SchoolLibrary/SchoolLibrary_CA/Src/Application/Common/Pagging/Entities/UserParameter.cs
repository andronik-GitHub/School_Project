namespace Application.Common.Pagging.Entities
{
    public class UserParameter : BaseParameter
    {
        public UserParameter()
        {
            OrderBy = "UserId"; // default sorting
        }
    }
}