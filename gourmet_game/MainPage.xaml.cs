using gourmet_game.Models;
using gourmet_game.Services;

namespace gourmet_game
{
    public partial class MainPage : ContentPage
    {
        private readonly IFoodControlGame _foodControlGame;
        public MainPage(IFoodControlGame chooseService)
        {
            InitializeComponent();

            _foodControlGame = chooseService;
        }
         
        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await _foodControlGame.StartGame(this);
        }
    }

}
