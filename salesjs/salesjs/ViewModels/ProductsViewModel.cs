

namespace salesjs.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using salesjs.Helpers;
    using Services;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel

    {

        private ApiService apiService;



        private bool isRefreshing;



        private ObservableCollection<Product> products;



        public ObservableCollection<Product> Products

        {

            get { return this.products; }

            set { this.SetValue(ref this.products, value); }

        }



        public bool IsRefreshing

        {

            get { return this.isRefreshing; }

            set { this.SetValue(ref this.isRefreshing, value); }

        }



        public ProductsViewModel()

        {

            this.apiService = new ApiService();

            this.LoadProducts();

        }



        private async void LoadProducts()

        {

            this.IsRefreshing = true;
            //validamos que haya connecion a internet
            var connection = await this.apiService.CheckConnection();
            if(!connection.IsSuccess)
            {
                //no hay connecion y le doy error
                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);

                return;
            }

            //para no hardcode la direccion 
            //la ponemos en el diccionario de recursos

            //var response = await this.apiService.GetList<Product>("https://salesjsapi.azurewebsites.net", "/api", "/Products");

            //creamos una variable url y traemos la direccion del diccionario
            //se pone ToString porque el devuelve un objeto y debemos ponerlo en string
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetList<Product>(url, "/api", "/Products");

            if (!response.IsSuccess)

            {

                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message,Languages.Accept);

                return;

            }


            //como la lista llega en un objeto tipo object
            //la casteo a una lista
            var list = (List<Product>)response.Result;
            //como lo que se pinta es un observable collection
            //convierto la lista a una observablecollection
            this.Products = new ObservableCollection<Product>(list);

            this.IsRefreshing = false;

        }



        public ICommand RefreshCommand

        {

            get

            {

                return new RelayCommand(LoadProducts);

            }

        }

    }
}