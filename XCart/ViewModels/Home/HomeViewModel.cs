using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCart.Models.Product;
using XCart.Services.Product;
using XCart.ViewModels.Abstract;
using XCart.ViewModels.Home.Items;
using XCart.ViewModels.Product;
using XCart.Views.Product;

namespace XCart.ViewModels.Home
{
    public partial class HomeViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<ProductItemViewModel>? _products;

        private readonly IProductService _productService;
        private readonly ShopViewModel _shopViewModel;
        public HomeViewModel(IProductService productService,ShopViewModel shopViewModel)
        {
            _productService = productService;
            _shopViewModel = shopViewModel;
            Products = [];
        }

        public override async Task InitAsync(object? parameter)
        {
            if (parameter is ProductModel || parameter != null)
            {
                var item = parameter as ProductModel;
                Products!.Add(new ProductItemViewModel(_productService,_shopViewModel) { Id = item!.Id, Description = item.Description, Image = item.Image, IsInShop = item.IsInShop, Name = item.Name, Price = item.Price });
                return;
            }
            var storedProducts = await _productService.GetAllProductsAsync();
            if (storedProducts!=null)
            {
                Products = [];
                foreach (var item in storedProducts)
                {
                    Products.Add(new ProductItemViewModel(_productService,_shopViewModel) { Id = item.Id, Description = item.Description, Image = item.Image, IsInShop = item.IsInShop, Name = item.Name, Price = item.Price});
                }
            }
        }

        [RelayCommand]
        private async Task AddProductAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddProductView), true);
        }
    }
}
