using Final.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Repositorio
{
    public class SQLiteRepo
    {
        public string _dbPath;
        private SQLiteConnection conn;

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Auto>();
        }

        public SQLiteRepo(string dbPath)
        {
            _dbPath = dbPath;
        }

        // Crear (Insertar)
        public void GuardarAuto(Auto auto)
        {
            Init();

            // Verificar si ya existe un auto con el mismo ID
            var existingAuto = conn.Table<Auto>().FirstOrDefault(a => a.id == auto.id);

            if (existingAuto == null)
            {
                // No existe un auto con el mismo ID, entonces lo insertamos
                conn.Insert(auto);
            }

        }

        // Leer (Listar todos)
        public List<Auto> DevuelveListadoAutos()
        {
            Init();
            return conn.Table<Auto>().ToList();
        }

        // Leer (Obtener uno por Id)
        public Auto ObtenerAutoPorId(int id)
        {
            Init();
            return conn.Find<Auto>(id);
        }

        // Actualizar
        public void ActualizarAuto(Auto auto)
        {
            Init();
            conn.Update(auto);
        }

        // Eliminar
        public void EliminarAuto(string id)
        {
            Init();
            var autoAEliminar = conn.Table<Auto>().FirstOrDefault(a => a.id == id);
            if (autoAEliminar != null)
            {
                conn.Delete(autoAEliminar);
            }
        }

        // Obtener auto de API (ejemplo)
        public async Task<Auto> DevuelveAutoAsync()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://api.chucknorris.io/jokes/random");
            string response_json = await response.Result.Content.ReadAsStringAsync();

            Auto auto = JsonConvert.DeserializeObject<Auto>(response_json);

            return auto;
        }
    }
}
