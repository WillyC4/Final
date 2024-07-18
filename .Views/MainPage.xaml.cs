namespace Final.Views
{
    public partial class MainPage : ContentPage
    {
        private int currentImageIndex = 0; // Índice de la imagen actual
        private bool isTimerRunning = true; // Variable para controlar la ejecución del temporizador
        private int intervalSeconds = 3; // Intervalo en segundos entre cambios de imagen
        private Image[] images; // Arreglo para almacenar las imágenes

        public MainPage()
        {
            InitializeComponent();

            // Inicializar arreglo de imágenes
            images = new Image[] { firstImage, secondImage, thirdImage };

            // Iniciar el temporizador al cargar la página
            Device.StartTimer(TimeSpan.FromSeconds(intervalSeconds), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    // Ocultar la imagen actual
                    images[currentImageIndex].IsVisible = false;

                    // Incrementar el índice de imagen actual
                    currentImageIndex = (currentImageIndex + 1) % images.Length;

                    // Mostrar la siguiente imagen
                    images[currentImageIndex].IsVisible = true;
                });

                // Devolver true para continuar ejecutando el temporizador de manera recurrente
                return isTimerRunning;
            });

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            isTimerRunning = false; // Marcar que la página ya no está activa
        }
        
        private void btnSiguiente(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Consulta());
        }

    }

}