using ThreeInRowGame.Domain.Base;

namespace ThreeInRowGame;

public sealed class GameController : GameControllerBase
{
    private readonly GameManager? _gameManager;

    public GameController()
    {
        _gameManager = GameManager.Instance;
    }
    
    public override void Turn(string input)
    {
        throw new NotImplementedException();
    }

    public override void StartNewGame()
    {
        _gameManager.InitializeNewGame();
    }

    public override void EndGame()
    {
        throw new NotImplementedException();
    }

    public override GameGridBase GetGrid()
    {
        throw new NotImplementedException();
    }

    public override GameStatistic GetStatistic()
    {
        throw new NotImplementedException();
    }
}