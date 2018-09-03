namespace Maths.Olympiad.Dal.Interfaces
{
    public interface ISerializer
    {
        string Serialize<TData>(TData data);
        TData Deserialize<TData>(string data);
    }
}