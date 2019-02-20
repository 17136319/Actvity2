using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web.Models
{
    public class General
    {
        //Fields for report criteria
        public IEnumerable<SelectListItem> Customers { get; set; }
        public int ID { get; set; }
        public DateTime MaxDate { get; set; }
        public DateTime MinDate { get; set; }

        //Fields for report data
        public lgcustomer customer { get; set; }
        public List<IGrouping<string, Report>> results { get; set; }
        public Dictionary<string, double> chartData { get; set; }
    }
    //this reports the sale info
    public class Report
    {
        public int total { get; set; }
    }
}