using ThreeInRowGame.Domain;
using ThreeInRowGame.Domain.Base;

namespace ThreeInRowGame;


public class GameManager : GameManagerBase
{
    private GameGridImpl _grid;
    private static GameManager? _instance;
    
    public event Action<int>? NotifyOnTurn;
    public event Action<string>? NotifyStatistic; 

    public static GameManager? Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    public override void InitializeNewGame()
    {
        ElementFactory.InitializeElements();
        _grid = new GameGridImpl();
        _grid.Initialize();
        if (CheckCombinations())
            _grid.ReDrawFields();
        GameBonuses.Instance!.PrintStatistic();
    }

    public override void ProcessTurn(string? input)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        if (string.IsNullOrWhiteSpace(input) || input.Length != 4 || !int.TryParse(input[1].ToString(), out _)|| !int.TryParse(input[3].ToString(), out _))
        {
            Console.WriteLine("Неправильный ввод, повторите");
        }

        //todo - добавить валидацию, что ввод не выходит за пределы индекса
        if (!Validate(input!))
        {
            Console.WriteLine("Вы ввели не соседние координаты, повторите");
        }
        _grid.ChangeElements(input!);
        if (CheckCombinations())
            _grid.ReDrawFields();
        
        NotifyOnTurn?.Invoke(_grid.LastMatched);
        NotifyStatistic?.Invoke(input!);
        GameBonuses.Instance!.PrintStatistic();
    }

    public override void FinishGame()
    {
        return;
    }

    public override bool CheckPossibleMoves()
    {
        throw new NotImplementedException();
    }

    public override bool CheckCombinations()
    {
        return _grid.CheckThreeInRowAndClear();
    }

    private bool Validate(string input)
    {
        return _grid.IsNeighboors(input);
    }
}