using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonBook.Data.Core;
using PersonBook.Data.Dtos.Concrete;
using PersonBook.Data.Enums;
using PersonBook.Data.Infrastructure;
using PersonBook.Data.Model.Concrete;
using PersonBook.Data.Repositories;

namespace PersonBook.Tests
{
    [TestClass]
    public class DataAccessTest
    {
        private string _filePath;

        public string FilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_filePath))
                {
                    var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        _filePath = Path.Combine(directory, "PersonBookData.bin");
                    }
                }
                return _filePath;
            }
            set { _filePath = value; }
        }

        public DataAccessTest()
        {
            if (CheckIfDataFileExists())
            {
                RemoveFile();
            }

            Initialize();
        }

        private static void Initialize()
        {
            NinjectKernel.Initialize(new NinjectConfiguration());
        }

        private static List<PersonDto> GetSampleList()
        {
            return new List<PersonDto>
            {
                new PersonDto
                {
                    Id = Guid.NewGuid(),
                    BirthDate = DateTime.Now,
                    MaritalStatus = MaritalStatus.Single,
                    Name = "John", Surname = "Kowalsky",
                    CreationDate = DateTime.Now.AddMinutes(-1)
                },
                new PersonDto
                {
                    Id = Guid.NewGuid(),
                    BirthDate = DateTime.Now.AddYears(-10),
                    MaritalStatus = MaritalStatus.Single,
                    Name = "John", Surname = "Kowalsky",
                    CreationDate = DateTime.Now.AddMinutes(-2)
                },
                new PersonDto
                {
                    Id = Guid.NewGuid(),
                    BirthDate = DateTime.Now.AddYears(-15),
                    MaritalStatus = MaritalStatus.Married,
                    Name = "Adam", Surname = "Kowalsky",
                    CreationDate = DateTime.Now.AddMinutes(-3)
                },
                new PersonDto
                {
                    Id = Guid.NewGuid(),
                    BirthDate = DateTime.Now.AddYears(-20),
                    MaritalStatus = MaritalStatus.Married,
                    Name = "Lindsey", Surname = "Kowalsky",
                    CreationDate = DateTime.Now.AddMinutes(-4)
                }
            };
        }

        private static PersonDto GetSamplePersonDto()
        {
            return new PersonDto
            {
                Id = new Guid("ACB43F5F-94DF-44BA-B3D1-C8FD6501F7D7"),
                BirthDate = DateTime.Now.AddYears(-20),
                MaritalStatus = MaritalStatus.Married,
                Name = "Rony",
                Surname = "Sample",
                CreationDate = DateTime.Now
            };
        }

        private static Person GetSamplePerson()
        {
            return new Person
            {
                BirthDate = DateTime.Now.AddYears(-20),
                MaritalStatus = MaritalStatus.Married,
                Name = "Rony",
                Surname = "Sample",
            };
        }

        [TestMethod]
        public void WriteNotFaillingTest()
        {
            var dataAccess = new DataAccess();
            dataAccess.Write(GetSampleList());

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void WriteReadTest()
        {
            var dataAccess = new DataAccess();

            var inList = GetSampleList();
            dataAccess.Write(inList);

            var outList = dataAccess.Read<PersonDto>();

            var areEqual = inList.SequenceEqual(outList, new PersonComparer());

            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void AddPersonNotFaillingTest()
        {
            InitializeData();

            var samplePerson = GetSamplePersonDto();
            var dataAccess = new DataAccess();
            dataAccess.Write(new List<PersonDto> { samplePerson });

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AddPersonTest()
        {
            InitializeData();

            var person = GetSamplePerson();
            Person outPerson = null;

            var repoProxy = new RepositoryProxy<Person, PersonDto>();
            repoProxy.Execute(repo => repo.Create(person));
            repoProxy.Execute(repo =>
            {
                outPerson = repo.Read(person.Id);
            });

            if (outPerson == null)
            {
                Assert.Fail();
            }

            if (outPerson.Name != person.Name
                || outPerson.Surname != person.Surname
                || outPerson.BirthDate != person.BirthDate
                || outPerson.MaritalStatus != person.MaritalStatus)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdatePersonTest()
        {
            InitializeData();

            var person = GetSamplePerson();
            Person outPerson = null;
            var repoProxy = new RepositoryProxy<Person, PersonDto>();
            repoProxy.Execute(repo => repo.Create(person));

            person.MaritalStatus = MaritalStatus.Divorcee;

            repoProxy.Execute(repo =>
            {
                repo.Update(person);
                outPerson = repo.Read(person.Id);
            });

            if (outPerson == null || outPerson.Name != person.Name
                || outPerson.Surname != person.Surname
                || outPerson.BirthDate != person.BirthDate
                || outPerson.MaritalStatus != person.MaritalStatus)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DeletePersonTest()
        {
            InitializeData();

            Person outPerson = null;
            var person = GetSamplePerson();
            var repoProxy = new RepositoryProxy<Person, PersonDto>();
            repoProxy.Execute(repo =>
            {
                repo.Create(person);
                outPerson = repo.Read(person.Id);
            });

            if (outPerson == null)
            {
                Assert.Fail();
            }

            repoProxy.Execute(repo =>
            {
                repo.Delete(outPerson.Id);
                outPerson = repo.Read(outPerson.Id);
            });

            if (outPerson != null)
            {
                Assert.Fail();
            }
        }

        private void CreateDataFile()
        {
            File.Create(FilePath).Dispose();
        }

        private bool CheckIfDataFileExists()
        {
            return File.Exists(FilePath);
        }

        private void RemoveFile()
        {
            File.Delete(FilePath);
        }

        private void InitializeData()
        {
            if (CheckIfDataFileExists())
            {
                RemoveFile();
            }

            var dataAccess = new DataAccess();

            var inList = GetSampleList();
            dataAccess.Write(inList);
        }
    }

    public class PersonComparer : IEqualityComparer<PersonDto>
    {
        public bool Equals(PersonDto x, PersonDto y)
        {
            if (x == null && y == null)
                return true;
            if (x == null | y == null)
                return false;

            return x.Name == y.Name & x.Surname == y.Surname & x.BirthDate == y.BirthDate &
                   x.MaritalStatus == y.MaritalStatus;
        }

        public int GetHashCode(PersonDto obj)
        {
            var hCode = obj.Name.GetHashCode() ^ obj.Surname.GetHashCode() ^ obj.BirthDate.GetHashCode() ^
                        obj.MaritalStatus.GetHashCode();

            return hCode.GetHashCode();
        }
    }
}
