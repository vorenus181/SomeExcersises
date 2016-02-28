using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PersonBook.Data.Annotations;

namespace PersonBook.Data.Model
{
    public class SerializableEntity : INotifyPropertyChanged
    {
        private DateTime _creationDate;

        public Guid Id { get; set; }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                if (value.Equals(_creationDate)) return;
                _creationDate = value;
                OnPropertyChanged();
            }
        }

        public bool IsNew { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
