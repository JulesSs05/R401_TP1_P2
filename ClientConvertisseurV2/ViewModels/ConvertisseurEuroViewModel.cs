using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurEuroViewModel: ObservableObject
    {
        private ObservableCollection<Devise> devises;
        public IRelayCommand BtnSetConversion { get; }
        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set
            {
                devises = value;
                OnPropertyChanged();
            }
        }

        private double montant;
        public double Montant
        {
            get { return montant; }
            set
            {
                montant = value;
                OnPropertyChanged();
            }
        }

        private Devise selectedDevise;
        public Devise SelectedDevise
        {
            get { return selectedDevise; }
            set
            {
                selectedDevise = value;
                OnPropertyChanged();
            }
        }

        private double montantDevise;
        public double MontantDevise
        {
            get { return montantDevise; }
            set
            {
                montantDevise = value;
                OnPropertyChanged();
            }
        }

        public WSService service = new WSService("https://localhost:44320/api/");
        public ConvertisseurEuroViewModel()
        {
            GetDataOnLoadAsync();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }

        public async void GetDataOnLoadAsync()
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
        public void ActionSetConversion()
        {
            if (SelectedDevise == null)
            {
                DisplayError("Erreur", "Vous devez sélectionner une devise !");
            }
            else
                MontantDevise = Montant * SelectedDevise.Taux;
        }

        private async void DisplayError(string title, string content)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };
            errorDialog.XamlRoot = App.MainRoot.XamlRoot;
            ContentDialogResult result = await errorDialog.ShowAsync();
        }
    }   
}
