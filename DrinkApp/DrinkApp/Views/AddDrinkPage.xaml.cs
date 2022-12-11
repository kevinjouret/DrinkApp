using DrinkApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrinkApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDrinkPage : ContentPage
    {
        public AddDrinkPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            DrinkList.ItemsSource = await App.Database.GetDrinksAsync();
        }

        Drink lastSelection;
        private void DrinkList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lastSelection = e.CurrentSelection[0] as Drink;

            nameEntry.Text = lastSelection.Name;
            colorEntry.Text = lastSelection.Color;
            quantityEntry.Text = lastSelection.Quantity.ToString();
        }

        async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.Database.SaveDrinkAsync(new Drink
                {
                    Name = nameEntry.Text,
                    Color = colorEntry.Text,
                    Quantity = Convert.ToInt32(quantityEntry.Text)
                });

                ClearEntry();

                DrinkList.ItemsSource = await App.Database.GetDrinksAsync();
            }
        }
        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            if (lastSelection != null)
            {
                lastSelection.Name = nameEntry.Text;
                lastSelection.Color = colorEntry.Text;
                lastSelection.Quantity = Convert.ToInt32(quantityEntry.Text);

                await App.Database.UpdateDrinkAsync(lastSelection);

                ClearEntry();

                DrinkList.ItemsSource = await App.Database.GetDrinksAsync();
            }
        }
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (lastSelection != null)
            {
                await App.Database.DeleteDrinkAsync(lastSelection);

                ClearEntry();

                DrinkList.ItemsSource = await App.Database.GetDrinksAsync();
            }
        }

        private void ClearEntry()
        {
            nameEntry.Text = string.Empty;
            colorEntry.Text = string.Empty;
            quantityEntry.Text = string.Empty;
        }
    }
}