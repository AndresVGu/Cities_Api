using AndresVillarreal_project2.ViewModels;

namespace AndresVillarreal_project2.Views;

public partial class CityDetailsPage : ContentPage
{
	public CityDetailsPage(CityDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}