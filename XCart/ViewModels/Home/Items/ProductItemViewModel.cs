using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCart.Services.Product;
using XCart.ViewModels.Product;
using XCart.ViewModels.Product.Items;

namespace XCart.ViewModels.Home.Items
{
    public partial class ProductItemViewModel : ObservableObject
    {

        private readonly IProductService _productService;
        private readonly ShopViewModel _shopViewModel;
        public ProductItemViewModel(IProductService productService,ShopViewModel shopViewModel)
        {
            _productService = productService;
            _shopViewModel = shopViewModel;
        }

        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        public string? description;

        [ObservableProperty]
        public string? image;

        [ObservableProperty]
        public decimal price;

        [ObservableProperty]
        public bool isInShop;


        [RelayCommand]
        private async Task ToggleProductInShopAsync(ProductItemViewModel product)
        {
            await _productService.UpdateProductAsync(new Models.Product.ProductModel()
            {
                Id = product.Id,
                Description = product.Description,
                Image = product.Image,
                IsInShop = !(product.IsInShop),
                Name = product.Name,
                Price = product.Price,
            });
            product.IsInShop = !product.IsInShop;
            await _shopViewModel.InitAsync(null);
        }
    }
}
