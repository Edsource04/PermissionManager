namespace PermissionManagerAPI.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string? EmployeeName { get;set; }
        public string? EmployeeLastName { get; set; }
        public PermissionType? PermissionType { get; set;  } 
        public DateTime? PermissionDate { get; set; }



    }
}
