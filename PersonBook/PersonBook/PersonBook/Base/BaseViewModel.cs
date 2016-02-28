using System.ComponentModel;
using System.Runtime.CompilerServices;
using PersonBook.Annotations;

namespace PersonBook.Base
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields

        private bool _hasError;

        #endregion


        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        public bool HasError
        {
            get { return _hasError; }
            set
            {
                if (value == _hasError) return;
                _hasError = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
