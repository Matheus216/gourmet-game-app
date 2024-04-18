

namespace gourmet_game
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            
        }

        const int _height = 500;
        const int _width = 600; 

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            window.MaximumHeight = _height;
            window.MaximumWidth = _width;
            window.MinimumHeight = _height;
            window.MinimumWidth = _width;

            return window;
        }
    }
}
