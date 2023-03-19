using SoftUni.Data;
using SoftUni.Models;
using System.Linq;
using System.Text;

namespace SoftUni;

public class StartUp
{
    static void Main(string[] args)
    {

        SoftUniContext dbContext = new SoftUniContext();
        //var result = GetEmployeesFullInformation(dbContext);
        //var result = GetEmployeesWithSalaryOver50000(dbContext);
        //var result = GetEmployeesFromResearchAndDevelopment(dbContext);
        //var result = AddNewAddressToEmployee(dbContext);
        var result = DeleteProjectById(dbContext);

		Console.WriteLine(result);
    }
   // prob 3
    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();

        var employee = context.Employees
            .OrderBy(e => e.EmployeeId)
            .Select(e => new
            {
                e.FirstName,
                e.MiddleName,
                e.LastName,
                e.JobTitle,
                e.Salary
            }
             )
            .ToArray();
        foreach (var e in employee)
        {
            sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");

        }
        return sb.ToString().TrimEnd();
    }

    //prob 4
    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();

        var employeesWithBigSalary = context.Employees
                   .OrderBy(e=>e.FirstName)
                   .Select(e => new
                   {
                       e.FirstName,
                       e.Salary
                   })
                   .Where(e=>e.Salary>50000)
                   .ToArray();

        foreach (var e in employeesWithBigSalary)
        {
            sb.AppendLine
                (
                $"{e.FirstName} – {e.Salary:f2}"
                );

        }
        return sb.ToString().TrimEnd();
    }

    //prob 5
    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        StringBuilder sb = new StringBuilder();

        var employeeRAD = context.Employees
            .Where(e => e.Department.Name == "Research and Development")
            .Select(e => new
            {
                e.FirstName,
                e.LastName,
                DepartmentName = e.Department.Name,
                e.Salary
            })
            .OrderBy(e => e.Salary)
            .ThenByDescending(e => e.FirstName)
            .ToArray();

        foreach (var e in employeeRAD)
        {
            sb.AppendLine
                (
                $"{e.FirstName} {e.LastName} from Research and Development - ${e.Salary:f2}"
                );
        }

        return sb.ToString().TrimEnd();
    }

    //prob 6
    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        Address newAddress = new Address()
        {
            AddressText = "Vitoshka 15",
            TownId = 4
        };
        Employee? employee = context.Employees
            .FirstOrDefault(e => e.LastName == "Nakov");
        employee!.Address = newAddress;

        context.SaveChanges();

        var employeeAddress = context.Employees
            .OrderByDescending(e => e.AddressId)
            .Take(10)
            .Select(e => e.Address!.AddressText)
            .ToArray();
        return String.Join(Environment.NewLine, employeeAddress);
    }
    //14
    public static string DeleteProjectById(SoftUniContext context)
    {

        var employeeToDelete = context.EmployeesProjects
            .Where(ep => ep.ProjectID == 2);
        context.EmployeesProjects.RemoveRange(employeeToDelete);

        Project projectToDel = context.Projects.Find(2)!;
        context.Projects.Remove(projectToDel);
        context.SaveChanges ();

        string[] projectNames = context.Projects
            .Take(10)
            .Select (e => e.Name)
            .ToArray();

        return String.Join(Environment.NewLine, projectNames);
    }


}

