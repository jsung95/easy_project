using EasyProject.ViewModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyProject
{
    public  class Locator
    {
        public ProductViewModel PVM=> Ioc.Default.GetService<ProductViewModel>();
    }
}
