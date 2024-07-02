using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCart.ViewModels.Product.Items
{
    public partial class ShopItemViewModel : ObservableObject
    {
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
        public int quantity;

        public ShopItemViewModel()
        {
            Quantity = 1;
        }

        [RelayCommand]
        private async Task AddToCartAsync()
        {
        }
    }
}
