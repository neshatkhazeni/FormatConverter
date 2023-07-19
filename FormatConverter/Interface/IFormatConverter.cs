namespace FormatConverter.Interface
{
    public interface IFormatConverter<T>
    {
        string FromObject(T doc);
        T ToObject(string input);
    }
}
