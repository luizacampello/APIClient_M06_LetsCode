namespace Ativ01Controller
{
    public class Person
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }


        public Person(string name, string CPF, DateTime birthDate)
        {
            Name = name;
            this.CPF = CPF;
            BirthDate = birthDate;
            Age = CalculateAge(birthDate);
        }

        private int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Now;
            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears ( - age))
            {
                age--;
            }      

            return age;
        }

    }
}
