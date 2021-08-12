using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ComponentSpace.SAML2;

namespace TestApp2.Controllers
{
	public class HomeController : Controller
	{		
		public ActionResult Index()
		{
			return View();
		}		

		[HttpPost]
		public ActionResult ListCertificateDetails()
		{
			string serialNumber = Request["serialNumValue"].ToString();

			var x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
			x509Store.Open(OpenFlags.ReadOnly);

			var x509CertificateCollection = x509Store.Certificates.Find(X509FindType.FindBySerialNumber, serialNumber, false);

			StringBuilder certDetails = new StringBuilder();

			if (x509CertificateCollection == null || x509CertificateCollection.Count == 0)
			{
				certDetails.Append(string.Format("No certificates with a serial number {0} were found.", serialNumber));
			}
			else
			{				
				foreach (X509Certificate2 x509Certificate in x509CertificateCollection)
				{
					certDetails.Append("<b>Subject: </b> " + x509Certificate.Subject + "<br/>");
					certDetails.Append("<b>Issuer: </b> " + x509Certificate.Issuer + "<br/>");
					certDetails.Append("<b>Expiration date: </b> " + x509Certificate.NotAfter.ToShortDateString() + "<br/>");
					certDetails.Append("<b>Friendly name: </b> " + x509Certificate.FriendlyName + "<br/>");
					certDetails.Append("<b>Serial number: </b> " + x509Certificate.SerialNumber + "<br/>");
					certDetails.Append("<b>Thumbprint: </b> " + x509Certificate.Thumbprint + "<br/>");
					certDetails.Append("<b>Has private key: </b> " + x509Certificate.HasPrivateKey + "<br/>");
				}
			}

			return Content(certDetails.ToString());

		}
    }
}