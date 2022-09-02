using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Ativ01Controller.Repository
{
    public class PersonRepository
    {
        private readonly IConfiguration _configuration;

        public PersonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Person> GetPersonRepository()
        {
            var query = "SELECT * FROM clientes";

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Person>(query).ToList();
        }

        public Person GetPersonByCPF(string userCPF)
        {
            var query = "SELECT * FROM clientes WHERE cpf = @cpf";

            var parameters = new DynamicParameters(new
            {
                cpf = userCPF,
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.QueryFirstOrDefault<Person>(query, parameters);
        }

        public bool AddPerson(Person newPerson)
        {
            var query = "INSERT INTO clientes VALUES (@cpf, @nome, @dataNascimento, @idade)";

            DynamicParameters parameters = new ();
            parameters.Add("cpf", newPerson.cpf);
            parameters.Add("nome", newPerson.nome);
            parameters.Add("dataNascimento", newPerson.dataNascimento);
            parameters.Add("idade", newPerson.idade);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool EditPersonInfoByCPF(string personCPF, Person changePerson)
        {
            var query = "UPDATE clientes SET nome = @nome, dataNascimento = @dataNascimento, cpf = @newCPF WHERE cpf = @oldCPF;";

            DynamicParameters parameters = new();
            parameters.Add("oldCPF", personCPF);
            parameters.Add("newCPF", changePerson.cpf);
            parameters.Add("nome", changePerson.nome);
            parameters.Add("dataNascimento", changePerson.dataNascimento);
            parameters.Add("idade", changePerson.idade);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeletePersonById(long userId)
        {
            var query = "DELETE FROM clientes WHERE id = @id";

            DynamicParameters parameters = new (new
            {
                id = userId,
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool DeletePersonByCPF(string personCPF)
        {
            var query = "DELETE FROM clientes WHERE cpf = @cpf";

            DynamicParameters parameters = new(new
            {
                cpf = personCPF,
            });

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;

        }
    }
}
