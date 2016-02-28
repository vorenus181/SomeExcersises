using System;
using PersonBook.Data.Enums;

namespace PersonBook.Data.Model.Concrete
{
    public class Person : SerializableEntity
    {
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        private MaritalStatus _maritalStatus;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                if (value == _surname) return;
                _surname = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (value.Equals(_birthDate)) return;
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        public MaritalStatus MaritalStatus
        {
            get { return _maritalStatus; }
            set
            {
                if (value == _maritalStatus) return;
                _maritalStatus = value;
                OnPropertyChanged();
            }
        }
    }
}
