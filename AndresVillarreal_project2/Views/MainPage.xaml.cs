using AndresVillarreal_project2.ViewModels;

namespace AndresVillarreal_project2.Views
{
    public partial class MainPage : ContentPage
    {
   

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

       
    }

}
