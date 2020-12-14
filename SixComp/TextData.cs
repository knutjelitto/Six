namespace SixComp
{
    public class TextData : ToData
    {
        public TextData(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
