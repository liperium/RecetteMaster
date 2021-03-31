using System;
using System.IO;
using RecetteMaster;
using RecetteMaster.Models;
using Xamarin.Forms;

namespace RecetteMaster.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class AlimentEntryPage2 : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadAlimentPossible(value);
            }
        }

        public AlimentEntryPage2()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.
            BindingContext = new AlimentPossible();
        }

        async void LoadAlimentPossible(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                AlimentPossible alimentPossible = await App.Database.GetAlimentPossibleAsync(id);
                BindingContext = alimentPossible;
                checkBox.IsChecked = alimentPossible.Important;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var alimentPossible = (AlimentPossible)BindingContext;
            alimentPossible.Important = checkBox.IsChecked;
            //TODO INIT ICI
            if (!string.IsNullOrWhiteSpace(alimentPossible.Nom))
            {
                await App.Database.SaveAlimentPossibleAsync(alimentPossible);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var alimentPossible = (AlimentPossible)BindingContext;
            await App.Database.DeleteAlimentPossibleAsync(alimentPossible);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        
    }
}