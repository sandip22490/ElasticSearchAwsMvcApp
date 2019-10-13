using Elasticsearch.Net;
using Elasticsearch.Net.Aws;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EsAwsMvcApp.Controllers
{
    class DocumentAttributes
    {
        public string id { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var httpConnection = new AwsHttpConnection("ap-south-1");

            var pool = new SingleNodeConnectionPool(new Uri("https://search-resume-search-es-5prjnwjoxtjmpgkmtmrloe5wkm.ap-south-1.es.amazonaws.com"));
            var config = new ConnectionSettings(pool, httpConnection);
            var client = new ElasticClient(config);

            var getTableData = client.Search<DocumentAttributes>(s => s.Index("sample")); // you can get maximum 10000 records at a time  

            var jsonResult = JsonConvert.SerializeObject("{\"result\":true\"}");
            List<DocumentAttributes> lstData = new List<DocumentAttributes>();

            foreach (var hit in getTableData.Hits)
            {
                lstData.Add(hit.Source);
            }
            if (lstData != null)
            {
                jsonResult = JsonConvert.SerializeObject(getTableData);
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}