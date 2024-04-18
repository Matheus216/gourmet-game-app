using gourmet_game.Models;

namespace gourmet_game.Services;

public class FoodControlGame: IFoodControlGame
{
    private Dictionary<string, List<Food>> GraphFood = new Dictionary<string, List<Food>>();

    public FoodControlGame()
    {
        ResetGraph();   
    }

    private string KeyFound = string.Empty;
    private string FoundFood = string.Empty;

    private void ResetGraph()
    {
        GraphFood["Massa"] = new List<Food>
        {
            new Food("Lasanha")
        };

        GraphFood["Bolo de chocolate"] = new List<Food>
        {
        };
    }

    public async Task StartGame(Page view)
    {
        while (true)
        {
            await ControlFlow(view);

            bool continueGame = await IsContinue(view);
            if (!continueGame) break;

            CleanVariables(continueGame);
        }
    }

    private async Task ControlFlow(Page view)
    {
        await VerifyType(view);
        if (!string.IsNullOrEmpty(KeyFound))
        {

            await VerifyFood(view);
            await VerifyFound(view);
        }
        else
        {
            await IncludeInList(view);
        }
    }


    private async Task VerifyType(Page view)
    {
        foreach (var dic in GraphFood ?? new Dictionary<string, List<Food>>())
        {
            var response = await GetPlateThought(view, dic.Key);

            if (response)
            {
                KeyFound = dic.Key;
                break;
            }
        }
    }
    private async Task VerifyFood(Page view)
    {
        foreach (var dic in GraphFood[KeyFound])
        {
            var response = await GetPlateThought(view, dic.Name);

            if (response)
            {
                FoundFood = dic.Name;
                break;
            }
        }
    }

    private async Task VerifyFound(Page view)
    {
        if (string.IsNullOrEmpty(FoundFood))
        {
            string foodResponse = await GetPlateThought(view);
            GraphFood[KeyFound].Add(new Food(foodResponse));
        }
        else
        {
            await view.DisplayAlert("Resultado", $"Seu prato é: {FoundFood} ( Acertei de novo )", "Ok");
        }
    }

    private async Task IncludeInList(Page view)
    {
        string foodResponse = await GetPlateThought(view);

        string result = await view.DisplayPromptAsync("Question", $"{foodResponse} é ___ mas {GetLastFood()} não");

        GraphFood?.Add(result, new List<Food> { new Food(foodResponse) });
    }

    private string GetLastFood()
    {
        var lastValue = GraphFood?.LastOrDefault().Value;

        var response = (lastValue is null || !lastValue.Any()) ? GraphFood?.LastOrDefault().Key : lastValue.LastOrDefault()?.Name;

        return response ?? ""; 
    }

    private async Task<bool> IsContinue(Page view)
    {
        bool _continue = true;
        if (!string.IsNullOrEmpty(FoundFood))
            _continue = await view.DisplayAlert("Question", $"Deseja continuar?", "Sim", "Não");
            
        return _continue; 
    }

    private async Task<string> GetPlateThought(Page view)
        => await view.DisplayPromptAsync("Question", "Qual prato você pensou?");

    private async Task<bool> GetPlateThought(Page view, string plate)
        => await view.DisplayAlert("Question", $"O prato que pensou é uma {plate}?", "Sim", "Não");

    private void CleanVariables(bool continueGame)
    {
        KeyFound = string.Empty;
        FoundFood = string.Empty;

        if (!continueGame)
            ResetGraph();
    }
}
