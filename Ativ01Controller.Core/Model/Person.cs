using System.ComponentModel.DataAnnotations;

namespace Ativ01Controller
{
    public class Person
    {
        public long id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string nome { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF deve conter 11 dígitos")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "Data de nascimento é obrigatória")]
        public DateTime dataNascimento { get; set; }

        public int idade => CalculateAge(dataNascimento);

        public Person()
        {

        }

        public Person(long id, string name, string CPF, DateTime birthDate, int age)
        {
            nome = name;
            this.cpf = CPF;
            dataNascimento = birthDate;
        }

        public int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Now;
            int age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}

