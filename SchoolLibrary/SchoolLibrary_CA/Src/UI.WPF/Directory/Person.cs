namespace UI.WPF.Directory
{
    public class Person
    {
        public string Name { get; set; } = default!;
        public int Age { get; set; } = default!;

        public override string ToString()
        {
            return $"{Name} {Age}";
        }
    }
}