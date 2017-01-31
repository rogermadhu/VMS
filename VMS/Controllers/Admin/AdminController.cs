using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VMS.ClassUtility;
using VMS.Models;

namespace VMS.Controllers.Admin
{

    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Authentication.IsSetSession())
            {
                return View("index");
            }
            else
            {
                return RedirectToAction("../../login");
            }
        }

        public ActionResult ViewSIF()
        {

            return View("ViewSIF");
        }

        public ActionResult VendorsList()
        {

            return View("VendorsList");
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
        public ActionResult getVendorList()
        {
            App_Code.DatabaseMSSQL db = new App_Code.DatabaseMSSQL();
            string query =
                "SELECT " +
                    " COUNT(VENDOR_GUID) AS 'SL', " +
	                " VENDOR_CODE AS 'CODE', " +
	                " VENDOR_NAME AS 'VENDOR_NAME', " +
	                " WEBSITE AS 'WEBSITE', " +
	                " TEL_NO AS 'PHONE_NO', " +
	                " EMAIL AS 'EMAIL', " +
	                " VENDOR_STATUS AS 'STATUS' " +
                " FROM " +
                " VENDOR " +
                " GROUP BY VENDOR_GUID, VENDOR_CODE, VENDOR_NAME, WEBSITE, TEL_NO, EMAIL, VENDOR_STATUS";
            System.Data.DataTable dt = db.GetDataTable(query);

            //return Json(new { data = dt }, JsonRequestBehavior.AllowGet);

            var list = JsonConvert.SerializeObject(dt, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Content(list, "application/json");
        }
    }
}