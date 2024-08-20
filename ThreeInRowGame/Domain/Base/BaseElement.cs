using ThreeInRowGame.Domain.ValueObjects;

namespace ThreeInRowGame.Domain.Base;

public abstract class BaseElement
{
    // Получает новый случайный элемент (через element elementFactory)
    public abstract Element GetNext();
}