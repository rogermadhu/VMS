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
                DataTable dt2 = me.GetVendorContact(Session["VENDOR_GUID"].ToString());
                object []ret = new object[2];
                ret[0] = dt;
                ret[1] = dt2;
                return View("~/Views/Members/enlistment/enlistment.cshtml", ret );
            }
            else
            {
                return RedirectToAction("Login");
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
                    string orgName = Session["VENDOR_GUID"].ToString();//Session["VENDOR_NAME"].ToString();
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

            //string tradeLicense = Request.Form["txtTradeLicense"];
            //string tinNo = Request.Form["txtTinNo"];
            //string telNo = Request.Form["txtTelNo"];
            //string cellNo = Request.Form["txtCellNo"];
            //string faxNo= Request.Form["txtFaxNo"];
            //string email = Request.Form["txtEmail"];
            //string yoe = Request.Form["txtYOE"];

            VendorTbl vendor = new VendorTbl();
            vendor.VendorGuid = vendorGuid;
            vendor.TradeLicense = Request.Form["tradeLicense"];
            vendor.TinNo = Request.Form["tinNo"];
            vendor.TelNo = Request.Form["telNo"];
            vendor.CellNo = Request.Form["cellNo"];
            vendor.FaxNo = Request.Form["faxNo"];
            vendor.TotalNoOfEmployee = Request.Form["totalEmployee"];
            vendor.DateOfEstablishment = Request.Form["yoe"];
            vendor.UpdateVendorTbl();

            string companyType = Request.Form["orgType"];
            if (companyType.Contains(","))
            {
                string[] companyTypeBreak = companyType.Split(',');
                for (int i = 0; i < companyTypeBreak.Length; i++)
                {
                    VendorType vt = new VendorType();
                    vt.VendorId = vendorGuid;
                    vt.VendorTypeName = companyTypeBreak[i].ToString();
                    vt.InsertVendorType();
                }
            }
            else
            {
                VendorType vt = new VendorType();
                vt.VendorId = vendorGuid;
                vt.VendorTypeName = companyType;
                vt.InsertVendorType();
            }


            string companyCategory = Request.Form["orgCategory"];
            if (companyCategory.Contains(","))
            {
                string[] companyCategoryBreak = companyCategory.Split(',');
                for (int i = 0; i < companyCategoryBreak.Length; i++)
                {
                    VCT_Map vtc = new VCT_Map();
                    vtc.VendorId = vendorGuid;
                    vtc.VendorCompanyType = companyCategoryBreak[i].ToString();
                    vtc.InsertVendorCompanyType();
                }
            }
            else
            {
                VCT_Map vtc = new VCT_Map();
                vtc.VendorId = vendorGuid;
                vtc.VendorCompanyType = companyCategory;
                vtc.InsertVendorCompanyType();
            }


            //string aSYear1 = Request.Form["aSYear1"];
            //string aSYear1Amnt = Request.Form["aSYear1Amnt"];
            //string aSYear2 = Request.Form["aSYear2"];
            //string aSYear2Amnt = Request.Form["aSYear2Amnt"];
            //string aSYear3 = Request.Form["aSYear3"];
            //string aSYear3Amnt = Request.Form["aSYear3Amnt"];
            AnnualSales as1 = new AnnualSales();
            as1.VendorId = vendorGuid;
            as1.Year = Request.Form["annualSalesY1"];
            as1.Value = Request.Form["annualSalesY1Amnt"];
            as1.InsertAnnualSales();
            AnnualSales as2 = new AnnualSales();
            as2.VendorId = vendorGuid;
            as2.Year = Request.Form["annualSalesY2"];
            as2.Value = Request.Form["annualSalesY2Amnt"];
            as2.InsertAnnualSales();
            AnnualSales as3 = new AnnualSales();
            as3.VendorId = vendorGuid;
            as3.Year = Request.Form["annualSalesY3"];
            as3.Value = Request.Form["annualSalesY3Amnt"];
            as3.InsertAnnualSales();

            
            //string bankName = Request.Form["txtBankName"];
            //string bankAddress = Request.Form["txtBankAddress"];
            //string bankAccountNumber = Request.Form["txtBankAccountNumber"];
            //string bankAccountName = Request.Form["txtBankAccountName"];
            //string bankSwiftCode = Request.Form["txtBankSwiftCode"];
            //string bankRoutingNo = Request.Form["txtBankRoutingNo"];
            VFI vfi = new VFI();
            vfi.VendorId = vendorGuid;
            vfi.BankName = Request.Form["bankName"];
            vfi.BankAddress = Request.Form["bankAddress"];
            vfi.AccountNo = Request.Form["bankAccountNumber"];
            vfi.AccountName = Request.Form["bankAccountName"];
            vfi.SwiftCode = Request.Form["bankSwiftCode"];
            vfi.RoutingNo = Request.Form["bankRoutingNo"];
            vfi.InsertBankInformation();


            string listOfc = Request.Form["listOfc"];
            string []listOfcBreak = listOfc.Split(';');
            for(int i = 0; i < listOfcBreak.Length; i++)
            {
                IntlOfc ofc = new IntlOfc();
                ofc.VendorId = vendorGuid;
                ofc.Country = listOfcBreak[i].ToString();
                ofc.Insert_Intl_Ofc();
            }


            string listImportCountries = Request.Form["listImportCountries"];
            if (companyCategory.Contains(";"))
            {
                string[] listImportCountriesBreak = listImportCountries.Split(';');
                for (int i = 0; i < listImportCountriesBreak.Length; i++)
                {
                    CountryRawMaterialsFrom ofc = new CountryRawMaterialsFrom();
                    ofc.VendorId = vendorGuid;
                    ofc.Country = listImportCountriesBreak[i].ToString();
                    ofc.InsertRawMaterialList();
                }
            }
            else
            {
                CountryRawMaterialsFrom ofc = new CountryRawMaterialsFrom();
                ofc.VendorId = vendorGuid;
                ofc.Country = listImportCountries;
                ofc.InsertRawMaterialList();
            }


            // EMPLOYEE INFORMATION
            for (int i = 0; i < Convert.ToInt32(Request.Form["expEmpCount"]); i++ )
            {
                //string expArea = Request.Form["expEmpArea" + i].ToString();
                //string empNo = Request.Form["expEmpNumber" + i].ToString();
                //string remarks = Request.Form["expEmpRemarks" + i].ToString();

                EmployeeInformation ei = new EmployeeInformation();
                ei.VendorId = vendorGuid;
                ei.ExpArea = Request.Form["expEmpArea" + i].ToString();
                ei.NoOfEmployee = Request.Form["expEmpNumber" + i].ToString();
                ei.Remarks = Request.Form["expEmpRemarks" + i].ToString();
                ei.InsertEmployeeInformation();
            }
            
            // ORGANIZATION EXPERIENCE INFORMATION
            for (int i = 0; i < Convert.ToInt32(Request.Form["intContactsCount"]); i++)
            {
                //string intContactsOrg = Request.Form["intContactsOrg" + i].ToString();
                //string intContactsVal = Request.Form["intContactsVal" + i].ToString();
                //string intContactsYr = Request.Form["intContactsYr" + i].ToString();
                //string intContactsGoods = Request.Form["intContactsGoods" + i].ToString();
                //string intContactsDestination = Request.Form["intContactsDestination" + i].ToString();

                VendorExp exp = new VendorExp();
                exp.VendorId = vendorGuid;
                exp.Org = Request.Form["intContactsOrg" + i].ToString();
                exp.Amount = Request.Form["intContactsVal" + i].ToString();
                exp.Year = Request.Form["intContactsYr" + i].ToString();
                exp.Goods = Request.Form["intContactsGoods" + i].ToString();
                exp.Destination = Request.Form["intContactsDestination" + i].ToString();
                exp.InsertVendorExp();
            }

            // CERTIFICATIONS INFORMATION
            for (int i = 0; i < Convert.ToInt32(Request.Form["attachmentCount"]); i++)
            {
                //string attachmentName = Request.Form["attachmentName" + i].ToString();
                Certification cer = new Certification();
                cer.VendorId = vendorGuid;
                cer.CertificationName = Request.Form["attachmentName" + i].ToString();
                cer.InsertCertification();
            }

            // MACHINE INFORMATION
            for (int i = 0; i < Convert.ToInt32(Request.Form["machineCount"]); i++)
            {
                //string machineName = Request.Form["machineName" + i].ToString();
                //string machineBrand = Request.Form["machineBrand" + i].ToString();
                //string machineOrigin = Request.Form["machineOrigin" + i].ToString();
                //string machinePurpose = Request.Form["machinePurpose" + i].ToString();
                //string machineQuantity = Request.Form["machineQuantity" + i].ToString();
                //string machineUOM = Request.Form["machineUOM" + i].ToString();

                Machine m = new Machine();
                m.VendorId = vendorGuid;
                m.MachineName = Request.Form["machineName" + i].ToString();
                m.BrandName = Request.Form["machineBrand" + i].ToString();
                m.Origin = Request.Form["machineOrigin" + i].ToString();
                m.Purpose = Request.Form["machinePurpose" + i].ToString();
                m.Quantity = Request.Form["machineQuantity" + i].ToString();
                m.Uom = Request.Form["machineUOM" + i].ToString();
                m.InsertMachine();
            }
            //// FORM DATA ENDS

            return Json(new { Response = "True" });
        }

        public ActionResult Success()
        {
            return View("success");
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