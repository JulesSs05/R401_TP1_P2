// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Devise> devises;
        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set { devises = value;
                OnPropertyChanged("Devises"); }
        }

        private double montant;
        public double Montant
        {
            get { return montant; }
            set { montant = value; 
                OnPropertyChanged("Montant");
            }
        }

        private Devise selectedDevise;
        public Devise SelectedDevise
        {
            get { return selectedDevise; }
            set {selectedDevise = value;
                OnPropertyChanged("SelectedDevise");
            }
        }

        private double montantDevise;
        public double MontantDevise
        {
            get { return montantDevise; }
            set { montantDevise = value;
                OnPropertyChanged("MontantDevise");
            }
        }

        public WSService service = new WSService("https://localhost:44320/api/");

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:44320/api/");
            List<Devise> result = await service.GetDevisesAsync("Devise");
            if (result == null)
            {
                DisplayError("Erreur", "API non disponible !");
            }
            else
            {
                Devises = new ObservableCollection<Devise>(result);
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void BtnConvertir_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedDevise == null)
            {
                DisplayError("Erreur", "Vous devez sélectionner une devise !");
            }            
            else
                MontantDevise = service.CalculConvertion(Montant, SelectedDevise.Taux);
        }
        private async void DisplayError(string title, string content)
        {
            ContentDialog noApiDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };
            noApiDialog.XamlRoot = this.Content.XamlRoot;
            ContentDialogResult result = await noApiDialog.ShowAsync();
        }
    }
}