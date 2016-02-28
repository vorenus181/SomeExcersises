using System.Windows;
using PersonBook.Data;
using PersonBook.Data.Infrastructure;

namespace PersonBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NinjectKernel.Initialize(new NinjectConfiguration());

            DispatcherUnhandledException += App_DispatcherUnhandledException;
            base.OnStartup(e);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error(e.Exception);
            MessageBox.Show(string.Format(PersonBookResources.ApplicationError, e.Exception.Message), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
