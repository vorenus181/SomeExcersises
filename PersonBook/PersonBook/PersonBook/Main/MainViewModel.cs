using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using PersonBook.Base;
using PersonBook.Data;
using PersonBook.Data.Dtos.Concrete;
using PersonBook.Data.Enums;
using PersonBook.Data.Model.Concrete;
using PersonBook.Data.Repositories;

namespace PersonBook.Main
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IRepositoryProxy<Person, PersonDto> _repositoryProxy;

        #region Fields

        private ObservableCollection<Person> _persons;
        private Person _currentPerson;
        private bool _isEditMode;
        private bool _canBeEditedOrDeleted;

        #endregion

        #region Properties

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set
            {
                if (_persons == value) return;
                _persons = value;
                OnPropertyChanged();
            }
        }

        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set
            {
                if (_currentPerson == value) return;
                _currentPerson = value;
                OnPropertyChanged();
                CanBeEditedOrDeleted = true;
            }
        }

        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                if (value == _isEditMode) return;
                _isEditMode = value;
                OnPropertyChanged();
            }
        }

        public bool CanBeEditedOrDeleted
        {
            get { return _canBeEditedOrDeleted; }
            set
            {
                if (value == _canBeEditedOrDeleted) return;
                _canBeEditedOrDeleted = value && CurrentPerson != null;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public MainViewModel(IRepositoryProxy<Person, PersonDto> repositoryProxy)
        {
            _repositoryProxy = repositoryProxy;
            LoadData();
            InitializeCommands();
            IsEditMode = false;
        }

        #endregion

        #region Private methods

        private void LoadData()
        {
            _repositoryProxy.Execute(repo =>
            {
                var persons = repo.ReadAll();
                Persons = persons != null
                    ? new ObservableCollection<Person>(persons)
                    : new ObservableCollection<Person>();
            });
        }

        private void InitializeCommands()
        {
            AddCommand = new RelayCommand(AddExecute);
            EditCommand = new RelayCommand(EditExecute);
            CancelCommand = new RelayCommand(CancelExecute);
            SaveCommand = new RelayCommand(SaveExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
            ExportToFile = new RelayCommand(ExportToFileExecute);
            ImportFromFile = new RelayCommand(ImportFromFileExecute);
        }

        #endregion

        #region Commands

        public ICommand AddCommand { get; internal set; }
        public ICommand EditCommand { get; internal set; }
        public ICommand CancelCommand { get; internal set; }
        public ICommand SaveCommand { get; internal set; }
        public ICommand DeleteCommand { get; internal set; }
        public ICommand ExportToFile { get; set; }
        public ICommand ImportFromFile { get; set; }

        public void AddExecute(object parameter)
        {
            CurrentPerson = new Person()
            {
                IsNew = true,
                BirthDate = DateTime.Now,
                CreationDate = DateTime.Now,
                MaritalStatus = MaritalStatus.Single
            };
            Persons.Add(CurrentPerson);
            EditExecute(null);
        }

        public void DeleteExecute(object parameter)
        {
            _repositoryProxy.Execute(repo =>
            {
                repo.Delete(CurrentPerson.Id);
            });

            Persons.Remove(CurrentPerson);

            CurrentPerson = Persons.LastOrDefault();
        }

        public void SaveExecute(object parameter)
        {
            if (HasError)
                return;

            _repositoryProxy.Execute(repo =>
            {
                if (CurrentPerson == null) return;

                if (CurrentPerson.IsNew)
                {
                    if (Persons.Count > 10)
                    {
                        var messageBoxResult = MessageBox.Show(
                            PersonBookResources.MainViewModel_SaveCommand_Message,
                            PersonBookResources.MainViewModel_SaveCommand_MessageTitle,
                            MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (messageBoxResult == MessageBoxResult.No)
                        {
                            CancelExecute(null);
                            return;
                        }

                        var personToDelete = Persons.OrderBy(a => a.CreationDate).FirstOrDefault();
                        if (personToDelete != null)
                        {
                            repo.Delete(personToDelete.Id);
                            Persons.Remove(personToDelete);
                        }
                    }

                    repo.Create(CurrentPerson);
                    CurrentPerson.IsNew = false;
                }
                else
                {
                    repo.Update(CurrentPerson);
                }
            });
            CanBeEditedOrDeleted = true;
            IsEditMode = false;
        }

        public void EditExecute(object parameter)
        {
            IsEditMode = true;
            CanBeEditedOrDeleted = false;
        }

        public void CancelExecute(object parameter)
        {
            if (CurrentPerson != null && CurrentPerson.IsNew)
            {
                Persons.Remove(CurrentPerson);
                if (Persons != null)
                {
                    CurrentPerson = Persons.LastOrDefault();
                }
            }
            OnPropertyChanged(nameof(CurrentPerson));
            IsEditMode = false;
            CanBeEditedOrDeleted = true;
        }

        public void ExportToFileExecute(object obj)
        {
            var saveDialog = new SaveFileDialog { Filter = "Text file (*.txt)|*.txt" };
            if (saveDialog.ShowDialog() != true) return;

            var serializeObject = JsonConvert.SerializeObject(Persons);
            File.WriteAllText(saveDialog.FileName, serializeObject);
        }

        public void ImportFromFileExecute(object obj)
        {
            var messageBoxResult = MessageBox.Show(PersonBookResources.MainWindow_MessageBox_EnsureImportData,
                PersonBookResources.MainWindow_MessageBox_EnsureImportDataTitle,
                MessageBoxButton.OKCancel,
                MessageBoxImage.Asterisk);

            if (messageBoxResult == MessageBoxResult.Cancel) return;

            var openFileDialog = new OpenFileDialog
            {
                Title = PersonBookResources.MainWindow_MessageBox_EnsureImportDataTitle,
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() != true) return;

            using (var streamReader = new StreamReader(openFileDialog.FileName))
            {
                var serializedList = streamReader.ReadToEnd();
                if (!string.IsNullOrEmpty(serializedList))
                {
                    var personsFromFile = JsonConvert.DeserializeObject<List<Person>>(serializedList);
                    Persons = new ObservableCollection<Person>(personsFromFile);
                    _repositoryProxy.Execute(repo => repo.WriteAll(Persons));
                }
            }
        }

        #endregion
    }
}
