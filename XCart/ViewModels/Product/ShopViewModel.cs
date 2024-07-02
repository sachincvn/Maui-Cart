using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCart.Services.Product;
using XCart.ViewModels.Abstract;
using XCart.ViewModels.Product.Items;

namespace XCart.ViewModels.Product
{
    public partial class ShopViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<ShopItemViewModel> products;

        private readonly IProductService _productService;
        public ShopViewModel(IProductService productService)
        {
            _productService = productService;
        }

        public override async Task InitAsync(object? parameter)
        {
            Products = new ObservableCollection<ShopItemViewModel>();
            var shopItems = await _productService.GetAllProductsAsync();
            foreach (var item in shopItems)
            {
                if (item.IsInShop)
                {
                    Products.Add(new ShopItemViewModel()
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Image = item.Image,
                        Name = item.Name,
                        Price = item.Price,
                    });
                }
            }
        }
    }
}
