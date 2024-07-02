using XCart.ViewModels.Product;

namespace XCart.Views.Product;

public partial class AddProductView : ContentPage
{
	public AddProductView(AddProductViewModel addProductViewModel)
	{
		InitializeComponent();
		BindingContext = addProductViewModel;
	}
}