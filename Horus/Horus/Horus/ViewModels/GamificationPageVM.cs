using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Horus.Models;
using Horus.Services;
using Xamarin.Forms;

namespace Horus.ViewModels
{
    public class GamificationPageVM:BaseViewModel
    {
        #region Variables
        string txtSample;
        string token;
        ObservableCollection<Gamification> retos;
        ApiService apiService;
        int totales, finalizados;
        #endregion

        #region CONSTRUCTOR
        public GamificationPageVM(INavigation navigation, string Token)
        {
            IsBusy = true;
            Navigation = navigation;
            apiService = new ApiService();
            retos = new ObservableCollection<Gamification>();
            token = Token;
           CargarDatos();
        }
        #endregion

        #region OBJETOS


        public string TxtSample
        {
            get { return txtSample; }
            set { SetValue(ref txtSample, value); }
        }
        public int Totales
        {
            get { return totales; }
            set { SetValue(ref totales, value); }
        }
        public int Finalizados
        {
            get { return finalizados; }
            set { SetValue(ref finalizados, value); }
        }



        public ObservableCollection<Gamification> Retos
        {
            get { return retos; }
            set { SetValue(ref retos, value); }
        }


        #endregion

        #region PROCESOS

        public async void CargarDatos()
        {
            IsBusy = true;
           var  RetosAux =  await apiService.GetCallenges(token);
            Totales = RetosAux.Count;
            foreach (var item in RetosAux)
            {
                string pancakeColor = "#FFFFFF";
                string titleColor = "#1A1A1A";
                string descriptionColor = "#828282";
                string barColor = "#F49390";
                string myIcon = "arrow_right_g.png";
                if (item.currentPoints == item.totalPoints)
                {
                     pancakeColor = "#F49390";
                     titleColor = "#FFFFFF";
                     descriptionColor = "#FFFFFF";
                     barColor = "#FFFFFF";
                     myIcon = "arrow_right_w.png";

                    Finalizados++;
                    
                }
                Retos.Add(new Gamification
                {
                    id = item.id,
                    title = item.title,
                    description = item.description,
                    currentPoints = item.currentPoints,
                    totalPoints = item.totalPoints,
                    currenTotalPoints = item.currentPoints + "/" + item.totalPoints,
                    percent = (double)(int.Parse(item.currentPoints.ToString())) / (double)(int.Parse(item.totalPoints.ToString())),
                    percentLbl=(((double)(int.Parse(item.currentPoints.ToString())) / (double)(int.Parse(item.totalPoints.ToString()))) * 100).ToString("#.##") + "%",
                    pancakeColor=pancakeColor,
                    titleColor=titleColor,
                    descriptionColor=descriptionColor,
                    barColor=barColor,
                    myIcon= myIcon

                }) ;

                

            }
            IsBusy = false;
        }

        public async Task returnPageAsync()
        {
           await Navigation.PopAsync();
        }
        #endregion

        #region COMANDOS
        public ICommand returnCmd => new Command(async () => await returnPageAsync());
        #endregion
    }
}

