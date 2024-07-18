using Final.Models;
using Final.Repositorio;
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
    internal class ConsultaViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Auto> _autos;
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

        public ConsultaViewModel()
        {
            LoadDataCommand = new Command(async () => await LoadData());
        }

        private async Task LoadData()
        {
            var httpClient = new HttpClient();
            string url = $"https://public.opendatasoft.com/api/explore/v2.1/catalog/datasets/all-vehicles-model/records?where=make%20like%20%22{PalabraUsuario}%22&limit=25";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                if (json!= null)
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
