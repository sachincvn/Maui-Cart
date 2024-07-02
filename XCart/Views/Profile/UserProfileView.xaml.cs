using XCart.ViewModels.Profile;

namespace XCart.Views.Profile;

public partial class UserProfileView : ContentPage
{
	public UserProfileView(ProfileViewModel profileViewModel)
	{
		InitializeComponent();
		BindingContext = profileViewModel;
	}
}