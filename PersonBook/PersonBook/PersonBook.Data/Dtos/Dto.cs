using System;

namespace PersonBook.Data.Dtos
{
    [Serializable]
    public class Dto
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}