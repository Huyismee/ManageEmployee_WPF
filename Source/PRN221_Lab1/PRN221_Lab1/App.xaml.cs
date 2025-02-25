using AutomobileLibrary.Repositories.Common;
using AutomobileLibrary.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using PRN221_Lab1.Interfaces;
using System.Configuration;
using System.Data;
using System.Windows;
using AutomobileLibrary.Repositories;
using PRN221_Lab1.Models;
using PRN221_Lab1.Services;
using PRN221_Lab1.ViewModels;
using AutoMapper;
using PRN221_Lab1.Mapping;

namespace PRN221_Lab1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            //Config for DependencyInjection (DI)
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

        }

        //---------------------------'-------------------'-------

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<PRN221Context>();
            services.AddScoped(typeof(IGenericRepositories<>), typeof(GenericRepositories<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<MainWindow>();
            services.AddTransient<OrderViewModel>();
            services.AddSingleton<INorthwindRepository, NorthwindRepository>();
            services.AddSingleton<INorthwindService, NorthwindService>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomDtoMapper());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


        }

        //-~-----------------------------------------------------

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var viewModel = serviceProvider.GetService<OrderViewModel>();
            var window = new MainWindow { DataContext = viewModel };
            window.Show();

        }
    }

}
