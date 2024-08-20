namespace ThreeInRowGame.Domain.Base;

public abstract class GameGridBase
{
    // todo добавить статусы 
    // private int[,] _field = new int[8,8];

    // КОМАНДЫ:
    // Инициализирует игровое поле при начале новой игры
    // Предусловие - новая сетка еще не создавалась
    // Постусловие - создана игровая сетка, заполненная элементами
    public abstract void Initialize();

    // Переставить элементы (по вводу)
    // Предусловие - в игровой сетке есть элементы (сетка создана)
    // Постусловие - элементы переставлены
    public abstract void ChangeElements(string input);

    // Удалить элементы и сдвинуть вниз для заполнения пустот
    // Предусловие - в игровой сетке есть элементы (сетка создана)
    // Постусловие - элементы сдвинуты    
    protected abstract void UpdateOnTurn();

    // Заполнить пустоты после удаления
    // Предусловие - кол-во элементов меньше исходного
    // Постусловие - кол-во элементов соответствует исходному
    public abstract void ReDrawFields();


    // ЗАПРОСЫ:
    // Получает текущее игровое поле
    // Предусловие: закончено создание поля
    public abstract void Draw();

    public abstract bool CheckThreeInRowAndClear(bool redraw = false);

    public abstract bool IsNeighboors(string input);
}