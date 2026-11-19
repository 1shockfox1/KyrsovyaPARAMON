using System;

namespace KyrsovyaPARAMON.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public Person()
        {
            Name = "Иван";
            LastName = "Иванов";
            MiddleName = "Иванович";
        }

        public Person(string name, string lastName, string middleName)
        {
            Name = name;
            LastName = lastName;
            MiddleName = middleName;
        }

        public string GetFIO()
        {
            return $"{LastName} {Name} {MiddleName}";
        }
    }
}
