using System.Text;

namespace ThreeInRowGame.Domain.Base;

public class GameStatistic : BaseEntity
{
    private static GameStatistic? _instance;
    private List<string> _turns = new();
    
    public static GameStatistic? Instance
    {
        get { return _instance ??= new GameStatistic(); }
    }

    public void UpdateTurns(string input)
    {
        _turns.Add(input);
    }

    public string GetHistory()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var turn in _turns)
        {
            sb.Append(turn);
            sb.AppendLine();
        }

        return sb.ToString();
    }
}