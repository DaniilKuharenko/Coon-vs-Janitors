
namespace Raccons_House_Games
{
    public interface IState
    {
        void OnEnter();
        void Update();
        void OnExit();
    }
}
