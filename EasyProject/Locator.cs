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
        public ProductViewModel ProductViewModel => Ioc.Default.GetService<ProductViewModel>();
        //public ProductShowViewModel ProductShowViewModel => Ioc.Default.GetService<ProductShowViewModel>();
        public ProductInOutViewModel ProductInOutViewModel => Ioc.Default.GetService<ProductInOutViewModel>();
        public LoginViewModel LoginViewModel => Ioc.Default.GetService<LoginViewModel>();
        public PasswordChangeViewModel PasswordChangeViewModel => Ioc.Default.GetService<PasswordChangeViewModel>();
        public UserAuthViewModel UserAuthViewModel => Ioc.Default.GetService<UserAuthViewModel>();
        public OrderViewModel OrderViewModel => Ioc.Default.GetService<OrderViewModel>();
        public SignupViewModel SignupViewModel => Ioc.Default.GetService<SignupViewModel>();

    }
}
