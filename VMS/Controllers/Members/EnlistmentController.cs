using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS.Models;
using VMS.ClassUtility;
using System.Data;

namespace VMS.Controllers.Members
{
    [RouteArea("Members")]
    public class EnlistmentController : Controller
    {
        [Route("enlistment")]
        public ActionResult Index()
        {
            if (Authentication.IsSetSession())
            {
                Model_Enlistment me = new Model_Enlistment();
                DataTable dt = me.GetVendorDetails(Session["VENDOR_GUID"].ToString());

                object []ret = new object[6];
                ret[0] = dt.Rows[0].ItemArray[1].ToString();
                ret[1] = dt.Rows[0].ItemArray[2].ToString();
                ret[2] = dt.Rows[0].ItemArray[3].ToString();
                ret[3] = dt.Rows[0].ItemArray[4].ToString();
                ret[4] = dt.Rows[0].ItemArray[5].ToString();
                ret[5] = dt;

                return View("~/Views/Members/enlistment/enlistment.cshtml", ret );
            }
            else
            {
                return RedirectToAction("../../login");
            }            
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            return RedirectToAction("../../login");
        }

        [HttpPost]
        public ActionResult Submit()
        {
            if (!Authentication.IsSetSession())
            {
                return RedirectToAction("../../login");
            }

            List<CertificationFileAttr> certificationFiles = new List<CertificationFileAttr>();
            List<AnnualReportFileAttr> annualReportFile = new List<AnnualReportFileAttr>();

            // FILE UPLOADING STARTS
            foreach (string upload in Request.Files)
            {
                if (!(Request.Files[upload] != null && Request.Files[upload].ContentLength > 0)) continue;

                HttpPostedFileBase file = Request.Files[upload];
                if (file == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    string orgName = Session["VENDOR_NAME"].ToString();
                    checkFolderPath(orgName);

                    string NewFileName = upload.ToString() + DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName).ToLower();
                    if ((Path.GetExtension(file.FileName).ToLower().Contains(".jpg"))
                        || (Path.GetExtension(file.FileName).ToLower().Contains(".jpeg"))
                        || (Path.GetExtension(file.FileName).ToLower().Contains(".png"))
                        || (Path.GetExtension(file.FileName).ToLower().Contains(".pdf")))
                    {
                        //if (upload.ToString().Contains("product_"))
                        //{
                        //    file.SaveAs(Server.MapPath("~/Content/Uploads/" + orgName + "/Products/" + NewFileName));
                        //} else
                        if (upload.ToString().Contains("cer_"))
                        {
                            CertificationFileAttr cer = new CertificationFileAttr();
                            cer.FileName = file.FileName;
                            cer.Ext = Path.GetExtension(file.FileName).ToLower();
                            cer.NewFileName = NewFileName;
                            cer.AbsPath = Server.MapPath("~/Content/Uploads/" + orgName + "/Certifications/" + NewFileName);
                            cer.Path = ("~/Content/Uploads/" + orgName + "/Certifications/" + NewFileName);

                            certificationFiles.Add(cer);

                            file.SaveAs(Server.MapPath("~/Content/Uploads/" + orgName + "/Certifications/" + NewFileName));
                            #region "blocked"
                            //certificationFiles.Add(new List<string>());
                            //certificationFile[certificationFile.Count()].Add(file.FileName);
                            //certificationFile[certificationFile.Count()].Add(Path.GetExtension(file.FileName).ToLower());
                            //certificationFile[certificationFile.Count()].Add(NewFileName);
                            //certificationFile[certificationFile.Count()].Add(orgName + "/Certifications/" + NewFileName);
                            //certificationFile[certificationFile.Count()].Add(Server.MapPath("~/Content/Uploads/" + orgName + "/Certifications/" + NewFileName));
                            #endregion "blocked"
                        }
                        else if (upload.ToString().Contains("annualReport_"))
                        {
                            AnnualReportFileAttr arf = new AnnualReportFileAttr();
                            arf.FileName = file.FileName;
                            arf.Ext = Path.GetExtension(file.FileName).ToLower();
                            arf.NewFileName = NewFileName;
                            arf.AbsPath = Server.MapPath("~/Content/Uploads/" + orgName + "/Certifications/" + NewFileName);
                            arf.Path = ("~/Content/Uploads/" + orgName + "/Certifications/" + NewFileName);

                            annualReportFile.Add(arf);

                            file.SaveAs(Server.MapPath("~/Content/Uploads/" + orgName + "/AnnualReport/" + NewFileName));
                            #region "blocked"
                            //annualReportFile.Add(new List<string>());
                            //annualReportFile[annualReportFile.Count()].Add(file.FileName);
                            //annualReportFile[annualReportFile.Count()].Add(Path.GetExtension(file.FileName).ToLower());
                            //annualReportFile[annualReportFile.Count()].Add(NewFileName);
                            //annualReportFile[annualReportFile.Count()].Add(orgName + "/AnnualReport/" + NewFileName);
                            //annualReportFile[annualReportFile.Count()].Add(Server.MapPath("~/Content/Uploads/" + orgName + "/AnnualReport/" + NewFileName));
                            #endregion "blocked"
                        }
                    }
                }
            }
            // FILE UPLOADING ENDS

