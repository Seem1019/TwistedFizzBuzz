namespace TwistedFizzBuzz.Library.Interfaces
{
    public interface IApiFizzBuzz
    {
        Task<string> CalculateFromApiAsync(string apiUrl);
    }
}
