using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RecetteMaster.Models;
using Xamarin.Forms;

namespace RecetteMaster.Views
{
    public partial class AlimentsPage : ContentPage
    {
        public AlimentsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetAlimentsPossibleAsync();}

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NoteEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(AlimentEntryPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                AlimentPossible alimentPossible = (AlimentPossible)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(AlimentEntryPage)}?{nameof(AlimentEntryPage.ItemId)}={alimentPossible.Id.ToString()}");
            }
        }
    }

}