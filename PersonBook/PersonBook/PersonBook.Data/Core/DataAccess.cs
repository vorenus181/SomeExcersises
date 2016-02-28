using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using PersonBook.Data.Dtos;
using PersonBook.Data.Model;
namespace PersonBook.Data.Core
{
    public class DataAccess : IDataAccess
    {
        #region Fields

        private string _filePath;

        #endregion

        #region Properties

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

        #endregion

        #region IDataAccess members

        public void Write<T>(T data, bool append = false) where T : IEnumerable<Dto>
        {
            if (!CheckIfDataFileExists())
            {
                CreateDataFile();
            }

            using (var stream = new FileStream(FilePath, append ? FileMode.Append : FileMode.Create, FileAccess.Write))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
            }
        }

        public IEnumerable<T> Read<T>() where T : Dto
        {
            if (!CheckIfDataFileExists())
            {
                CreateDataFile();
            }

            using (var stream = new FileStream(FilePath, FileMode.Open))
            {
                stream.Position = 0;
                if (stream.Length > 0)
                {
                    var binaryFormatter = new BinaryFormatter();

                    var items = binaryFormatter.Deserialize(stream);

                    return items as IEnumerable<T>;
                }
            }
            return null;
        }

        #endregion

        #region Private methods

        private void CreateDataFile()
        {
            File.Create(FilePath).Dispose();
        }

        private bool CheckIfDataFileExists()
        {
            return File.Exists(FilePath);
        }

        #endregion
    }
}
