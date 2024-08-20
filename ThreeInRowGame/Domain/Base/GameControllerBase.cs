namespace ThreeInRowGame.Domain.Base;

public abstract class GameControllerBase 
{
    // Команды:

    // Сделать ход (Переставить элементы) - отправляет команду в GameManager - logic    
    // Предусловие - получен корректный ввод (правильное количество координат)
    public abstract void Turn(string input); // todo - результат хода - перерисованная сетка

    // Начать новую игру
    public abstract void StartNewGame(); // результат - сетка

    // Завершить игру
    // Предусловие - игра была начата
    public abstract void EndGame(); // результат - статистика
    
    // Возвращает отображение сетки игроку
    // Предусловие - игра была начата (сетка инициализирована)
    public abstract GameGridBase GetGrid();

    // Возвращает статистику игрока
    public abstract GameStatistic GetStatistic();
}