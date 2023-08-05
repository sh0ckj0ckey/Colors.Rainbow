namespace Colors.Rainbow.Models
{
    public class WindowsColor
    {
        public string Name { get; set; }
        public double RValue { get; set; }
        public double GValue { get; set; }
        public double BValue { get; set; }
        public string Hex { get; set; }
        public Windows.UI.Color VisualColor { get; set; }
        public WindowsColor Self { get; set; }

        public WindowsColor()
        {
            Self = this;
        }
    }
}
