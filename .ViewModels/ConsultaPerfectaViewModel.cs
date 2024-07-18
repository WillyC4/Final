using Final.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Final.ViewModels
{
    internal class ConsultaPerfectaViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Auto> _autos { get; set; }
        private string _palabraUsuario;

        public ObservableCollection<Auto> Autos
        {
            get => _autos;
            set
            {
                _autos = value;
                OnPropertyChanged();
            }
        }
        public string PalabraUsuario
        {
            get => _palabraUsuario;
            set
            {
                _palabraUsuario = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadDataCommand { get; }
        public ICommand GuardarSQL { get; }
        public ICommand LoadItem { get; }
        public ICommand EliminarAuto { get; }

        public ConsultaPerfectaViewModel()
        {
            LoadDataCommand = new Command(async () => await LoadData());
            GuardarSQL = new Command(async () => await GuardarAutoSQL());
            LoadItem = new Command(async () => await LoadItems());
            EliminarAuto = new Command(async () => await EliminarAutoSQL());
        }

        private async Task LoadData()
        {
            var httpClient = new HttpClient();
            string url = "https://localhost:7286/api/Autos";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                if (json != null)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ListaAutos>(json);

                    Autos = new ObservableCollection<Auto>(apiResponse.Results);
                }

            }
            else
            {
                // Handle error
            }
        }
        private async Task EliminarAutoSQL() 
        {
            App.SQLRepo.EliminarAuto(PalabraUsuario);
            LoadItems();
            OnPropertyChanged();
        }

        private async Task GuardarAutoSQL()
        {
            var httpClient = new HttpClient();
            string url = $"https://localhost:7286/api/Autos/{PalabraUsuario}";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                if (json != null)
                {
                    Auto auto = JsonConvert.DeserializeObject<Auto>(json);
                    App.SQLRepo.GuardarAuto(auto);
                    LoadItems();

                    //var apiResponse = JsonConvert.DeserializeObject<ListaAutos>(json);
                    //Autos = new ObservableCollection<Auto>(apiResponse.Results);
                }

            }
        }

        private async Task LoadItems()
        {
            _autos = new ObservableCollection<Auto>();
            var items = App.SQLRepo.DevuelveListadoAutos();
            foreach (var item in items)
            {
                _autos.Add(item);
            }
            OnPropertyChanged(nameof(_autos));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
