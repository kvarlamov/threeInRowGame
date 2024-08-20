using ThreeInRowGame.Domain.Base;

namespace ThreeInRowGame.Domain;

public class User : BaseEntity
{
    public string Name {get;}

    public User(string name)
    {
        Name = name;
    }
}