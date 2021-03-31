using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RecetteMaster.Models;
using Xamarin.Forms;

namespace RecetteMaster.Views
{
    public partial class RecettesPage : ContentPage
    {
        public RecettesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetRecettesAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NoteEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(RecetteEntryPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Recette recette = (Recette)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(RecetteEntryPage)}?{nameof(RecetteEntryPage.ItemId)}={recette.Id.ToString()}");
            }
        }
    }
}