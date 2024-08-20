// See https://aka.ms/new-console-template for more information

// todo - добавить ВЕЗДЕ статусы-состояния

using ThreeInRowGame;
using ThreeInRowGame.Domain;
using ThreeInRowGame.Domain.Base;

Console.WriteLine("Please input your name and press ENTER:\n");
var name = Console.ReadLine();

bool processGame = true;

var user = new User(name!);

var gameManager = new GameManager();
gameManager.InitializeNewGame();

var bonuses = GameBonuses.Instance;
var statistic = GameStatistic.Instance;
gameManager.NotifyOnTurn += bonuses!.UpdateUserBonuses;
gameManager.NotifyStatistic += statistic!.UpdateTurns;

while (processGame)
{
    Console.WriteLine("Введите координаты для перестановки в формате A1B1");
    Console.WriteLine("e - закончить игру и выйти;   r - перезапустить игру;  s - показать историю ходов;");
    var input = Console.ReadLine();
    
    switch (input)
    {
        case "e":
            gameManager.FinishGame();
            processGame = false;
            Console.WriteLine($"{user.Name}, поздравляем, вы заработали {bonuses.GetBonuses()} очков за игру.");
            break;
        case "r":
            bonuses.ClearBonuses();
            gameManager.InitializeNewGame();
            break;
        case "s":
            Console.WriteLine(statistic.GetHistory());
            break;
        default:
            gameManager.ProcessTurn(input);
            break;
    }
}

gameManager.NotifyOnTurn -= bonuses.UpdateUserBonuses;