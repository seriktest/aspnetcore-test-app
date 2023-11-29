namespace GuessGame;

public abstract class Game
{
    private readonly IView _view;

    protected Game(IView view)
    {
        _view = view;
    }

    protected virtual void Play()
    {
        _view.Show("\t\tStarting the game...\n\n");
    }
}