            //// FORM DATA BEGINS
            string vendorGuid = Session["VENDOR_GUID"].ToString();

            string tradeLicense = Request.Form["txtTradeLicense"];
            string tinNo = Request.Form["txtTinNo"];
            string telNo = Request.Form["txtTelNo"];
            string cellNo = Request.Form["txtCellNo"];
            string faxNo= Request.Form["txtFaxNo"];
            string email = Request.Form["txtEmail"];

            string companyType = Request.Form["ddlOrgType"];
            string []companyTypeBreak = companyType.Split(',');

            string companyCategory = Request.Form["ddlOrgCategory"];
            string []companyCategoryBreak = companyCategory.Split(',');

            string yoe = Request.Form["txtYOE"];

            string aSYear1 = Request.Form["aSYear1"];
            string aSYear1Amnt = Request.Form["aSYear1Amnt"];
            string aSYear2 = Request.Form["aSYear2"];
            string aSYear2Amnt = Request.Form["aSYear2Amnt"];
            string aSYear3 = Request.Form["aSYear3"];
            string aSYear3Amnt = Request.Form["aSYear3Amnt"];

            string bankName = Request.Form["txtBankName"];
            string bankAddress = Request.Form["txtBankAddress"];
            string bankAccountNumber = Request.Form["txtBankAccountNumber"];
            string bankAccountName = Request.Form["txtBankAccountName"];
            string bankSwiftCode = Request.Form["txtBankSwiftCode"];
            string bankRoutingNo = Request.Form["txtBankRoutingNo"];

            string listOfc = Request.Form["txtListOfc"];
            string []listOfcBreak = listOfc.Split(';');

            string listImportCountries = Request.Form["txtListImportCountries"];
            string []listImportCountriesBreak = listImportCountries.Split(';');

            // EMPLOYEE INFORMATION
            for(int i = 0; i < Convert.ToInt32(Request.Form["expEmpCount"]); i++ )
            {
                string expArea = Request.Form["expEmpArea" + i].ToString();
                string empNo = Request.Form["expEmpNumber" + i].ToString();
                string remarks = Request.Form["expEmpRemarks" + i].ToString();

            }

            // ORGANIZATION EXPERIENCE INFORMATION
            for (int i = 0; i < Convert.ToInt32(Request.Form["intContactsCount"]); i++)
            {
                string intContactsOrg = Request.Form["intContactsOrg" + i].ToString();
                string intContactsVal = Request.Form["intContactsVal" + i].ToString();
                string intContactsYr = Request.Form["intContactsYr" + i].ToString();
                string intContactsGoods = Request.Form["intContactsGoods" + i].ToString();
                string intContactsDestination = Request.Form["intContactsDestination" + i].ToString();

            }

            // CERTIFICATIONS INFORMATION
            for (int i = 0; i < Convert.ToInt32(Request.Form["attachmentCount"]); i++)
            {
                string attachmentName = Request.Form["attachmentName" + i].ToString();

            }

            // MACHINE INFORMATION
            for (int i = 0; i < Convert.ToInt32(Request.Form["machineCount"]); i++)
            {
                string machineName = Request.Form["machineName" + i].ToString();
                string machineBrand = Request.Form["machineBrand" + i].ToString();
                string machineOrigin = Request.Form["machineOrigin" + i].ToString();
                string machinePurpose = Request.Form["machinePurpose" + i].ToString();
                string machineQuantity = Request.Form["machineQuantity" + i].ToString();
                string machineUOM = Request.Form["machineUOM" + i].ToString();

            }
            //// FORM DATA ENDS

            return Json(new { Response = "True" });
        }

        protected bool checkFolderPath(string orgName)
        {
            try
            {
                string folder = Server.MapPath("~/Content/Uploads/" + orgName + "/Products/");
                if (!Directory.Exists(folder))
                { Directory.CreateDirectory(folder); }

                folder = Server.MapPath("~/Content/Uploads/" + orgName + "/VCards/");
                if (!Directory.Exists(folder))
                { Directory.CreateDirectory(folder); }

                folder = Server.MapPath("~/Content/Uploads/" + orgName + "/Certifications/");
                if (!Directory.Exists(folder))
                { Directory.CreateDirectory(folder); }

                folder = Server.MapPath("~/Content/Uploads/" + orgName + "/AnnualReport/");
                if (!Directory.Exists(folder))
                { Directory.CreateDirectory(folder); }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}