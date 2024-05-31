using System.ComponentModel.DataAnnotations;

namespace Edumin.Models
{
    public class Student
    {
        [Key] // Define a propriedade abaixo como chave primária
        public int StudentId { get; set; }

        [Required] // Torna o campo obrigatório
        [StringLength(100, MinimumLength = 3)] // Define restrições para o comprimento
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)] // Especifica o tipo de dados como data
        public DateTime DateOfBirth { get; set; }

        // Relacionamento opcional: um estudante pode estar inscrito em vários cursos
        //public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
