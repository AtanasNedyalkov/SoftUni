using SoftUni.Data;

namespace SoftUni.Models;

public class EmployeeProject

{
	public int EmployeeID { get; set; }
	public virtual Employee? Employee { get; set; }
	public int ProjectID { get; set; }

	public virtual Project? Project { get; set; }
}