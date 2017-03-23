using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using VMS.ClassUtility;
using VMS.Models;
using VMS.App_Code;

namespace VMS.Controllers.Admin
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Authentication.IsSetSession())
            {                
                string uid = HttpContext.Session["USER_GUID"].ToString();
                return View("index");
            }
            else
            {
                return RedirectToAction("../../login");
            }
        }

        // SIF PAGE FUNCTIONS ** BEGINS **
        #region "SIF Approval Page"
        public ActionResult ViewSIF()
        {
            return View("ViewSIF");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult getVendorListSif()
        {
            App_Code.DatabaseMSSQL db = new App_Code.DatabaseMSSQL();
            string query =
                @"SELECT 
                     ROW_NUMBER() OVER (ORDER BY VENDOR_NAME) AS 'SL',
                     VENDOR_GUID,
                     VENDOR_NAME AS 'VENDOR_NAME', 
                     DATE_OF_ESTABLISHMENT AS 'DOE', 
                     WEBSITE AS 'WEBSITE'
                 FROM 
	                VENDOR 
                 WHERE
	                VENDOR_STATUS = 'P'
                    AND VENDOR_IS_ACTIVE = 'Y'
                 GROUP BY 
	                VENDOR_GUID, VENDOR_NAME, DATE_OF_ESTABLISHMENT, WEBSITE";
            System.Data.DataTable dt = db.GetDataTable(query);

            //return Json(new { data = dt }, JsonRequestBehavior.AllowGet);

            var list = JsonConvert.SerializeObject(dt, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }

        [HttpPost]
        public ActionResult Vendors_GetSIF(string vendor, string sector)
        {
            if (!string.IsNullOrEmpty(vendor))
            {
                System.Data.DataSet dt = new System.Data.DataSet();

                App_Code.DatabaseMSSQL db = new App_Code.DatabaseMSSQL();
                string query = string.Empty;

                if (sector == "details")
                {
                    query =
                        @"SELECT
	                        V.VENDOR_NAME,
	                        ISNULL(NULLIF(V.WEBSITE, ''), 'N/A') AS WEBSITE,
	                        TOB.TOB_NAME AS 'TOB'
                        FROM
	                        VENDOR V
	                        LEFT JOIN VENDOR_TOB_MAP TOBMAP ON V.VENDOR_GUID = TOBMAP.VENDOR_GUID
	                        LEFT JOIN TYPE_OF_BUSINESS TOB ON TOBMAP.TOB_GUID = TOB.TOB_GUID
                        WHERE
	                        V.VENDOR_GUID = '" + vendor + @"'";
                }
                else if (sector == "products")
                {
                    query = 
                        @"SELECT
	                        PM.PRODUCT_NAME,
	                        PM.DESCRIPTON,
	                        PM.PRODUCT_CATEGORY_GUID,
	                        PM.IS_SERVICE,
	                        PM.IS_OBSOLETE,
	                        PM.IS_OWN_PRODUCTION,
	                        PC.CATEGORY_NAME,
	                        PC.REMARKS,
	                        PCIM.FILE_NAME_UPLOADED,
	                        PCIM.FILE_EXTENTION,
	                        PCIM.FILE_RENAMED,
	                        PCIM.FILE_SAVED_ROOT_PATH,
	                        PCIM.FILE_SAVED_ABSOLUTE_PATH,
	                        PCIM.DATE_UPLOADED
                        FROM 
	                        VENDOR V
	                        LEFT JOIN PRODUCT_MAP PM ON v.VENDOR_GUID = pm.VENDOR_GUID
	                        LEFT JOIN PRODUCT_CATEGORY PC on PM.PRODUCT_CATEGORY_GUID = PC.PRODUCT_CATEGORY_GUID
	                        LEFT JOIN PRODUCT_CATALOGUE_IMAGE_MAP PCIM ON PM.PRODUCT_GUID = PCIM.PRODUCT_GUID
                        WHERE
	                        V.VENDOR_GUID = '" + vendor + @"'
	                        AND PM.IS_ACTIVE = 'Y'";
                }
                else if (sector == "contacts")
                {
                    query =
                        @"SELECT
	                        D.DESIGNATION_NAME,
	                        VCP.VCP_NAME,
	                        ISNULL(CAST(VCP.TEL_NO AS VARCHAR(30)), '&nbsp;') AS TEL_NO,
	                        ISNULL(CAST(VCP.CELL_NO AS VARCHAR(30)), '&nbsp;') AS CELL_NO,
	                        ISNULL(VCP.EMAIL, '&nbsp;') AS EMAIL,
	                        VCP.IS_POC,
	                        ISNULL(VCPV.FILE_NAME_UPLOADED, '&nbsp;') AS FILE_NAME_UPLOADED,
	                        ISNULL(VCPV.FILE_EXTENTION, '&nbsp;') AS FILE_EXTENTION,
	                        ISNULL(VCPV.FILE_SAVED_ROOT_PATH, '&nbsp;') AS FILE_SAVED_ROOT_PATH
                        FROM 
	                        VENDOR V
	                        LEFT JOIN VENDOR_CONTACT_PERSON VCP ON V.VENDOR_GUID = VCP.VENDOR_GUID
	                        LEFT JOIN DESIGNATION D ON VCP.DESIGNATION_GUID = D.DESIGNATION_GUID
	                        LEFT JOIN VENDOR_CONTACT_PERSON_VCARD VCPV ON VCP.VCP_GUID = VCPV.VCP_GUID
                        WHERE
	                        V.VENDOR_GUID = '" + vendor + "'";
                }
                else if (sector == "address")
                {
                    query = 
                        @"SELECT
	                        V.VENDOR_GUID,
	                        VAT.[TYPE_NAME],
	                        VAM.STREET_ADDRESS,
	                        VAM.CITY,
	                        VAM.THANA,
	                        VAM.COUNTRY,
	                        ISNULL(CAST(VAM.ZIP AS VARCHAR(10)), '') AS ZIP
                        FROM 
	                        VENDOR V
	                        LEFT JOIN VENDOR_ADDRESS_MAP VAM ON V.VENDOR_GUID = VAM.VENDOR_GUID
	                        LEFT JOIN VENDOR_ADDRESS_TYPE VAT ON VAM.VA_TYPE_GUID = VAT.VA_TYPE_GUID
                        WHERE
	                        V.VENDOR_GUID = '" + vendor + @"'
                        ORDER BY VAT.VA_TYPE_ID ASC";
                }

                dt = db.GetDataSet(query);

                var list = JsonConvert.SerializeObject(dt, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Content(list, "application/json");
            }
            else
                return null;
        }

        [HttpPost]
        [WebMethod(EnableSession = true)]
        [AllowAnonymous]
        public ActionResult PostApproved()
        {
            try
            {
                string[] categorys = Request.Form["category"].Split(new Char[] { ',' });
                string[] subCategorys = Request.Form["subCategory"].Split(new Char[] { ',' });
                string[] pocs = Request.Form["poc"].Split(new Char[] { ',' });
                string vid = Request.Form["vid"];
                string userid = Session["USER_GUID"].ToString();

                if (((!string.IsNullOrEmpty(userid)) && (!string.IsNullOrEmpty(vid)))
                    && (categorys.Length > 0)
                    && (subCategorys.Length > 0)
                    && (pocs.Length > 0))
                {
                    //INSERT SIF DATA
                    SIFApprove sifapprove = new SIFApprove();
                    sifapprove.VendorId = vid;
                    sifapprove.UserId = userid;
                    sifapprove.VendorCategory = categorys;
                    sifapprove.VendorSubCategory = subCategorys;
                    sifapprove.Poc = pocs;
                    sifapprove.Approve();

                    // GEN ID PWD AND SET DB CREDENTIALS
                    Utilities util = new Utilities();
                    string tempId = util.RandomGenSIF();
                    string pwd = util.RandPwd();
                    string vEmail = sifapprove.setCredentials(sifapprove.VendorId, tempId, pwd);

                    // SEND EMAIL
                    MailHelper email = new MailHelper();
                    email.SIFTemplate(vEmail, tempId, vEmail, pwd);

                    return Json(new { result = true });
                }
            }
            catch (Exception ex) { ex.Message.ToString(); return Json(new { result = false }); }
            return Json(new { result = false });
        }

        [HttpPost]
        [WebMethod(EnableSession = true)]
        [AllowAnonymous]
        public ActionResult PostReject()
        {
            try
            {
                string vid = Request.Form["vid"];
                string remarks = Request.Form["remarks"];
                string userid = Session["USER_GUID"].ToString();

                if ((!string.IsNullOrEmpty(userid)) && (!string.IsNullOrEmpty(vid)))
                {
                    SIFReject reject = new SIFReject();
                    reject.UserId = userid;
                    reject.VendorId = vid;
                    reject.Remarks = remarks;
                    reject.setReject();

                    return Json(new { result = true });
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json(new { result = false });
            }
            return Json(new { result = false });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult getParentCategory()
        {
            App_Code.DatabaseMSSQL db = new App_Code.DatabaseMSSQL();
            string query =
                @"SELECT
	                VC.VC_GUID AS 'ID',
	                VC.VC_NAME AS 'CATEGORY'
                FROM
	                VENDOR_CATEGORY VC
                WHERE
	                VC.PARENT_CATEGORY_GUID IS NULL
	                AND VC.IS_ACTIVE = 'Y'";
            System.Data.DataTable dt = db.GetDataTable(query);
            var list = JsonConvert.SerializeObject(dt, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult getChildCategory(string parentId)
        {
            App_Code.DatabaseMSSQL db = new App_Code.DatabaseMSSQL();
            string query =
                @"SELECT
	                VC.VC_GUID AS 'ID',
	                VC.VC_NAME AS 'CATEGORY'
                FROM
	                VENDOR_CATEGORY VC
                WHERE
	                VC.PARENT_CATEGORY_GUID = '" + parentId + @"'
	                AND VC.IS_ACTIVE = 'Y'";
            System.Data.DataTable dt = db.GetDataTable(query);
            var list = JsonConvert.SerializeObject(dt, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult getPOC(string vId)
        {
            App_Code.DatabaseMSSQL db = new App_Code.DatabaseMSSQL();
            string query =
                @"SELECT
	                VCP.VCP_GUID AS 'VCPID',
	                (VCP.VCP_NAME + ' - ' + D.DESIGNATION_NAME) AS 'VCP'
                FROM
	                VENDOR_CONTACT_PERSON VCP
	                INNER JOIN DESIGNATION D ON VCP.DESIGNATION_GUID = D.DESIGNATION_GUID
                WHERE 
	                VENDOR_GUID = '" + vId + @"'
	                AND VCP.IS_ACTIVE = 'Y'";
            System.Data.DataTable dt = db.GetDataTable(query);
            var list = JsonConvert.SerializeObject(dt, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }
        #endregion "SIF Approval Page"
        // SIF PAGE FUNCTIONS ** END **

        
        
        
        // SEF PAGE FUNCTIONS ** BEGINS **
        #region "SEF Approval Page"
        public ActionResult ViewSEF()
        {
            return View("ViewSEF");
        }
        [HttpPost]
        public ActionResult VendorsList_GetDetails(string vendor, string sector)
        {
            if (!string.IsNullOrEmpty(vendor))
            {
                System.Data.DataSet dt = new System.Data.DataSet();

                App_Code.DatabaseMSSQL db = new App_Code.DatabaseMSSQL();
                string query = string.Empty;

                if (sector == "details")
                {
                    query =
                        @"SELECT
	                        V.VENDOR_GUID,
	                        V.VENDOR_CODE,
	                        V.VENDOR_ID,
	                        V.VENDOR_NAME,
	                        V.WEBSITE,
	                        V.TEL_NO,
	                        V.CELL_NO,
	                        V.FAX_NO,
	                        V.EMAIL,
	                        V.DATE_OF_ESTABLISHMENT, 
	                        V.TOTAL_NO_OF_EMPLOYEE,	
	                        V.CREATE_DATE AS 'REQ_DATE',
	                        V.MODIFY_DATE AS 'APPROVED_DATE',
	                        V.MODIFY_BY AS 'APPROVED_BY',
	                        TOB.TOB_NAME,
	                        TOB_MAP.IS_OTHER AS 'IS_OTHER_TOB',
	                        TOB_MAP.OTHER_NAME,	
	                        VTYPE.VENDOR_TYPE_NAME AS 'VTYPE_NAME',
	                        VTYPE_MAP.OTHER_NAME AS 'VTYPE_OTHER',
	                        VTYPE_MAP.IS_OTHER AS 'IS_OTHER_VTYPE',	
	                        VCAT.VC_NAME,
	                        VC_MAP.IS_OTHER AS 'IS_OTHER_VC',
	                        VC_MAP.VC_OTHER,	
	                        VADDTYPE.[TYPE_NAME],
	                        VADD_MAP.STREET_ADDRESS,
	                        VADD_MAP.CITY,
	                        VADD_MAP.THANA,
	                        VADD_MAP.COUNTRY,
	                        VADD_MAP.ZIP
                        FROM 
	                        VENDOR V
	                        LEFT JOIN VENDOR_TOB_MAP TOB_MAP ON V.VENDOR_GUID = TOB_MAP.VENDOR_GUID
	                        LEFT JOIN TYPE_OF_BUSINESS TOB ON TOB_MAP.TOB_GUID = TOB.TOB_GUID

	                        LEFT JOIN VENDOR_TYPE_MAP VTYPE_MAP ON VTYPE_MAP.VENDOR_GUID = V.VENDOR_GUID
	                        LEFT JOIN VENDOR_TYPE VTYPE ON VTYPE_MAP.VENDOR_TYPE_GUID = VTYPE.VENDOR_TYPE_GUID

	                        LEFT JOIN VENDOR_VC_MAP VC_MAP ON V.VENDOR_GUID = VC_MAP.VENDOR_GUID
	                        LEFT JOIN VENDOR_CATEGORY VCAT ON VC_MAP.VC_GUID = VCAT.VC_GUID

	                        LEFT JOIN VENDOR_ADDRESS_MAP VADD_MAP ON V.VENDOR_GUID = VADD_MAP.VENDOR_GUID
	                        LEFT JOIN VENDOR_ADDRESS_TYPE VADDTYPE ON VADD_MAP.VA_TYPE_GUID = VADDTYPE.VA_TYPE_GUID";
                }
                else if (sector == "products")
                {
                    query = @"
                        SELECT
	                        V.VENDOR_GUID,
	                        V.VENDOR_CODE,
	                        V.VENDOR_ID,
	                        V.VENDOR_NAME,
	                        V.WEBSITE,
	                        V.TEL_NO,
	                        V.CELL_NO,
	                        V.FAX_NO,
	                        V.EMAIL,
	                        V.DATE_OF_ESTABLISHMENT, 
	                        V.TOTAL_NO_OF_EMPLOYEE,	
	                        V.CREATE_DATE AS 'REQ_DATE',
	                        V.MODIFY_DATE AS 'APPROVED_DATE',
	                        V.MODIFY_BY AS 'APPROVED_BY',
	                        PM.PRODUCT_NAME,
	                        PM.DESCRIPTON,
	                        PM.PRODUCT_CATEGORY_GUID,
	                        PM.IS_SERVICE,
	                        PM.IS_OBSOLETE,
	                        PM.IS_ACTIVE,
	                        PM.IS_OWN_PRODUCTION,
	                        PC.CATEGORY_NAME,
	                        PC.REMARKS,
	                        PCIM.FILE_NAME_UPLOADED,
	                        PCIM.FILE_EXTENTION,
	                        PCIM.FILE_RENAMED,
	                        PCIM.FILE_SAVED_ROOT_PATH,
	                        PCIM.FILE_SAVED_ABSOLUTE_PATH,
	                        PCIM.DATE_UPLOADED
                        FROM 
	                        VENDOR V
	                        LEFT JOIN PRODUCT_MAP PM ON v.VENDOR_GUID = pm.VENDOR_GUID
	                        LEFT JOIN PRODUCT_CATEGORY PC on PM.PRODUCT_CATEGORY_GUID = PC.PRODUCT_CATEGORY_GUID
	                        LEFT JOIN PRODUCT_CATALOGUE_IMAGE_MAP PCIM ON PM.PRODUCT_GUID = PCIM.PRODUCT_GUID";
                }
                else if (sector == "contacts")
                {
                    query =
                        @"SELECT
	                        V.VENDOR_GUID,
	                        D.DESIGNATION_NAME,
	                        VCP.VCP_NAME,
	                        VCP.TEL_NO,
	                        VCP.CELL_NO,
	                        VCP.EMAIL,
	                        VCP.IS_POC,
	                        VCPV.FILE_NAME_UPLOADED,
	                        VCPV.FILE_EXTENTION,
	                        VCPV.FILE_SAVED_ROOT_PATH
                        FROM 
	                        VENDOR V
	                        LEFT JOIN VENDOR_CONTACT_PERSON VCP ON V.VENDOR_GUID = VCP.VENDOR_GUID
	                        LEFT JOIN DESIGNATION D ON VCP.DESIGNATION_GUID = D.DESIGNATION_GUID
	                        LEFT JOIN VENDOR_CONTACT_PERSON_VCARD VCPV ON VCP.VCP_GUID = VCPV.VCP_GUID";
                }
                else if (sector == "financial")
                {
                    query = @"
                    SELECT
	                    V.VENDOR_GUID,
	                    V.VENDOR_CODE,
	                    V.VENDOR_ID,
	                    V.VENDOR_NAME,
	                    V.EMAIL,
	                    V.DATE_OF_ESTABLISHMENT, 
	                    V.CREATE_DATE AS 'REQ_DATE',
	                    V.MODIFY_DATE AS 'APPROVED_DATE',
	                    V.MODIFY_BY AS 'APPROVED_BY',

	                    VFIBMAP.BANK_BRANCH_GUID,
	                    VFIBMAP.BANK_NAME,
	                    VFIBMAP.BANK_ADDRESS,
	                    VFIBMAP.ACCOUNT_NO,
	                    VFIBMAP.ACCOUNT_NAME,
	                    VFIBMAP.SWIFT_CODE,
	                    VFIBMAP.ROUTING_NO,
	                    VFIBMAP.IS_PRIMARY,

	                    B.TEL_NO1,
	                    B.TEL_NO2,
	                    B.CONTACT_PERSON_NAME AS 'BCONTACT_PERSON_NAME',
	                    B.CONTACT_PERSON_PHONE AS 'BCONTACT_PERSON_PHONE',

	                    BBMAP.ADDRESS AS 'BRANCHADDRESS',
	                    BBMAP.TEL_NO1 AS 'BRANCHTEL_NO1',
	                    BBMAP.TEL_NO2 AS 'BRANCHTEL_NO2',
	                    BBMAP.CONTACT_PERSON_NAME AS 'BRANCHCONTACT_PERSON_NAME',
	                    BBMAP.CONTACT_PERSON_PHONE AS 'BRANCHCONTACT_PERSON_PHONE'
                    FROM 
	                    VENDOR V
	                    LEFT JOIN VFI_BANK_INFORMATION_MAP VFIBMAP ON V.VENDOR_GUID = VFIBMAP.VENDOR_GUID
	                    LEFT JOIN BANK B ON VFIBMAP.BANK_GUID = B.BANK_GUID
	                    LEFT JOIN BANK_BRANCH_MAP BBMAP ON VFIBMAP.BANK_BRANCH_GUID = BBMAP.BANK_BRANCH_GUID;

                    SELECT
	                    V.VENDOR_GUID,
	                    VFIAS.[YEAR],
	                    VFIAS.VALUE,
	                    VFIAS.CURRENCY,
	                    VFIAS.EXCHANGE_RATE
                    FROM
	                    VENDOR V
	                    LEFT JOIN VFI_ANNUAL_SALES VFIAS ON V.VENDOR_GUID = VFIAS.VENDOR_GUID";
                }
                else if (sector == "address")
                {
                    query = @"SELECT
	                    V.VENDOR_GUID,
	                    VAT.[TYPE_NAME],
	                    VAM.STREET_ADDRESS,
	                    VAM.CITY,
	                    VAM.THANA,
	                    VAM.COUNTRY,
	                    VAM.ZIP
                    FROM 
	                    VENDOR V
	                    LEFT JOIN VENDOR_ADDRESS_MAP VAM ON V.VENDOR_GUID = VAM.VENDOR_GUID
	                    LEFT JOIN VENDOR_ADDRESS_TYPE VAT ON VAM.VA_TYPE_GUID = VAT.VA_TYPE_GUID";
                }
                else if (sector == "machines")
                {
                    query =
                        @"SELECT
	                        V.VENDOR_GUID,
	                        PMM.PM_NAME,
	                        PMM.BRAND_NAME,
	                        PMM.PM_ORIGIN,
	                        PMM.PURPOSE,
	                        PMM.QUANTITY,
	                        PMM.UOM
                        FROM 
	                        VENDOR V
	                        INNER JOIN PRODUCT_MACHINE_MAP PMM ON V.VENDOR_GUID = PMM.VEDNOR_GUID";
                }
                else if (sector == "certifications")
                {
                    query =
                        @"SELECT
	                    V.VENDOR_GUID,
	                    VC.CERTIFICATION_NAME,
	                    VC.CERTIFICATION_NO,
	                    VC.DESCRIPTION,
	                    VC.REMARKS,
	                    VC.IS_OTHER,
	                    VCTM.CERTIFICATION_NAME AS CERTIFICATION_NAME_OTHER
                    FROM 
	                    VENDOR V
	                    LEFT JOIN VENDOR_CERTIFICATION VC ON V.VENDOR_GUID = VC.VENDOR_GUID
	                    LEFT JOIN CERTIFICATION_TYPE_MAP VCTM ON VC.CERTIFICATION_TYPE_GUID = VCTM.CERTIFICATION_TYPE_GUID";
                }

                dt = db.GetDataSet(query);

                var list = JsonConvert.SerializeObject(dt, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Content(list, "application/json");
            }
            else
                return null;
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult getVendorListSef()
        {
            App_Code.DatabaseMSSQL db = new App_Code.DatabaseMSSQL();
            string query =
                @"SELECT 
                    ROW_NUMBER() OVER (ORDER BY VENDOR_NAME) AS 'SL',
                    VENDOR_GUID,
                    VENDOR_NAME AS 'VENDOR_NAME', 
                    VENDOR_CODE AS 'CODE',
                    DATE_OF_ESTABLISHMENT AS 'DOE', 
                    WEBSITE AS 'WEBSITE',
                    TEL_NO AS 'PHONE_NO',
                    EMAIL AS 'EMAIL',
                    CASE WHEN VENDOR_STATUS = 'A' THEN 'SIF Accepted' ELSE 'ERROR' END AS 'STATUS'
                FROM 
                    VENDOR 
                WHERE
                    VENDOR_STATUS = 'A'
                    AND VENDOR_IS_ACTIVE = 'Y'
                GROUP BY 
                    VENDOR_GUID, VENDOR_CODE, TEL_NO, EMAIL, VENDOR_NAME, DATE_OF_ESTABLISHMENT, WEBSITE, VENDOR_STATUS";
            System.Data.DataTable dt = db.GetDataTable(query);

            //return Json(new { data = dt }, JsonRequestBehavior.AllowGet);

            var list = JsonConvert.SerializeObject(dt, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }

        [HttpPost]
        public ActionResult Vendors_GetSEF(string vendor, string sector)
        {
            if (!string.IsNullOrEmpty(vendor))
            {
                System.Data.DataSet ds = new System.Data.DataSet();

                App_Code.DatabaseMSSQL db = new App_Code.DatabaseMSSQL();
                string query = string.Empty;

                if (sector == "details")
                {
                    query =
                        @"DECLARE @VGUID VARCHAR(36) = '" + vendor + @"';

	                    SELECT 
                            V.VENDOR_NAME,
		                    ISNULL(CAST(V.TEMP_ID1 AS VARCHAR(30)), 'N/A') as 'TEMPID',
		                    ISNULL(CAST(V.TRADE_LICENSE AS VARCHAR(30)), 'N/A') as 'TRADE_LICENSE',
		                    ISNULL(CAST(V.TIN_NO AS VARCHAR(30)), 'N/A') as 'TIN_NO',
		                    ISNULL(CAST(V.TEL_NO AS VARCHAR(30)), 'N/A') as 'TEL_NO',
		                    ISNULL(CAST(V.CELL_NO AS VARCHAR(30)), 'N/A') as 'CELL_NO',
		                    ISNULL(CAST(V.FAX_NO AS VARCHAR(30)), 'N/A') as 'FAX_NO'
	                    FROM 
		                    VENDOR V
		                    LEFT JOIN VENDOR_TOB_MAP TOBMAP ON V.VENDOR_GUID = TOBMAP.VENDOR_GUID
	                    WHERE
		                    V.VENDOR_IS_ACTIVE = 'Y'
		                    AND V.VENDOR_GUID = @VGUID

	                    SELECT 
		                    TOB.TOB_NAME AS 'BUSINESS'
	                    FROM 
		                    VENDOR V
		                    LEFT JOIN VENDOR_TOB_MAP TOBMAP ON V.VENDOR_GUID = TOBMAP.VENDOR_GUID
		                    LEFT JOIN TYPE_OF_BUSINESS TOB ON TOBMAP.TOB_GUID = TOB.TOB_GUID
	                    WHERE
		                    V.VENDOR_IS_ACTIVE = 'Y'
		                    AND V.VENDOR_GUID = @VGUID

	                    SELECT 
		                    VT.VENDOR_TYPE_NAME AS 'COMPANY'
	                    FROM
		                    VENDOR V
		                    INNER JOIN VENDOR_TYPE_MAP VTM ON V.VENDOR_GUID = VTM.VENDOR_GUID
		                    INNER JOIN VENDOR_TYPE VT ON VTM.VENDOR_TYPE_GUID = VT.VENDOR_TYPE_GUID
	                    WHERE
		                    V.VENDOR_IS_ACTIVE = 'Y'
		                    AND V.VENDOR_GUID = @VGUID

	                    SELECT 
		                    VCT.VENDOR_COMPANY_TYPE_NAME AS 'COMPANYCATEGORY'
	                    FROM
		                    VENDOR V
		                    INNER JOIN VENDOR_COMPANY_TYPE_MAP VCTM ON V.VENDOR_GUID = VCTM.VENDOR_GUID
		                    INNER JOIN VENDOR_COMPANY_TYPE VCT ON VCTM.VENDOR_COMPANY_TYPE_GUID = VCT.VENDOR_COMPANY_TYPE_GUID
	                    WHERE
		                    V.VENDOR_IS_ACTIVE = 'Y'
		                    AND V.VENDOR_GUID = @VGUID

	                    SELECT
		                    OFC.VENDOR_INTL_OFC_COUNTRY
	                    FROM 
		                    VENDOR V
		                    INNER JOIN VENDOR_INTL_OFC OFC ON V.VENDOR_GUID = OFC.VENDOR_GUID
	                    WHERE
		                    V.VENDOR_IS_ACTIVE = 'Y'
		                    AND V.VENDOR_GUID = @VGUID

	                    SELECT
		                    IMPORT.MATERIAL_NAME,
		                    IMPORT.COUNTRY_NAME
	                    FROM
		                    VENDOR V
		                    INNER JOIN VENDOR_RAW_MATERIAL_IMPORT_LIST_MAP IMPORT ON V.VENDOR_GUID = IMPORT.VENDOR_GUID
	                    WHERE
		                    V.VENDOR_IS_ACTIVE = 'Y'
		                    AND IMPORT.IS_ACTIVE = 'Y'
		                    AND V.VENDOR_GUID = @VGUID


	                    SELECT P.PARENT, C.CHILD FROM (
		                    SELECT
			                    VC.VC_GUID AS 'PARENT_GUID',
			                    VC.VC_NAME AS 'PARENT'
		                    FROM
			                    VENDOR V
			                    INNER JOIN VENDOR_VC_MAP VCMAP ON V.VENDOR_GUID = VCMAP.VENDOR_GUID
			                    INNER JOIN VENDOR_CATEGORY VC ON VCMAP.VC_GUID = VC.VC_GUID
		                    WHERE
			                    V.VENDOR_GUID = @VGUID
			                    AND PARENT_CATEGORY_GUID IS NULL
	                    ) P 
	                    INNER JOIN ( 
		                    SELECT
			                    VC.PARENT_CATEGORY_GUID AS 'CHILD_GUID',
			                    VC.VC_NAME AS 'CHILD'
		                    FROM
			                    VENDOR V
			                    INNER JOIN VENDOR_VC_MAP VCMAP ON V.VENDOR_GUID = VCMAP.VENDOR_GUID
			                    INNER JOIN VENDOR_CATEGORY VC ON VCMAP.VC_GUID = VC.VC_GUID
		                    WHERE
			                    V.VENDOR_GUID = @VGUID
			                    AND PARENT_CATEGORY_GUID IS NOT NULL
	                    ) C ON P.PARENT_GUID = C.CHILD_GUID
	                    ORDER BY
		                    P.PARENT ASC";
                }
                else if (sector == "products")
                {
                    query =
                        @"SELECT
	                        PM.PRODUCT_NAME,
	                        PM.DESCRIPTON,
	                        PM.PRODUCT_CATEGORY_GUID,
	                        PM.IS_SERVICE,
	                        PM.IS_OBSOLETE,
	                        PM.IS_OWN_PRODUCTION,
	                        PC.CATEGORY_NAME,
	                        PC.REMARKS,
	                        PCIM.FILE_NAME_UPLOADED,
	                        PCIM.FILE_EXTENTION,
	                        PCIM.FILE_RENAMED,
	                        PCIM.FILE_SAVED_ROOT_PATH,
	                        PCIM.FILE_SAVED_ABSOLUTE_PATH,
	                        PCIM.DATE_UPLOADED
                        FROM 
	                        VENDOR V
	                        LEFT JOIN PRODUCT_MAP PM ON v.VENDOR_GUID = pm.VENDOR_GUID
	                        LEFT JOIN PRODUCT_CATEGORY PC on PM.PRODUCT_CATEGORY_GUID = PC.PRODUCT_CATEGORY_GUID
	                        LEFT JOIN PRODUCT_CATALOGUE_IMAGE_MAP PCIM ON PM.PRODUCT_GUID = PCIM.PRODUCT_GUID
                        WHERE
	                        V.VENDOR_GUID = '" + vendor + @"'
	                        AND PM.IS_ACTIVE = 'Y'";
                }
                else if (sector == "employee")
                {
                    query =
                        @"DECLARE @VGUID VARCHAR(36) = '" + vendor + @"'
	                    SELECT 
		                    V.TOTAL_NO_OF_EMPLOYEE AS 'TTL_EMP',
		                    VEI.VEI_EXPERIENCE_AREA AS 'AREA',
		                    VEI.VEI_QUANTITY AS 'QTY', 
		                    VEI.VEI_REMARKS AS 'REMARKS'
	                    FROM
		                    VENDOR V
		                    INNER JOIN VENDOR_EMPLOYEE_INFORMATION VEI ON V.VENDOR_GUID = VEI.VENDOR_GUID
	                    WHERE 
		                    V.VENDOR_IS_ACTIVE = 'Y'
		                    AND V.VENDOR_GUID = @VGUID";
                }
                else if (sector == "exp")
                {
                    query = 
                        @"DECLARE @VGUID VARCHAR(36) = '"+ vendor + @"'
                        SELECT 
		                    ROW_NUMBER() OVER (ORDER BY EXPN.VENDOR_EXP_ORG_NAME) AS 'SL',
		                    EXPN.VENDOR_EXP_ORG_NAME AS 'ORGNAME',
		                    EXPN.VENDOR_EXP_AMOUNT AS 'AMNT',
		                    EXPN.VENDOR_EXP_YEAR AS 'YR',
		                    EXPN.VENDOR_EXP_GOODS AS 'GOODS',
		                    EXPN.VENDOR_EXP_DESTINATION AS 'DESC'
	                    FROM
		                    VENDOR V
		                    INNER JOIN VENDOR_EXP EXPN ON V.VENDOR_GUID = EXPN.VENDOR_GUID
	                    WHERE
		                    V.VENDOR_IS_ACTIVE = 'Y'
		                    AND V.VENDOR_GUID = @VGUID
	                    ORDER BY 
		                    EXPN.VENDOR_EXP_ORG_NAME ASC";
                }
                else if (sector == "machines")
                {
                    query =
                        @"DECLARE @VGUID VARCHAR(36) = '"+ vendor + @"'
                        SELECT 
                            ROW_NUMBER() OVER (ORDER BY MMAP.PM_NAME) AS 'SL',
	                        MMAP.PM_NAME AS 'NAME',
	                        MMAP.BRAND_NAME AS 'BRAND',
	                        MMAP.PM_ORIGIN AS 'ORIGIN',
	                        MMAP.PURPOSE AS 'PURPOSE',
	                        MMAP.QUANTITY AS 'QTY',
	                        MMAP.UOM AS 'UOM'
                        FROM
	                        VENDOR V
	                        INNER JOIN PRODUCT_MACHINE_MAP MMAP ON V.VENDOR_GUID = MMAP.VENDOR_GUID
                        WHERE
	                        V.VENDOR_IS_ACTIVE = 'Y'
	                        AND V.VENDOR_IS_ACTIVE = 'Y'";
                }
                else if (sector == "certification")
                {
                    query =
                        @"DECLARE @VGUID VARCHAR(36) = '"+ vendor + @"'
                        SELECT
	                        ROW_NUMBER() OVER (ORDER BY CER.CERTIFICATION_NAME) AS 'SL',
	                        ISNULL(CER.CERTIFICATION_NAME, [TYPE].CERTIFICATION_NAME) AS 'CERNAME',
	                        CER.REMARKS,
	                        CER.[DESCRIPTION]
                        FROM
	                        VENDOR V
	                        INNER JOIN VENDOR_CERTIFICATION CER ON V.VENDOR_GUID = CER.VENDOR_GUID
	                        INNER JOIN CERTIFICATION_TYPE [TYPE] ON CER.CERTIFICATION_TYPE_GUID = [TYPE].CERTIFICATION_NAME
                        WHERE
	                        V.VENDOR_IS_ACTIVE = 'Y'
	                        AND V.VENDOR_GUID = @VGUID
		
                        SELECT
	                        CERFILE.FILE_RENAMED AS 'FILENAME',
	                        CERFILE.FILE_SAVED_ROOT_PATH AS 'PATH',
	                        CERFILE.FILE_SAVED_ABSOLUTE_PATH AS 'ABSPATH'
                        FROM
	                        VENDOR V
	                        INNER JOIN VENDOR_CERTIFICATION_FILE_MAP CERFILE ON V.VENDOR_GUID = CERFILE.VENDOR_GUID
                        WHERE
	                        V.VENDOR_IS_ACTIVE = 'Y'
	                        AND CERFILE.IS_ACTIVE = 'Y'
	                        AND V.VENDOR_GUID = @VGUID";
                }
                else if (sector == "fin")
                {
                    query =
                        @"DECLARE @VGUID VARCHAR(36) = '"+ vendor + @"'
                        SELECT 
	                        BANK.BANK_NAME AS 'BANKNAME',
	                        BANK.BANK_ADDRESS AS 'ADDRESS',
	                        BANK.ACCOUNT_NO AS 'ACCOUNTNO',
	                        BANK.ACCOUNT_NAME AS 'ACCOUNTNAME',
	                        BANK.SWIFT_CODE AS 'SWIFT',
	                        BANK.ROUTING_NO AS 'ROUTINGNO'
                        FROM 
	                        VENDOR V
	                        INNER JOIN VFI_BANK_INFORMATION_MAP BANK ON V.VENDOR_GUID = BANK.VENDOR_GUID
                        WHERE 
	                        V.VENDOR_IS_ACTIVE = 'Y'
	                        AND V.VENDOR_GUID = @VGUID

                        SELECT
	                        SALES.[YEAR],
	                        SALES.VALUE
                        FROM
	                        VENDOR V
	                        INNER JOIN VFI_ANNUAL_SALES SALES ON V.VENDOR_GUID = SALES.VENDOR_GUID
                        WHERE
	                        V.VENDOR_IS_ACTIVE = 'Y'
	                        AND V.VENDOR_GUID = @VGUID

                        SELECT 
	                        VFIAR.FILE_RENAMED AS 'FILENAME',
	                        VFIAR.FILE_SAVED_ROOT_PATH AS 'PATH',
	                        VFIAR.FILE_SAVED_ABSOLUTE_PATH AS 'ABSPATH'
                        FROM
	                        VENDOR V
	                        INNER JOIN VENDOR_FINANCIAL_INFORMATION_AUDIT_REPORT VFIAR ON V.VENDOR_GUID = VFIAR.VENDOR_GUID
                        WHERE
	                        V.VENDOR_IS_ACTIVE = 'Y'
	                        AND V.VENDOR_GUID = @VGUID
	                        AND VFIAR.IS_ACTIVE = 'Y'";
                }
                else if (sector == "contacts")
                {
                    query =
                        @"SELECT
	                        D.DESIGNATION_NAME AS 'DESIGNAME',
	                        VCP.VCP_NAME AS 'NAME',
	                        ISNULL(CAST(VCP.TEL_NO AS VARCHAR(30)), '&nbsp;') AS TELNO,
	                        ISNULL(CAST(VCP.CELL_NO AS VARCHAR(30)), '&nbsp;') AS CELLNO,
	                        ISNULL(VCP.EMAIL, '&nbsp;') AS EMAIL,
	                        VCP.IS_POC AS 'POC',
	                        ISNULL(VCPV.FILE_NAME_UPLOADED, '&nbsp;') AS 'FILENAME',
	                        ISNULL(VCPV.FILE_EXTENTION, '&nbsp;') AS 'EXT',
	                        ISNULL(VCPV.FILE_SAVED_ROOT_PATH, '&nbsp;') AS 'ABSPATH'
                        FROM 
	                        VENDOR V
	                        LEFT JOIN VENDOR_CONTACT_PERSON VCP ON V.VENDOR_GUID = VCP.VENDOR_GUID
	                        LEFT JOIN DESIGNATION D ON VCP.DESIGNATION_GUID = D.DESIGNATION_GUID
	                        LEFT JOIN VENDOR_CONTACT_PERSON_VCARD VCPV ON VCP.VCP_GUID = VCPV.VCP_GUID
                        WHERE
	                        V.VENDOR_GUID = '" + vendor + "'";
                }
                else if (sector == "address")
                {
                    query = 
                        @"SELECT
	                        VAT.[TYPE_NAME] AS 'TYPE',
	                        VAM.STREET_ADDRESS AS 'STREET',
	                        VAM.CITY AS 'CITY',
	                        VAM.THANA AS 'THANA',
	                        VAM.COUNTRY AS 'COUNTRY',
	                        ISNULL(CAST(VAM.ZIP AS VARCHAR(10)), '') AS ZIP
                        FROM 
	                        VENDOR V
	                        LEFT JOIN VENDOR_ADDRESS_MAP VAM ON V.VENDOR_GUID = VAM.VENDOR_GUID
	                        LEFT JOIN VENDOR_ADDRESS_TYPE VAT ON VAM.VA_TYPE_GUID = VAT.VA_TYPE_GUID
                        WHERE
	                        V.VENDOR_GUID = '" + vendor + @"'
                        ORDER BY VAT.VA_TYPE_ID ASC";
                }


                ds = db.GetDataSet(query);

                var list = JsonConvert.SerializeObject(ds, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Content(list, "application/json");
            }
            else
                return null;
        }

        [HttpPost]
        [WebMethod(EnableSession = true)]
        [AllowAnonymous]
        public ActionResult PostApprovedSEF()
        {
            try
            {
                string vid = Request.Form["vid"];
                string userid = Session["USER_GUID"].ToString();

                if ((!string.IsNullOrEmpty(userid)) && (!string.IsNullOrEmpty(vid)))
                {
                    //INSERT SIF DATA
                    SEFApprove sefapprove = new SEFApprove();
                    sefapprove.VendorId = vid;
                    sefapprove.UserId = userid;
                    string vEmail = sefapprove.Approve();

                    // GEN ID PWD AND SET DB CREDENTIALS
                    Utilities util = new Utilities();
                    string tempId2 = util.RandomGenSEF();

                    // SEND EMAIL
                    MailHelper email = new MailHelper();
                    email.SEFTemplate(vEmail, tempId2);

                    return Json(new { result = true });
                }
            }
            catch (Exception ex) { ex.Message.ToString(); return Json(new { result = false }); }
            return Json(new { result = false });
        }

        [HttpPost]
        [WebMethod(EnableSession = true)]
        [AllowAnonymous]
        public ActionResult PostRejectSEF()
        {
            try
            {
                string vid = Request.Form["vid"];
                string remarks = Request.Form["remarks"];
                string userid = Session["USER_GUID"].ToString();

                if ((!string.IsNullOrEmpty(userid)) && (!string.IsNullOrEmpty(vid)))
                {
                    SEFReject reject = new SEFReject();
                    reject.UserId = userid;
                    reject.VendorId = vid;
                    reject.Remarks = remarks;
                    reject.setReject();

                    return Json(new { result = true });
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Json(new { result = false });
            }
            return Json(new { result = false });
        }
        // SEF PAGE FUNCTIONS ** END **
        #endregion "SEF Approval Page"
    }
}