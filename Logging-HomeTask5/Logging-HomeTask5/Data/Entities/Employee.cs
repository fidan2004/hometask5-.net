namespace Logging_HomeTask5.Data.Entities
{
    public class Employee
    {

       public int Id { get; set; }
       public string  Name { get; set; }
       public string Surname { get; set; }
       public DateTime  BirthDate { get; set; }
       public string  Position { get; set; }
       public int Salary { get; set; }
       public bool IsManager { get; set; }
    }
}
