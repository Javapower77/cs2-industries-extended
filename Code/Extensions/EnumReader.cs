using Colossal.UI.Binding;

namespace IndustriesExtended.Extensions
{
    public class EnumReader<T> : IReader<T>
    {
        public void Read(IJsonReader reader, out T value)
        {
            reader.Read(out int value2);
            value = (T)(object)value2;
        }
    }
}
