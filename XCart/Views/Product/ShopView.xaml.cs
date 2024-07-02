using XCart.ViewModels.Product;

namespace XCart.Views.Product;

public partial class ShopView : ContentPage
{
	public ShopView(ShopViewModel shopViewModel)
	{
		InitializeComponent();
		BindingContext = shopViewModel;
	}
}