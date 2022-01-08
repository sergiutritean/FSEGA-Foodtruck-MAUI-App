using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodtrucks_final.Data;
using Foodtrucks_final.Models;

namespace Foodtrucks_final.Views;
[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class FoodtruckPage : ContentPage
{

    public FoodtruckPage()
    {
        InitializeComponent();
    }
    
    async void OnSaveClicked(object sender, EventArgs e)
    {
        Foodtruck foodtruck = (Foodtruck)BindingContext;
        foodtruck.Latitude = Latitude.Text != null ? double.Parse(Latitude.Text) : 0 ;
        foodtruck.Longitude = Longitude.Text != null ? double.Parse(Longitude.Text) : 0 ;
        FoodtruckDatabase database = await FoodtruckDatabase.Instance;
        await database.SaveItemAsync(foodtruck);
        await Navigation.PopAsync();
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        var foodtruck = (Foodtruck)BindingContext;
        FoodtruckDatabase database = await FoodtruckDatabase.Instance;
        await database.DeleteItemAsync(foodtruck);
        await Navigation.PopAsync();
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    async void OnGetLocation(object sender, EventArgs e)
    {
        Location location = await Geolocation.Default.GetLastKnownLocationAsync();
        if (location != null)
        {
            Foodtruck position = new Foodtruck{};
            position.Latitude = location.Latitude;
            position.Longitude = location.Longitude;
            
            Longitude.BindingContext = position;
            Latitude.BindingContext = position;
            
            Console.WriteLine($"Location {location.Latitude} and {location.Longitude}");
        }
    }
}