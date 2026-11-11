using SQLite;
using AguaLosPibesAPP.Models;
using System.IO;

namespace AguaLosPibesAPP.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        private async Task Init()
        {
            if (_database != null)
                return;

            // Define la ruta donde se guardará el archivo de la base de datos en el celu
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "aguadospibes.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // Crea la tabla User si no existe
            await _database.CreateTableAsync<User>();
        }

        // Registrar un nuevo usuario
        public async Task<int> RegisterUserAsync(string username, string password)
        {
            await Init();
            // Validar si el usuario ya existe
            var existingUser = await _database.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return -1; // Usuario ya registrado
            }

            return await _database.InsertAsync(new User { Username = username, Password = password });
        }

        // Validar el Login
        public async Task<bool> LoginAsync(string username, string password)
        {
            await Init();
            var user = await _database.Table<User>()
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();

            return user != null; // Devuelve true si encuentra coincidencia
        }
    }
}