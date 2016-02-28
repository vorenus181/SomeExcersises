using System;
using PersonBook.Data.Enums;

namespace PersonBook.Data.Dtos.Concrete
{
    [Serializable]
    public class PersonDto : Dto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
    }
}
