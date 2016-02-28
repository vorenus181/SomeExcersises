using PersonBook.Data.Dtos.Concrete;
using PersonBook.Data.Model.Concrete;

namespace PersonBook.Data.Mapping.Concrete
{
    public class PersonMapper : Mapper<Person, PersonDto>
    {
        public override PersonDto Map(Person source, PersonDto destination)
        {
            if (destination == null) destination = new PersonDto();

            destination.Name = source.Name;
            destination.Surname = source.Surname;
            destination.BirthDate = source.BirthDate;
            destination.MaritalStatus = source.MaritalStatus;

            base.Map(source, destination);

            return destination;
        }

        public override Person Map(PersonDto source, Person destination)
        {
            if (destination == null) destination = new Person();

            destination.Name = source.Name;
            destination.Surname = source.Surname;
            destination.BirthDate = source.BirthDate;
            destination.MaritalStatus = source.MaritalStatus;

            base.Map(source, destination);

            return destination;
        }
    }
}
