using XCart.ViewModels.Home;
using XCart.Views.Profile;

namespace XCart.Views.Home;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel homeViewModel)
	{
		InitializeComponent();
		BindingContext = homeViewModel;
    }
}