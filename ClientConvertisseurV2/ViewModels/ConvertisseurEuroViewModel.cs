using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:44320/api/");
            List<Devise> result = await service.GetDevisesAsync("Devise");
            if (result == null)
            {
                
            }
            else
            {
                Devises = new ObservableCollection<Devise>(result);
            }
        }
        private void ActionSetConversion()
        {
            MontantDevise = service.CalculConvertion(Montant, SelectedDevise.Taux);
        }
    }   
}
