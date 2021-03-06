using System;
using System.Collections.Generic;
using System.IO;
using RecetteMaster;
using RecetteMaster.Models;
using Xamarin.Forms;

namespace RecetteMaster.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class RecetteEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadRecette(value);
            }
        }

        public RecetteEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.
            BindingContext = new Recette();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var recette = (Recette)BindingContext;
            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            if(recette!=null){
                collectionView.ItemsSource = await App.Database.GetAlimentPossibleRecetteAsync(recette.Id);
            }
            List<AlimentPossible> alimentPossibles=await App.Database.GetAlimentsPossibleAsync();
            picker.ItemsSource = alimentPossibles;
            picker.SelectedIndex=0;
           
        }
        async void LoadRecette(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Recette recette = await App.Database.GetRecetteAsync(id);
                BindingContext = recette;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var recette = (Recette)BindingContext;
            recette.Date = DateTime.UtcNow;
            if (!string.IsNullOrWhiteSpace(recette.Nom))
            {
                await App.Database.SaveRecetteAsync(recette);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var recette = (Recette)BindingContext;
            await App.Database.DeleteRecetteAsync(recette);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
        async void OnAddButtonClicked(object sender, EventArgs e)
        {
            var recette = (Recette)BindingContext;
            var pickerSelectedItem = (AlimentPossible)picker.SelectedItem;
            await App.Database.SaveAlimentRecetteAsync(pickerSelectedItem.Id,recette.Id);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}