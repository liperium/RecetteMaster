using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RecetteMaster.Models;
using Xamarin.Forms;

namespace RecetteMaster.Views
{
    public partial class AlimentsPage2 : ContentPage
    {
        public AlimentsPage2()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            List<AlimentPossible> alimentPossibles=await App.Database.GetAlimentsPossibleAsync();
            collectionView.ItemsSource = alimentPossibles;
            
        }
        

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NoteEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(AlimentEntryPage2));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                AlimentPossible alimentPossible = (AlimentPossible)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(AlimentEntryPage2)}?{nameof(AlimentEntryPage2.ItemId)}={alimentPossible.Id.ToString()}");
            }
        }
    }

}