using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCart.ViewModels.Abstract
{
    public abstract class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
            Task.Run(async () => await InitAsync(null));
        }

        public virtual Task InitAsync(object? parameter)
        {
            return Task.CompletedTask;
        }
    }
}
