using AndresVillarreal_project2.Views;

namespace AndresVillarreal_project2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CityDetailsPage), typeof(CityDetailsPage));
        }
    }
}
