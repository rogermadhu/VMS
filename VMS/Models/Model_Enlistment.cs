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
	                    vat.[TYPE_NAME] AS [TYPE]
                    FROM 
	                    VENDOR v 
	                    LEFT JOIN VENDOR_ADDRESS_MAP vam ON v.VENDOR_GUID = vam.VENDOR_GUID 
	                    LEFT JOIN VENDOR_ADDRESS_TYPE vat ON vam.VA_TYPE_GUID = vat.VA_TYPE_GUID 
                    WHERE 
	                    v.VENDOR_GUID = '" + vendorGuid + @"' AND 
	                    v.VENDOR_IS_ACTIVE = 'Y'";
                //@"SELECT 
                // v.VENDOR_GUID, 
                // v.VENDOR_NAME, 
                // vam.STREET_ADDRESS, 
                // vam.CITY, 
                // vam.THANA, 
                // vam.COUNTRY, 
                // vam.ZIP, 
                // d.DESIGNATION_NAME AS 'DESIGNATION', 
                // vcp.VCP_NAME AS 'CONTACT_NAME', 
                // vcp.CELL_NO AS 'CELL_NO', 
                // vcp.IS_POC 
                //FROM 
                // VENDOR v 
                // LEFT JOIN VENDOR_ADDRESS_MAP vam ON v.VENDOR_GUID = vam.VENDOR_GUID 
                // LEFT JOIN VENDOR_ADDRESS_TYPE vat ON vam.VA_TYPE_GUID = vat.VA_TYPE_GUID 
                // LEFT JOIN VENDOR_CONTACT_PERSON vcp ON v.VENDOR_GUID = vcp.VENDOR_GUID 
                // LEFT JOIN DESIGNATION d ON vcp.DESIGNATION_GUID = d.DESIGNATION_GUID 
                //WHERE 
                // v.VENDOR_GUID = '"+vendorGuid+@"' AND 
                // v.VENDOR_IS_ACTIVE = 'Y'"; 
                ret = db.GetDataTable(query);
            }
            catch (Exception ex)
            {
                ret = null;
            }
            return ret;
        }
        public DataTable GetVendorContact(string vendorGuid)
        {
            DatabaseMSSQL db = new DatabaseMSSQL();
            DataTable ret;
            try
            {
                string query =
                    @"SELECT 
	                    d.DESIGNATION_NAME AS 'DESIGNATION', 
	                    vcp.VCP_NAME AS 'CONTACT_NAME', 
	                    vcp.TEL_NO AS 'CELL_NO', 
	                    vcp.IS_POC 
                    FROM 
	                    VENDOR v 
	                    LEFT JOIN VENDOR_CONTACT_PERSON vcp ON v.VENDOR_GUID = vcp.VENDOR_GUID 
	                    LEFT JOIN DESIGNATION d ON vcp.DESIGNATION_GUID = d.DESIGNATION_GUID 
                    WHERE 
	                    v.VENDOR_GUID = '027CDC29-FBA0-4232-B70A-BDDD06608847' AND 
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
        private string vendorGuid;
        private string tradeLicense;
        private string tinNo;
        private string telNo;
        private string cellNo;
        private string faxNo;
        //private string email;
        private string dateOfEstablishment;
        private string totalNoOfEmployee;
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
        //public string Email
        //{
        //    get
        //    {
        //        return email;
        //    }

        //    set
        //    {
        //        email = value;
        //    }
        //}
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
        public string VendorGuid
        {
            get
            {
                return vendorGuid;
            }

            set
            {
                vendorGuid = value;
            }
        }
        #endregion "SETGET"

        public DataTable UpdateVendorTbl()
        {
            //[VENDOR_STATUS] = 'C' = COMPLETE
            DataTable ret;
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"UPDATE [VENDOR]
                    SET
	                    [TRADE_LICENSE] = '"+TradeLicense+@"'
	                    ,[TIN_NO] = '"+TinNo+ @"'
	                    ,[TEL_NO] = "+TelNo+ @"
	                    ,[CELL_NO] = "+CellNo+@"
	                    ,[FAX_NO] = "+FaxNo+ @"
	                    ,[DATE_OF_ESTABLISHMENT] = CAST('"+DateOfEstablishment+ @"' AS DATE)
	                    ,[TOTAL_NO_OF_EMPLOYEE] = "+TotalNoOfEmployee+@"
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
        private string vendorId;

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
        public string VendorId
        {
            get
            {
                return vendorId;
            }

            set
            {
                vendorId = value;
            }
        }
        #endregion "SETGET"

        public void InsertVendorType()
        {
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"DECLARE @VTYPE VARCHAR(100) = '"+ VendorTypeName +@"'
                    DECLARE @VID VARCHAR(36) = '"+ vendorId +@"' 

                    IF EXISTS (SELECT * FROM VENDOR_TYPE WHERE VENDOR_TYPE_NAME = @VTYPE)
                    BEGIN
	                    SET @VTYPE = (SELECT TOP(1) VENDOR_TYPE_GUID FROM VENDOR_TYPE WHERE VENDOR_TYPE_NAME = @VTYPE)
                    END
                    ELSE
                    BEGIN
	                    INSERT INTO VENDOR_TYPE (VENDOR_TYPE_NAME, IS_INSERTED) VALUES (@VTYPE, 'Y')
	                    SET @VTYPE = (SELECT VENDOR_TYPE_GUID FROM VENDOR_TYPE WHERE VENDOR_TYPE_ID = SCOPE_IDENTITY())
                    END

                    IF NOT EXISTS( SELECT * FROM VENDOR_TYPE_MAP WHERE VENDOR_GUID = @VID AND VENDOR_TYPE_GUID = @VTYPE)
                    BEGIN
                    INSERT INTO VENDOR_TYPE_MAP (VENDOR_GUID, VENDOR_TYPE_GUID) VALUES (@VID, @VTYPE);
                    END";
                db.ExecuteNonQuery(query);
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }

    public class VCT_Map
    {
        private string vendorId;
        private string vendorCompanyType;

        #region "setter"
        public string VendorId
        {
            get
            {
                return vendorId;
            }

            set
            {
                vendorId = value;
            }
        }
        public string VendorCompanyType
        {
            get
            {
                return vendorCompanyType;
            }

            set
            {
                vendorCompanyType = value;
            }
        }
        #endregion "setter"

        public void InsertVendorCompanyType()
        {
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"DECLARE @VTYPE VARCHAR(100) = '" + vendorCompanyType + @"'
                    DECLARE @VID VARCHAR(36) = '" + VendorId + @"' 

                    IF EXISTS (SELECT * FROM VENDOR_COMPANY_TYPE WHERE VENDOR_COMPANY_TYPE_NAME = @VTYPE)
                    BEGIN
	                    SET @VTYPE = (SELECT TOP(1) VENDOR_COMPANY_TYPE_GUID FROM VENDOR_COMPANY_TYPE WHERE VENDOR_COMPANY_TYPE_NAME = @VTYPE)
                    END
                    ELSE
                    BEGIN
	                    INSERT INTO VENDOR_COMPANY_TYPE (VENDOR_COMPANY_TYPE_NAME, IS_INSERTED) VALUES (@VTYPE, 'Y')
	                    SET @VTYPE = (SELECT VENDOR_COMPANY_TYPE_GUID FROM VENDOR_COMPANY_TYPE WHERE VENDOR_COMPANY_TYPE_ID = SCOPE_IDENTITY())
                    END

                    IF NOT EXISTS( SELECT * FROM VENDOR_COMPANY_TYPE_MAP WHERE VENDOR_GUID = @VID AND VENDOR_COMPANY_TYPE_MAP_GUID = @VTYPE)
                    BEGIN
	                    INSERT INTO VENDOR_COMPANY_TYPE_MAP (VENDOR_GUID, VENDOR_COMPANY_TYPE_MAP_GUID) VALUES (@VID, @VTYPE);
                    END";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }

    public class AnnualSales
    {
        private string vendorId;
        private string year;
        private string value;

        #region "setter"
        public string VendorId
        {
            get
            {
                return vendorId;
            }

            set
            {
                vendorId = value;
            }
        }
        public string Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
            }
        }
        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
        #endregion "setter"

        public void InsertAnnualSales()
        {
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"INSERT INTO VFI_ANNUAL_SALES (VENDOR_GUID, [YEAR], VALUE) VALUES ('"+ VendorId +"', '"+ year +"', '"+ value +"')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }

    public class VFI
    {
        private string vendorId;
        private string bankName;
        private string bankAddress;
        private string accountNo;
        private string accountName;
        private string swiftCode;
        private string routingNo;        

        #region "setter"
        public string BankName
        {
            get
            {
                return bankName;
            }

            set
            {
                bankName = value;
            }
        }
        public string BankAddress
        {
            get
            {
                return bankAddress;
            }

            set
            {
                bankAddress = value;
            }
        }
        public string AccountNo
        {
            get
            {
                return accountNo;
            }

            set
            {
                accountNo = value;
            }
        }
        public string AccountName
        {
            get
            {
                return accountName;
            }

            set
            {
                accountName = value;
            }
        }
        public string SwiftCode
        {
            get
            {
                return swiftCode;
            }

            set
            {
                swiftCode = value;
            }
        }
        public string RoutingNo
        {
            get
            {
                return routingNo;
            }

            set
            {
                routingNo = value;
            }
        }

        public string VendorId
        {
            get
            {
                return vendorId;
            }

            set
            {
                vendorId = value;
            }
        }
        #endregion "setter"

        public void InsertBankInformation()
        {
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"INSERT INTO VFI_BANK_INFORMATION_MAP (VENDOR_GUID, BANK_NAME, BANK_ADDRESS, ACCOUNT_NO, ACCOUNT_NAME, SWIFT_CODE, ROUTING_NO ) 
                    VALUES ('"+vendorId+"', '"+ bankName +"', '"+ bankAddress +"' ,'"+ accountNo +"' ,'"+ accountName +"' ,'"+ swiftCode +"', '"+ routingNo +"')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }

    public class IntlOfc
    {
        private string vendorId;
        private string country;

        public string VendorId
        {
            get
            {
                return vendorId;
            }

            set
            {
                vendorId = value;
            }
        }
        public string Country
        {
            get
            {
                return country;
            }

            set
            {
                country = value;
            }
        }

        public void Insert_Intl_Ofc()
        {
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"INSERT INTO VENDOR_INTL_OFC(VENDOR_GUID, VENDOR_INTL_OFC_COUNTRY) VALUES('"+vendorId+"', '"+country+"')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }

    public class CountryRawMaterialsFrom
    {
        private string vendorId;
        private string country;

        public string VendorId
        {
            get
            {
                return vendorId;
            }

            set
            {
                vendorId = value;
            }
        }
        public string Country
        {
            get
            {
                return country;
            }

            set
            {
                country = value;
            }
        }

        public void InsertRawMaterialList()
        {
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"INSERT INTO VENDOR_RAW_MATERIAL_IMPORT_LIST_MAP (VENDOR_GUID, COUNTRY_NAME) VALUES ('"+ vendorId +"','"+ country +"')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }


    public class EmployeeInformation
    {
        private string vendorId;
        private string expArea;
        private string noOfEmployee;
        private string remarks;

        #region "setter"
        public string VendorId
        {
            get
            {
                return vendorId;
            }

            set
            {
                vendorId = value;
            }
        }
        public string ExpArea
        {
            get
            {
                return expArea;
            }

            set
            {
                expArea = value;
            }
        }
        public string NoOfEmployee
        {
            get
            {
                return noOfEmployee;
            }

            set
            {
                noOfEmployee = value;
            }
        }
        public string Remarks
        {
            get
            {
                return remarks;
            }

            set
            {
                remarks = value;
            }
        }
        #endregion "setter"

        public void InsertEmployeeInformation()
        {
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"INSERT INTO VENDOR_EMPLOYEE_INFORMATION (VENDOR_GUID, VEI_EXPERIENCE_AREA, VEI_QUANTITY, VEI_REMARKS) VALUES ('"+vendorId+"','"+expArea+"','"+noOfEmployee+"','"+remarks+"')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }

    public class VendorExp
    {
        private string vendorId;
        private string org;
        private string amount;
        private string year;
        private string goods;
        private string destination;

        #region "setter"
        public string Org
        {
            get
            {
                return org;
            }

            set
            {
                org = value;
            }
        }
        public string Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
            }
        }
        public string Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
            }
        }
        public string Goods
        {
            get
            {
                return goods;
            }

            set
            {
                goods = value;
            }
        }
        public string Destination
        {
            get
            {
                return destination;
            }

            set
            {
                destination = value;
            }
        }

        public string VendorId
        {
            get
            {
                return vendorId;
            }

            set
            {
                vendorId = value;
            }
        }
        #endregion "setter"

        public void InsertVendorExp()
        {
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"INSERT INTO VENDOR_EXP (VENDOR_GUID, VENDOR_EXP_ORG_NAME, VENDOR_EXP_AMOUNT, VENDOR_EXP_YEAR, VENDOR_EXP_GOODS, VENDOR_EXP_DESTINATION) 
                    VALUES ('"+ vendorId +"','"+ org +"','"+ amount +"','"+ year +"','"+ goods +"','"+ destination +"')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }

    public class Machine
    {
        private string vendorId;
        private string machineName;
        private string brandName;
        private string origin;
        private string purpose;
        private string quantity;
        private string uom;

        #region "setter"
        public string VendorId
        {
            get
            {
                return vendorId;
            }

            set
            {
                vendorId = value;
            }
        }
        public string MachineName
        {
            get
            {
                return machineName;
            }

            set
            {
                machineName = value;
            }
        }
        public string BrandName
        {
            get
            {
                return brandName;
            }

            set
            {
                brandName = value;
            }
        }
        public string Origin
        {
            get
            {
                return origin;
            }

            set
            {
                origin = value;
            }
        }
        public string Purpose
        {
            get
            {
                return purpose;
            }

            set
            {
                purpose = value;
            }
        }
        public string Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }
        public string Uom
        {
            get
            {
                return uom;
            }

            set
            {
                uom = value;
            }
        }
        #endregion "setter"

        public void InsertMachine()
        {
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"INSERT INTO PRODUCT_MACHINE_MAP (VENDOR_GUID, PM_NAME, BRAND_NAME, PM_ORIGIN, PURPOSE, QUANTITY, UOM)
                    VALUES ('" + vendorId +"', '" + machineName +"','" + brandName +"','" + origin +"','" + purpose +"','" + quantity +"','" + uom +"')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }

    public class Certification
    {
        private string vendorId;
        private string certificationName;
        #region "setter"
        public string VendorId
        {
            get
            {
                return vendorId;
            }

            set
            {
                vendorId = value;
            }
        }
        public string CertificationName
        {
            get
            {
                return certificationName;
            }

            set
            {
                certificationName = value;
            }
        }
        #endregion "setter"

        public void InsertCertification()
        {
            try
            {
                DatabaseMSSQL db = new DatabaseMSSQL();
                string query =
                    @"DECLARE @VENDORGUID VARCHAR(36) = '"+ vendorId +@"'
                    DECLARE @CER VARCHAR(20) = '"+ certificationName +@"'
                    DECLARE @CER_GUID VARCHAR(36) = NULL;

                    IF EXISTS (SELECT * FROM CERTIFICATION_TYPE WHERE CERTIFICATION_NAME = @CER)
                    BEGIN
	                    SET @CER_GUID = (SELECT TOP(1) CERTIFICATION_TYPE_GUID FROM CERTIFICATION_TYPE WHERE CERTIFICATION_NAME = @CER)
                    END
                    ELSE
                    BEGIN
	                    INSERT INTO CERTIFICATION_TYPE (CERTIFICATION_NAME) VALUE (@CER)
	                    SET @CER_GUID = (SELECT CERTIFICATION_NAME FROM CERTIFICATION_TYPE WHERE CERTIFICATION_TYPE_ID = SCOPE_IDENTITY())
                    END

                    IF NOT EXISTS (SELECT * FROM VENDOR_CERTIFICATION WHERE VENDOR_GUID = @VENDORGUID AND CERTIFICATION_TYPE_GUID = @CER_GUID)
                    BEGIN
	                    INSERT INTO VENDOR_CERTIFICATION (VENDOR_GUID, CERTIFICATION_NAME, VENDOR_CERTIFICATION_GUID) VALUES (@VENDORGUID, @CER, @CER_GUID)
                    END";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
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