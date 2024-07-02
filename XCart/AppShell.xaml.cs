using XCart.Views.Auth;
using XCart.Views.Home;
using XCart.Views.Product;
using XCart.Views.Profile;

namespace XCart
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(UserProfileView), typeof(UserProfileView));
            Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(AddProductView), typeof(AddProductView));
            Routing.RegisterRoute(nameof(ShopView), typeof(ShopView));
            Routing.RegisterRoute(nameof(CartView), typeof(CartView));
        }
    }
}
