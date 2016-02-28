using System.Collections.Generic;
using PersonBook.Data.Dtos;
using PersonBook.Data.Model;

namespace PersonBook.Data.Core
{
    public interface IDataAccess
    {
        /// <summary>
        /// Write collection
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="data">Collection to write</param>
        /// <param name="append">Append or overwrite</param>
        void Write<T>(T data, bool append = false) where T : IEnumerable<Dto>;
        IEnumerable<T> Read<T>() where T : Dto;
    }
}