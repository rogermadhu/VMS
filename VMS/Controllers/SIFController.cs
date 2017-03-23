using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS.Models;

namespace VMS.Controllers
{
    public class SIFController : Controller
    {
        // GET: SIF
        public ActionResult Index()
        {
            return View("sif");
        }
        public ActionResult Sif(FormCollection collection)
        {
            return View("sif");
        }

        [HttpPost]
        public ActionResult SubmitAction()
        {
            App_Code.Utilities util = new App_Code.Utilities();

            #region "dataGet"
            string orgName = Request.Form["orgname"];
            string []orgType = Request.Form["orgtype"].Split(new Char[] {','});
            DateTime orgDOE = DateTime.ParseExact(Request.Form["orgdoe"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            string orgEmail = Request.Form["orgemail"];

            string headOfficeStreet = Request.Form["orghoastreet"];
            string headOfficeCity = Request.Form["orghoacity"];
            string headOfficeThana = Request.Form["orghoathana"];
            string headOfficeCountry = Request.Form["orghoacountry"];

            string salesOfficeStreet = Request.Form["orgsoastreet"];
            string salesOfficeCity = Request.Form["orgsoacity"];
            string salesOfficeThana = Request.Form["orgsoathana"];
            string salesOfficeCountry = Request.Form["orgsoacountry"];

            string factoryOfficeStreet = Request.Form["orgfastreet"];
            string factoryOfficeCity = Request.Form["orgfacity"];
            string factoryOfficeThana = Request.Form["orgfathana"];
            string factoryOfficeCountry = Request.Form["orgfacountry"];

            string warehouseOfficeStreet = Request.Form["orgwhdastreet"];
            string warehouseOfficeCity = Request.Form["orgwhdacity"];
            string warehouseOfficeThana = Request.Form["orgwhdathana"];
            string warehouseOfficeCountry = Request.Form["orgwhdacountry"];

            string primaryContactDesig = Request.Form["orgcontactprimarydesig"];
            string primaryContactName = Request.Form["orgcontactprimaryname"];
            string primaryContactCell = Request.Form["orgcontactprimarycell"];
            string primaryContactEmail = Request.Form["orgcontactprimaryemail"];

            string secondaryContactDesig = Request.Form["orgcontactsecondarydesig"];
            string secondaryContactName = Request.Form["orgcontactsecondaryname"];
            string secondaryContactCell = Request.Form["orgcontactsecondarycell"];
            string secondaryContactEmail = Request.Form["orgcontactsecondaryemail"];

            string representativeContactDesig = Request.Form["orgcontactrepresentativedesig"];
            string representativeContactName = Request.Form["orgcontactrepresentativename"];
            string representativeContactCell = Request.Form["orgcontactrepresentativephone"];
            string representativeContactEmail = Request.Form["orgcontactrepresentativeemail"];

            string website = Request.Form["orgwebsite"];
            #endregion "dataGet"

            if (!util.checkVendorNameExists(orgName))
            {
                if (!util.checkVendorEmailExists(orgEmail))
                {
                    Vendor_SIF_Form vendor = new Vendor_SIF_Form();
                    vendor.VendorName = orgName;
                    vendor.Doe = orgDOE;
                    vendor.Website = website;
                    vendor.Email = orgEmail;
                    string vendorGuid = vendor.InsertVendor(vendor);

                    Model_Contact mcRep = new Model_Contact();
                    mcRep.VendorGUID = vendorGuid;
                    mcRep.Designation = representativeContactDesig;
                    mcRep.Name = representativeContactName;
                    mcRep.Phone = representativeContactCell;
                    mcRep.Email = representativeContactEmail;
                    mcRep.InsertContactRepresentative(mcRep);

                    int productsCount = (Request.Form.Count - 33) / 2;
                    //string[] productName = new string[productsCount];
                    //string[] productCategory = new string[productsCount];

                    for (int i = 0; i < productsCount; i++)
                    {
                        Model_Product mp = new Model_Product();
                        mp.VendorGUID = vendorGuid;
                        mp.ProductName = Request.Form["product" + i];
                        mp.ProductCategory = Request.Form["productType" + i];
                        if (mp.ProductCategory.ToLower() == "service")
                        {
                            mp.IsService = "Y";
                        }
                        else
                        {
                            mp.IsService = "N";
                        }
                        mp.InsertProduct(mp);
                        //productName[i] = Request.Form["product" + i];
                        //productCategory[i] = Request.Form["productType" + i];
                    }

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
                            checkFolderPath(vendorGuid);

                            string NewFileName = upload.ToString() + Path.GetExtension(file.FileName).ToLower(); ;
                            if ((Path.GetExtension(file.FileName).ToLower().Contains(".jpg"))
                                || (Path.GetExtension(file.FileName).ToLower().Contains(".jpeg"))
                                || (Path.GetExtension(file.FileName).ToLower().Contains(".png"))
                                || (Path.GetExtension(file.FileName).ToLower().Contains(".pdf")))
                            {
                                if (upload.ToString().Contains("_proFile_"))
                                {
                                    file.SaveAs(Server.MapPath("~/Content/Uploads/" + vendorGuid + "/Products/" + NewFileName));

                                    PImage pImage = new PImage();
                                    pImage.VendorGuid = vendorGuid;
                                    pImage.PName = upload.ToString().Substring(0, upload.ToString().IndexOf("_proFile_"));
                                    pImage.FileNameUploaded = file.FileName.ToString();
                                    pImage.FileExtention = Path.GetExtension(file.FileName).ToLower();
                                    pImage.FileRenamed = upload.ToString();
                                    pImage.FileSavedRootPath = "/Content/Uploads/" + vendorGuid + "/Products/" + NewFileName;
                                    pImage.FileSavedAbsolutePath = Server.MapPath("~/Content/Uploads/" + vendorGuid + "/Products/" + NewFileName);
                                    pImage.InsertProductImage();
                                }
                                else if (upload.ToString().Contains("_vCard"))
                                {
                                    file.SaveAs(Server.MapPath("~/Content/Uploads/" + vendorGuid + "/VCards/" + NewFileName));

                                    VCard vc = new VCard();
                                    vc.VendorGuid = vendorGuid;
                                    vc.VcpName = upload.ToString().Substring(0, upload.ToString().IndexOf("_vCard"));
                                    vc.FileNameUploaded = file.FileName.ToString();
                                    vc.FileExtention = Path.GetExtension(file.FileName).ToLower();
                                    vc.FileRenamed = upload.ToString();
                                    vc.FileSavedRootPath = "/Content/Uploads/" + vendorGuid + "/VCards/" + NewFileName;
                                    vc.FileSavedAbsolutePath = Server.MapPath("~/Content/Uploads/" + vendorGuid + "/VCards/" + NewFileName);
                                    vc.InsertVCard();
                                }
                            }
                        }
                    }

                    for (int i = 0; i < orgType.Length; i++)
                    {
                        Model_Business_Type businessType = new Model_Business_Type();
                        businessType.VendorGUID = vendorGuid;
                        businessType.BusinessTypeName = orgType[i].ToString();
                        businessType.SearchMapBusinessType(businessType);
                    }

                    Model_Address maHo = new Model_Address();
                    maHo.VendorGUID = vendorGuid;
                    maHo.StreetAddress = headOfficeStreet;
                    maHo.City = headOfficeCity;
                    maHo.Thana = headOfficeThana;
                    maHo.Country = headOfficeCountry;
                    maHo.Insert_Address_HO(maHo);

                    if ((!string.IsNullOrEmpty(salesOfficeStreet)) && (!string.IsNullOrEmpty(salesOfficeCity)) && (!string.IsNullOrEmpty(salesOfficeCountry)))
                    {
                        Model_Address maSo = new Model_Address();
                        maSo.VendorGUID = vendorGuid;
                        maSo.StreetAddress = salesOfficeStreet;
                        maSo.City = salesOfficeCity;
                        maSo.Thana = salesOfficeThana;
                        maSo.Country = salesOfficeCountry;
                        maSo.Insert_Address_SO(maSo);
                    }

                    if ((!string.IsNullOrEmpty(factoryOfficeStreet)) && (!string.IsNullOrEmpty(factoryOfficeCity)) && (!string.IsNullOrEmpty(factoryOfficeCountry)))
                    {
                        Model_Address maFactory = new Model_Address();
                        maFactory.VendorGUID = vendorGuid;
                        maFactory.StreetAddress = factoryOfficeStreet;
                        maFactory.City = factoryOfficeCity;
                        maFactory.Thana = factoryOfficeThana;
                        maFactory.Country = factoryOfficeCountry;
                        maFactory.Insert_Address_Factory(maFactory);
                    }

                    if ((!string.IsNullOrEmpty(warehouseOfficeStreet)) && (!string.IsNullOrEmpty(warehouseOfficeCity)) && (!string.IsNullOrEmpty(warehouseOfficeCountry)))
                    {
                        Model_Address maWarehouse = new Model_Address();
                        maWarehouse.VendorGUID = vendorGuid;
                        maWarehouse.StreetAddress = warehouseOfficeStreet;
                        maWarehouse.City = warehouseOfficeCity;
                        maWarehouse.Thana = warehouseOfficeThana;
                        maWarehouse.Country = warehouseOfficeCountry;
                        maWarehouse.Insert_Address_Warehouse(maWarehouse);
                    }
                    Model_Contact mcPrim = new Model_Contact();
                    mcPrim.VendorGUID = vendorGuid;
                    mcPrim.Designation = primaryContactDesig;
                    mcPrim.Name = primaryContactName;
                    mcPrim.Phone = primaryContactCell;
                    mcPrim.Email = primaryContactEmail;
                    mcPrim.InsertContact(mcPrim);

                    if ((!string.IsNullOrEmpty(secondaryContactDesig)) && (!string.IsNullOrEmpty(secondaryContactName)) && (!string.IsNullOrEmpty(secondaryContactEmail)))
                    {
                        Model_Contact mcSec = new Model_Contact();
                        mcSec.VendorGUID = vendorGuid;
                        mcSec.Designation = secondaryContactDesig;
                        mcSec.Name = secondaryContactName;
                        mcSec.Phone = secondaryContactCell;
                        mcSec.Email = secondaryContactEmail;
                        mcSec.InsertContact(mcSec);
                    }
                    
                    return Json(new { response = "success" }, JsonRequestBehavior.AllowGet);
                    //return View("SubmitActionTrue");
                }
                else
                {
                    // vendor email esists return msg
                    return Json(new { response = "email" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // vendor name exists return msg
                return Json(new { response = "name" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult Success()
        {
            return View("SubmitActionTrue");
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public ActionResult SubmitAction()
        //{
        //    string test = Request.Form["orgname"];
        //    string test2 = Request.Form["orgType"];

        //    //if (!Request.Content.IsMimeMultipartContent("form-data"))
        //    //{
        //    //    return Request.CreateResponse(HttpStatusCode.BadRequest);
        //    //}
        //    //string uploadFolder = "mydestinationfolder";

        //    //// Create a stream provider for setting up output streams that saves the output under -uploadFolder-
        //    //// If you want full control over how the stream is saved then derive from MultipartFormDataStreamProvider and override what you need.            
        //    //MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(uploadFolder);
        //    //MultipartFileStreamProvider multipartFileStreamProvider = await Request.Content.ReadAsMultipartAsync(streamProvider);

        //    //// Get the file names.
        //    //foreach (MultipartFileData file in streamProvider.FileData)
        //    //{
        //    //    //Do something awesome with the files..
        //    //}

        //    foreach (string upload in Request.Files)
        //    {
        //        if (!(Request.Files[upload] != null && Request.Files[upload].ContentLength > 0)) continue;

        //        int memUsage_baseline_id = 0;
        //        int timingComp_baseline_id = 0;
        //        if (upload == "FileUploadMemoryUsage" || upload == "FileUploadResultsComparison")
        //        {
        //            if (upload == "FileUploadMemoryUsage")
        //            {
        //                if (Request.Params["memUsage_project"] == null || Request.Params["memUsage_project"] == "")
        //                {
        //                    ModelState.AddModelError("Project", "Please Select Project for Memory Usage");
        //                }
        //                else
        //                {
        //                    memUsage_baseline_id = int.Parse(Request.Params["memUsage_project"]);
        //                }
        //            }
        //            else
        //            {
        //                if (Request.Params["resultsComp_project"] == null || Request.Params["resultsComp_project"] == "")
        //                {
        //                    ModelState.AddModelError("Project", "Please Select Project for Timing Comparison");
        //                }
        //                else
        //                {
        //                    timingComp_baseline_id = int.Parse(Request.Params["resultsComp_project"]);
        //                }
        //            }
        //        }

        //        HttpPostedFileBase file = Request.Files[upload];

        //        if (ModelState.IsValid)
        //        {
        //            if (file == null)
        //            {
        //                ModelState.AddModelError("File", "Please Upload Your file");
        //            }
        //            else if (file.ContentLength > 0)
        //            {
        //                //int MaxContentLength = 1024 * 1024 * 3; //3 MB
        //                string[] AllowedFileExtensions = new string[] { ".jpeg", ".jpg", ".png" };

        //                if (!AllowedFileExtensions.Contains(file.FileName.ToLower().Substring(file.FileName.ToLower().LastIndexOf('.'))))
        //                {
        //                    ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
        //                }

        //                //else if (file.ContentLength > MaxContentLength)
        //                //{
        //                //    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
        //                //}
        //                else
        //                {
        //                    var fileName = Path.GetFileName(file.FileName);
        //                    var path = Path.Combine(Server.MapPath("~/Content/Upload"), fileName);
        //                    file.SaveAs(path);
        //                    ModelState.Clear();
        //                    ViewBag.Message = "File uploaded successfully";
        //                }
        //            }
        //        }
        //    }
        //    return View("SubmitActionTrue");
        //}
    }
}