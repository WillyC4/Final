using Final.Repositorio;

namespace Final
{
    public partial class App : Application
    {
        public static SQLiteRepo SQLRepo { get; private set; }
        public App(SQLiteRepo repo)
        {
            InitializeComponent();

            MainPage = new AppShell();

            SQLRepo = repo;
        }
        
    }
}
