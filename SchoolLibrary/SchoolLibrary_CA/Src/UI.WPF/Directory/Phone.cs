namespace UI.WPF.Directory
{
    public class Phone
    {
        public string Title { get; set; } = default!;
        public string Company { get; set; } = default!;
        public int Price { get; set; } = default!;


        public override string ToString()
        {
            return $"{Title} ({Company}) - {Price}";
        }
    }
}