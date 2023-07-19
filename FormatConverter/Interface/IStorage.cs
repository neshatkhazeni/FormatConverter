namespace FormatConverter.Interface
{
    public interface IStorage
    {
        string Read();
        void Write(string content);
    }
}
