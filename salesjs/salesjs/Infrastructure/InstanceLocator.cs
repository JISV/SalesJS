namespace salesjs.Infrastructure
{
    using ViewModels;
    public class InstanceLocator
    {
        //esta clase es para hacer una unica instacia
        //de la MainViewModel a traves del objeto Main
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }

    }
}
