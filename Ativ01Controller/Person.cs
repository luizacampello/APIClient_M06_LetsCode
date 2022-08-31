using System.ComponentModel.DataAnnotations;

namespace Ativ01Controller
{
    public class Person
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [MaxLength(11, ErrorMessage = "CPF deve conter 11 caracteres")]
        [MinLength(11, ErrorMessage = "CPF deve conter 11 caracteres")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
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
