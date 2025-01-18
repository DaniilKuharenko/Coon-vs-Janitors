namespace Raccons_House_Games
{
    public interface IObjective
    {
        string Title { get; }
        string Description { get; }
        bool IsCompleted { get; }
    }
}