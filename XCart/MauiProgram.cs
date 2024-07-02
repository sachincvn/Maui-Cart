using Microsoft.Extensions.Logging;
using SQLite;
using XCart.Helpers;
using XCart.Services.Auth;
using XCart.Services.Product;
using XCart.ViewModels;
using XCart.ViewModels.Auth;
using XCart.ViewModels.Home;
using XCart.ViewModels.Product;
using XCart.ViewModels.Profile;
using XCart.Views.Auth;
using XCart.Views.Home;
using XCart.Views.Product;
using XCart.Views.Profile;
using CommunityToolkit.Maui;

namespace XCart
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().RegisterSqlConfig().RegisterServices().RegisterView().RegisterViewModel().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        public static MauiAppBuilder RegisterSqlConfig(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<DatabaseHelper>();
            return builder;
        }

        public static MauiAppBuilder RegisterView(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<App>();
            builder.Services.AddTransient<RegisterView>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<AddProductView>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<ShopView>();
            builder.Services.AddTransient<CartView>();
            builder.Services.AddTransient<UserProfileView>();
            builder.Services.AddSingleton<MainPage>();
            return builder;
        }

        public static MauiAppBuilder RegisterViewModel(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<AddProductViewModel>();
            builder.Services.AddSingleton<ShopViewModel>();
            builder.Services.AddSingleton<CartViewModel>();
            builder.Services.AddSingleton<ProfileViewModel>();
            return builder;
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            return builder;
        }
    }
}