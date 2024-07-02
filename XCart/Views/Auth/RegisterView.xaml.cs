using XCart.ViewModels.Auth;

namespace XCart.Views.Auth;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterViewModel registerViewModel)
	{
		InitializeComponent();
		BindingContext = registerViewModel;
	}
}