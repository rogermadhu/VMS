using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VMS.App_Code;
using System.Data;

namespace VMS.Models
{
    public class Model_Enlistment
    {
        public Model_Enlistment() { }
        public DataTable GetVendorDetails(string vendorGuid)
        {
            DatabaseMSSQL db = new DatabaseMSSQL();
            DataTable ret;
            try
            {
                string query =
                    @"SELECT 
	                    v.VENDOR_GUID, 
	                    v.VENDOR_NAME, 
	                    vam.STREET_ADDRESS, 
	                    vam.CITY, 
	                    vam.THANA, 
	                    vam.COUNTRY, 
	                    vam.ZIP, 
	                    d.DESIGNATION_NAME AS 'DESIGNATION', 
	                    vcp.VCP_NAME AS 'CONTACT_NAME', 
	                    vcp.CELL_NO AS 'CELL_NO', 
	                    vcp.IS_POC 
                    FROM 
	                    VENDOR v 
	                    LEFT JOIN VENDOR_ADDRESS_MAP vam ON v.VENDOR_GUID = vam.VENDOR_GUID 
	                    LEFT JOIN VENDOR_ADDRESS_TYPE vat ON vam.VA_TYPE_GUID = vat.VA_TYPE_GUID 
	                    LEFT JOIN VENDOR_CONTACT_PERSON vcp ON v.VENDOR_GUID = vcp.VENDOR_GUID 
	                    LEFT JOIN DESIGNATION d ON vcp.DESIGNATION_GUID = d.DESIGNATION_GUID 
                    WHERE 
	                    v.VENDOR_GUID = '"+vendorGuid+@"' AND 
	                    vat.[TYPE_NAME] = 'Head / Corporate Office' AND 
	                    v.VENDOR_IS_ACTIVE = 'Y'"; 
                ret = db.GetDataTable(query); 
            }
            catch (Exception ex)
            {
                ret = null;
            }
            return ret;
        }
    }
    
    public class VendorTbl
    {
        private string tradeLicense;
        private string tinNo;
        private string telNo;
        private string cellNo;
        private string faxNo;
        private string email;
        private string dateOfEstablishment;
        private string totalNoOfEmployee;
        private string vendorStatus;
        #region "SETGET"
        public string TradeLicense
        {
            get
            {
                return tradeLicense;
            }

            set
            {
                tradeLicense = value;
            }
        }
        public string TinNo
        {
            get
            {
                return tinNo;
            }

            set
            {
                tinNo = value;
            }
        }
        public string TelNo
        {
            get
            {
                return telNo;
            }

            set
            {
                telNo = value;
            }
        }
        public string CellNo
        {
            get
            {
                return cellNo;
            }

            set
            {
                cellNo = value;
            }
        }
        public string FaxNo
        {
            get
            {
                return faxNo;
            }

            set
            {
                faxNo = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
        public string DateOfEstablishment
        {
            get
            {
                return dateOfEstablishment;
            }

            set
            {
                dateOfEstablishment = value;
            }
        }
        public string TotalNoOfEmployee
        {
            get
            {
                return totalNoOfEmployee;
            }

            set
            {
                totalNoOfEmployee = value;
            }
        }
        public string VendorStatus
        {
            get
            {
                return vendorStatus;
            }

            set
            {
                vendorStatus = value;
            }
        }
        #endregion "SETGET"

        public DataTable UpdateVendorTbl(string vendorGuid, VendorTbl v)
        {
            //[VENDOR_STATUS] = 'C' = COMPLETE
            DataTable ret;
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"UPDATE [VENDOR]
                    SET
	                    [TRADE_LICENSE] = '"+v.TradeLicense+@"'
	                    ,[TIN_NO] = '"+v.TinNo+ @"'
	                    ,[TEL_NO] = "+v.TelNo+ @"
	                    ,[CELL_NO] = "+v.CellNo+@"
	                    ,[FAX_NO] = "+v.FaxNo+ @"
	                    ,[EMAIL] = '"+v.Email+ @"'
	                    ,[DATE_OF_ESTABLISHMENT] = CAST('"+v.DateOfEstablishment+ @"' AS DATE)
	                    ,[TOTAL_NO_OF_EMPLOYEE] = "+v.TotalNoOfEmployee+@"
	                    ,[VENDOR_STATUS] = 'C'
                     WHERE 
	                    VENDOR_GUID = '"+vendorGuid+"'";
                ret = db.GetDataTable(query);
            }
            catch (Exception ex)
            {
                ret = null;
            }
            return ret;
        }
    }

    public class VendorType
    {
        private string vendorTypeName;
        private string vendorTypeId;

        #region "SETGET"
        public string VendorTypeName
        {
            get
            {
                return vendorTypeName;
            }

            set
            {
                vendorTypeName = value;
            }
        }

        public string VendorTypeId
        {
            get
            {
                return vendorTypeId;
            }

            set
            {
                vendorTypeId = value;
            }
        }
        #endregion "SETGET"

        public DataTable InsertVendorType(string vendorGuid, VendorType vt)
        {
            DataTable ret = null;
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query = 
                    @"";

            }
            catch(Exception ex)
            {
                ret = null;
            }
            return ret;
        }
    }


    //private string vendorGuid;
    //private string vendorName;
    //private string streetAddress;
    //private string city;
    //private string thana;
    //private string country;
    //private string zip;
    //private string designation;
    //private string contactName;
    //private string cellNo;
    //private string isPOC;
}