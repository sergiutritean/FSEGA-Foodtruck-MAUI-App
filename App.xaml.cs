using Foodtrucks_final.Views;

namespace Foodtrucks_final;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new FoodtruckListPage());
    }
}