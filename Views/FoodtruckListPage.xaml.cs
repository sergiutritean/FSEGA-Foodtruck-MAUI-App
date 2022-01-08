using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodtrucks_final.Data;
using Foodtrucks_final.Models;
using Plugin.LocalNotification;

namespace Foodtrucks_final.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class FoodtruckListPage : ContentPage
    {
        public FoodtruckListPage()
        {
            InitializeComponent();
        }
    
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            FoodtruckDatabase database = await FoodtruckDatabase.Instance;

             var foodtrucks = await database.GetItemsAysnc();
             var myLocation = await Geolocation.GetLocationAsync();

             for(int i = 0; i < foodtrucks.Count; ++i)
             {
                 var foodtruckLocation = new Location(foodtrucks[i].Latitude, foodtrucks[i].Longitude);
                 var distance = myLocation.CalculateDistance(foodtruckLocation, DistanceUnits.Kilometers);
                 foodtrucks[i].Distance = distance;
                 
                 if (distance < 1)
                 {
                     var request = new NotificationRequest
                     {
                         Title = $"{foodtrucks[i].Name} este în apropiere!",
                         Description = "Vezi ce bunătăți are pregătite pentru tine!",
                         Schedule = new NotificationRequestSchedule
                         {
                             NotifyTime = DateTime.Now.AddSeconds(1)
                         }

                     };
                     LocalNotificationCenter.Current.Show(request);
                 }

             }

             listView.ItemsSource = foodtrucks;

        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FoodtruckPage
            {
                BindingContext = new Foodtruck()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new FoodtruckPage
                {
                    BindingContext = e.SelectedItem as Foodtruck
                });
            }
        }
    
    }   
}