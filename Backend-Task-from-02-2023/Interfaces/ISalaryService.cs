namespace Backend.Interfaces
{
    public interface ISalaryService
    {
        void ReadSalary();

        void PrintSalary();

        string CalculateSalary(string PersonName, decimal grossSalary);
    }
}
