namespace GuessGame;

public class GuessGame : Game, IParserInput
{
    private readonly IView _view;
    private readonly int _number;
    private readonly int _guess;
    private readonly IParserInput _parserInputImplementation;

    public GuessGame(int number, int guess, IParserInput parserInputImplementation) : this(new ConsoleView(), number, guess, parserInputImplementation) { }
    
    public GuessGame(IView view, int number, int guess, IParserInput parserInputImplementation) : base(view)
    {
        _view = view;
        _number = number;
        _guess = guess;
        _parserInputImplementation = parserInputImplementation;
    }
    
    protected override void Play()
    {
        PlayGame();
    }

    private void PlayGame()
    {
        var guess = 0;
        _view.Show("Guess a number between 1 and 100");
        
        _view.Show($"Guess a number between 1 and {_number}");

        while (_guess != _number)
        {
            _view.Show("Your guess: ");

            if (!_parserInputImplementation.TryParse(Console.ReadLine(), out guess))
            {
                _view.Show("Invalid input");
            }

            if (guess < _number)
            {
                _view.Show("Too low");
            }
            else if (guess > _number)
            {
                _view.Show("Too high");
            }
            else
            {
                _view.Show("You guessed it!");
                break;
            }
            
        }
    }

    public bool TryParse(string input, out int number)
    {
        return int.TryParse(input, out number);
    }
}