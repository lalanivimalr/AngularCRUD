using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        AngularCRUD.Models.EmployeeDBEntities EmpDB = new Models.EmployeeDBEntities();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllEmployee()
        {            
            return Json(EmpDB.Employees.ToList(),JsonRequestBehavior.AllowGet);
        }
        public string DeleteEmployee(Models.Employee emp)
        {
            EmpDB.Employees.Remove(EmpDB.Employees.Where(p=>p.Emp_Id == emp.Emp_Id).First());
            EmpDB.SaveChanges();
            return "Record Deleted Sucessfully !!";
        }
        public string AddEmployee(Models.Employee emp)
        {
            EmpDB.Employees.Add(new Models.Employee() { Emp_Age = emp.Emp_Age ,Emp_City=emp.Emp_City,Emp_Name=emp.Emp_Name});
            EmpDB.SaveChanges();
            return "Record Added Sucessfully !!";
        }
    }
}