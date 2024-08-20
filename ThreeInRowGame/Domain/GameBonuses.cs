namespace ThreeInRowGame.Domain.Base;

public class GameBonuses : BaseEntity
{
    private static GameBonuses? _instance;

    private static int _bonusCount;
    
    public static GameBonuses? Instance
    {
        get { return _instance ??= new GameBonuses(); }
    }
    
    public void UpdateUserBonuses(int count)
    {
        _bonusCount += count;
    }

    public void PrintStatistic()
    {
        Console.WriteLine("Количество очков в текущей игре: {0}", _bonusCount);
    }

    public int GetBonuses()
    {
        return _bonusCount;
    }

    public void ClearBonuses()
    {
        _bonusCount = 0;
    }
}