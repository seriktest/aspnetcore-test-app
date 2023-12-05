namespace GuessGame;

public class ConsoleView : IView
{
    public void Show(string message)
    {
        Console.WriteLine(message);
    }
}