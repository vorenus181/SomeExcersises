using PersonBook.Data.Infrastructure;
using PersonBook.Main;

namespace PersonBook
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => NinjectKernel.Get<MainViewModel>();
    }
}
