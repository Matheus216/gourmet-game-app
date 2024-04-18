namespace gourmet_game.Models;

/// <summary>
/// Types of food
/// </summary>
public class TypeFood
{
    public TypeFood()
    {
        Food = new List<Food>();
    }

    public string Description { get; set; } = string.Empty;
    public List<Food> Food { get; set; }

}
