using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using web.Models;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            General vm = new General();
            vm.Customers = GetCustomers(0);   
            return View(vm);
        }

        public ActionResult GetInfo(General GM)
        {
            //ViewBag.Message = "Your application description page.";
            HardwareDBEntities db = new HardwareDBEntities();
            //Retrieve a list of vendors so that it can be used to populate the dropdown on the View
            //The ID of the currently selected item is passed through so that the returned list has that item preselected
           GM.Customers = GetCustomers(GM.ID);

            //Get the full details of the selected vendor so that it can be displayed on the view
            GM.customer = db.lgcustomers.Where(zz => zz.cust_code == GM.ID).FirstOrDefault();

            //Get all supplier orders that adheres to the entered criteria
            //For each of the results, load data into a new ReportRecord object
            var list = db.lginvoices.Where(zz=> zz.cust_code == GM.customer.cust_code)

            //Load the list of ReportRecords returned by the above query into a new list grouped by Shipment Method
           

            //Load the list of ReportRecords returned by the above query into a new dictionary grouped by Employee
            //This will be used to generate the chart on the View through the MicroSoft Charts helper
            

            //Store the chartData dictionary in temporary data so that it can be accessed by the EmployeeOrdersChart action resonsible for generating the chart
            TempData["chartData"] = vm.chartData;
            TempData["records"] = list.ToList();/***We use this list at different Action Result we can use
                tempdata to store the infromation and use somwhere else**/
            TempData["vendor"] = vm.vendor;/**if we wanna be able to use this vendor details at different
                Action Result**/

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public SelectList GetCustomers(int selected)
        {
            using (HardwareDBEntities  db = new HardwareDBEntities())
            {
                
                db.Configuration.ProxyCreationEnabled = false;

                //Create a SelectListItem for each Vendor record in the DB
                //Value is set to the primary key of the record and Text is set to the Name of the vendor
                var Customer = db.lgcustomers.Select(x => new SelectListItem
                {
                    Value = x.cust_code.ToString(),
                    Text = x.cust_fname+" " + x.cust_lname
                }).ToList();

                //If selected pearameter has a value, configure the SelectList so that the apporiate item is preselected
                if (selected == 0)
                    return new SelectList(Customer, "Value", "Text");
                else
                    return new SelectList(Customer, "Value", "Text", selected);
            }
            
        }
        
    }
}