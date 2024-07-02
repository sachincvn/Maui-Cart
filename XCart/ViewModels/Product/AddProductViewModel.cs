using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using XCart.Models.Product;
using XCart.Services.Product;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using XCart.ViewModels.Abstract;
using XCart.ViewModels.Home;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace XCart.ViewModels.Product
{
    public partial class AddProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly HomeViewModel _homeViewModel;

        public AddProductViewModel( IProductService productService,HomeViewModel homeViewModel)
        {
            _productService = productService;
            _homeViewModel = homeViewModel;
        }

        [ObservableProperty]
        private string? _productName;

        [ObservableProperty]
        private string? _productDescription;

        [ObservableProperty]
        private string? _productImage;

        [ObservableProperty]
        private decimal _productPrice;

        [ObservableProperty]
        private ImageSource? _productImageSource;

        [RelayCommand]
        private async Task AddProductAsync()
        {
            if (!(await ValidateInputs()))
                return;

            var product = new ProductModel
            {
                Name = ProductName,
                Description = ProductDescription,
                Image = ProductImage,
                Price = ProductPrice
            };

            await _productService.AddProductAsync(product);
            await _homeViewModel.InitAsync(product);
            await Shell.Current.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task PickImageAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions { FileTypes = FilePickerFileType.Images, PickerTitle = "Select the product image" });
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    var imagePath = await SaveImageToLocalAsync(stream);
                    ProductImageSource = ImageSource.FromFile(imagePath);
                    ProductImage = imagePath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async Task<bool> ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(ProductName) ||
                string.IsNullOrWhiteSpace(ProductDescription) ||
            string.IsNullOrWhiteSpace(ProductImage) ||
            ProductPrice <= 0)
            {
                await Snackbar.Make("Enter all the requires fields").Show();
                return false;
            }

            return true;
        }

        private async Task<string> SaveImageToLocalAsync(Stream stream)
        {
            var imagePath = Path.Combine(FileSystem.CacheDirectory, $"{Guid.NewGuid()}.jpg");

            using (var fileStream = File.OpenWrite(imagePath))
            {
                await stream.CopyToAsync(fileStream);
            }

            return imagePath;
        }
    }
}
