namespace gourmet_game.Models;

public class Food
{
    public Food(string inputName)
    {
        this.Name = inputName;
    }

    public string Name { get; set; } = string.Empty;
}
