namespace Raccons_House_Games
{
    public interface IServiceLocator
    {
        T GetService<T>();
    }

}