namespace mktsystem.application.Interfaces
{
    public interface IAIService
    {
        Task<string> GenerateAsync(string prompt);
    }
}
