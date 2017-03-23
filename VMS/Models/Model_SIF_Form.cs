using System;
using System.Collections.Generic;
using VMS.App_Code;


namespace VMS.Models
{
    public class Vendor_SIF_Form
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendorName;
        private DateTime doe;
        private string website;
        private string email;

        public string VendorName
        {
            get
            {
                return vendorName;
            }

            set
            {
                vendorName = value;
            }
        }
        public DateTime Doe
        {
            get
            {
                return doe;
            }

            set
            {
                doe = value;
            }
        }
        public string Website
        {
            get
            {
                return website;
            }

            set
            {
                website = value;
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

        public string InsertVendor(Vendor_SIF_Form vendor)
        {
            string vendorGUID = string.Empty;
            try
            {
                string query =
                    @"INSERT INTO VENDOR (VENDOR_NAME, DATE_OF_ESTABLISHMENT, WEBSITE, EMAIL, USERNAME) 
                    VALUES ('" + vendor.vendorName + "', CAST('" + vendor.Doe + "' AS DATE), '" + vendor.website + "','"+ vendor.email + "','" + vendor.email + "'); " +
                    "SELECT VENDOR_GUID FROM VENDOR WHERE VENDOR_ID = SCOPE_IDENTITY();";
                vendorGUID = db.GetDataTable(query).Rows[0].ItemArray[0].ToString();
            }
            catch (Exception ex)
            {
                vendorGUID = string.Empty;
            }
            return vendorGUID;
        }
    }
    public class Model_Business_Type
    {
        DatabaseMSSQL db = new DatabaseMSSQL();
        private string businessTypeName;
        private string vendorGUID;

        public string BusinessTypeName
        {
            get
            {
                return businessTypeName;
            }

            set
            {
                businessTypeName = value;
            }
        }
        public string VendorGUID
        {
            get
            {
                return vendorGUID;
            }

            set
            {
                vendorGUID = value;
            }
        }

        public bool SearchMapBusinessType(Model_Business_Type bt)
        {
            bool ret = true;
            try
            {
                string query =
                    @"DECLARE @VendorGUID VARCHAR(36) = '"+bt.vendorGUID+@"';
                    DECLARE @TOBName NVARCHAR(50) = '"+bt.businessTypeName+@"';
                    DECLARE @InsertGUID VARCHAR(36);
                    DECLARE @IsInserted CHAR(1) = 'N';

                    IF(NOT EXISTS(SELECT * FROM TYPE_OF_BUSINESS WHERE TOB_NAME = @TOBName))
                    BEGIN
	                    -- INSERT INTO 
	                    INSERT INTO TYPE_OF_BUSINESS (TOB_NAME) VALUES (@TOBName);
	                    BEGIN SET @InsertGUID = (SELECT TOB_GUID FROM TYPE_OF_BUSINESS WHERE TOB_ID = SCOPE_IDENTITY()) END
	                    BEGIN SET @IsInserted = 'Y' END
                    END

                    IF(@IsInserted = 'Y')
                    BEGIN
	                    INSERT INTO VENDOR_TOB_MAP (VENDOR_GUID, TOB_GUID, IS_OTHER) VALUES (@VendorGUID, @InsertGUID, 'Y')
                    END
                    ELSE
                    BEGIN
	                    INSERT INTO VENDOR_TOB_MAP (VENDOR_GUID, TOB_GUID) VALUES (@VendorGUID, (SELECT TOB_GUID FROM TYPE_OF_BUSINESS WHERE TOB_NAME = @TOBName))
                    END";
                db.ExecuteNonQuery(query);
            }
            catch(Exception ex)
            {
                ret = false;
            }
            return ret;
        }
    }
    public class Model_Address
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendorGUID;
        private string streetAddress;
        private string city;
        private string thana;
        private string country;

        public string VendorGUID
        {
            get
            {
                return vendorGUID;
            }

            set
            {
                vendorGUID = value;
            }
        }
        public string StreetAddress
        {
            get
            {
                return streetAddress;
            }

            set
            {
                streetAddress = value;
            }
        }
        public string City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }
        public string Thana
        {
            get
            {
                return thana;
            }

            set
            {
                thana = value;
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

        public bool Insert_Address_HO(Model_Address ho)
        {
            bool ret = true;
            try
            {
                string query =
                    @"INSERT INTO VENDOR_ADDRESS_MAP (VENDOR_GUID, VA_TYPE_GUID, STREET_ADDRESS, CITY, THANA, COUNTRY) 
                    VALUES ('"+ho.vendorGUID+ "', '2BE91A9F-74D7-4776-9CD2-CF0CD71C3630', '"+ho.streetAddress+"', '"+ho.city+"', '"+ho.thana+"', '"+ho.country+"')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
        public bool Insert_Address_SO(Model_Address so)
        {
            bool ret = true;
            try
            {
                string query =
                    @"INSERT INTO VENDOR_ADDRESS_MAP (VENDOR_GUID, VA_TYPE_GUID, STREET_ADDRESS, CITY, THANA, COUNTRY) 
                    VALUES ('" + so.vendorGUID + "', '8F7F51E7-B55B-409E-9353-35BC4C2D6AB3', '" + so.streetAddress + "', '" + so.city + "', '" + so.thana + "', '" + so.country + "')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
        public bool Insert_Address_Factory(Model_Address factory)
        {
            bool ret = true;
            try
            {
                string query =
                    @"INSERT INTO VENDOR_ADDRESS_MAP (VENDOR_GUID, VA_TYPE_GUID, STREET_ADDRESS, CITY, THANA, COUNTRY) 
                    VALUES ('" + factory.vendorGUID + "', '07005AC7-861E-480E-9C71-E016FF18175C', '" + factory.streetAddress + "', '" + factory.city + "', '" + factory.thana + "', '" + factory.country + "')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
        public bool Insert_Address_Warehouse(Model_Address warehouse)
        {
            bool ret = true;
            try
            {
                string query =
                    @"INSERT INTO VENDOR_ADDRESS_MAP (VENDOR_GUID, VA_TYPE_GUID, STREET_ADDRESS, CITY, THANA, COUNTRY) 
                    VALUES ('" + warehouse.vendorGUID + "', '75134408-A91D-41D2-AD3E-DC31017A0671', '" + warehouse.streetAddress + "', '" + warehouse.city + "', '" + warehouse.thana + "', '" + warehouse.country + "')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
    }
    public class Model_Contact
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendorGUID;
        private string designation;
        private string name;
        private string phone;
        private string email;
        private string isPOC;

        public string VendorGUID
        {
            get
            {
                return vendorGUID;
            }

            set
            {
                vendorGUID = value;
            }
        }
        public string Designation
        {
            get
            {
                return designation;
            }

            set
            {
                designation = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }

            set
            {
                phone = value;
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
        public string IsPOC
        {
            get
            {
                return isPOC;
            }

            set
            {
                isPOC = value;
            }
        }

        public bool InsertContact(Model_Contact contact)
        {
            bool ret = true;
            try
            {
                string query =
                    @"DECLARE @Designation NVARCHAR(50) = '"+designation+@"';
                    DECLARE @DesignationGUID VARCHAR(36);
                    BEGIN SET @DesignationGUID = ISNULL((SELECT DESIGNATION_GUID FROM DESIGNATION WHERE DESIGNATION_NAME = @Designation),'0') END

                    IF (@DesignationGUID = '0')
                    BEGIN
	                    INSERT INTO DESIGNATION (DESIGNATION_NAME) VALUES (@Designation)
	                    BEGIN SET @DesignationGUID = (SELECT DESIGNATION_GUID FROM DESIGNATION WHERE DESIGNATION_ID = SCOPE_IDENTITY()) END
                    END

                    INSERT INTO VENDOR_CONTACT_PERSON (VENDOR_GUID, DESIGNATION_GUID, VCP_NAME, TEL_NO, EMAIL)
                    VALUES ('"+ contact.vendorGUID + "', @DesignationGUID, '"+ contact.name + "', '"+ contact.phone + "', '"+ contact.email + "')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        public bool InsertContactRepresentative(Model_Contact contact)
        {
            bool ret = true;
            try
            {
                string query =
                    @"DECLARE @Designation NVARCHAR(50) = '" + designation + @"';
                    DECLARE @DesignationGUID VARCHAR(36);
                    BEGIN SET @DesignationGUID = ISNULL((SELECT DESIGNATION_GUID FROM DESIGNATION WHERE DESIGNATION_NAME = @Designation),'0') END

                    IF (@DesignationGUID = '0')
                    BEGIN
	                    INSERT INTO DESIGNATION (DESIGNATION_NAME) VALUES (@Designation)
	                    BEGIN SET @DesignationGUID = (SELECT DESIGNATION_GUID FROM DESIGNATION WHERE DESIGNATION_ID = SCOPE_IDENTITY()) END
                    END

                    INSERT INTO VENDOR_CONTACT_PERSON (VENDOR_GUID, DESIGNATION_GUID, VCP_NAME, TEL_NO, EMAIL, IS_POC)
                    VALUES ('" + contact.vendorGUID + "', @DesignationGUID, '" + contact.name + "', '" + contact.phone + "', '" + contact.email + "','Y')";
                db.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
    }
    public class Model_Product
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendorGUID;
        private string productName;
        private string productCategory;
        private string isService;

        public string VendorGUID
        {
            get
            {
                return vendorGUID;
            }

            set
            {
                vendorGUID = value;
            }
        }
        public string ProductName
        {
            get
            {
                return productName;
            }

            set
            {
                productName = value;
            }
        }
        public string ProductCategory
        {
            get
            {
                return productCategory;
            }

            set
            {
                productCategory = value;
            }
        }
        public string IsService
        {
            get
            {
                return isService;
            }

            set
            {
                isService = value;
            }
        }

        public bool InsertProduct(Model_Product mp)
        {
            bool flag = true;
            try
            {
                string query =
                    @"DECLARE @VendorGuid VARCHAR(36) = '"+mp.vendorGUID+@"'
                    DECLARE @ProductName VARCHAR(36) = '"+mp.productName+@"'
                    DECLARE @ProductCategoryName VARCHAR(36) = '"+mp.productCategory+@"'
                    DECLARE @IsService CHAR(1) = '"+mp.isService+@"'
                    DECLARE @PRODUCT_CATEGORY_GUID VARCHAR(36)

                    BEGIN SET @PRODUCT_CATEGORY_GUID = ISNULL((SELECT PRODUCT_CATEGORY_GUID FROM PRODUCT_CATEGORY WHERE CATEGORY_NAME = @ProductCategoryName),'0') END
                    print(@PRODUCT_CATEGORY_GUID)

                    IF((@PRODUCT_CATEGORY_GUID = NULL) OR (@PRODUCT_CATEGORY_GUID = '0'))
                    BEGIN
	                    INSERT INTO PRODUCT_CATEGORY (CATEGORY_NAME, IS_SERVICE) VALUES (@ProductCategoryName, @IsService)
	                    BEGIN SET @PRODUCT_CATEGORY_GUID = (SELECT PRODUCT_CATEGORY_GUID FROM PRODUCT_CATEGORY WHERE PRODUCT_CATEGORY_ID = SCOPE_IDENTITY()) END;
	                    print(@PRODUCT_CATEGORY_GUID);
                    END

                    INSERT INTO PRODUCT_MAP (VENDOR_GUID, PRODUCT_NAME, PRODUCT_CATEGORY_GUID, IS_SERVICE)
                    VALUES (@VendorGuid, @ProductName, @PRODUCT_CATEGORY_GUID, @IsService)";
                db.ExecuteNonQuery(query);
            }
            catch(Exception ex)
            {
                flag = false;
            }
            return flag;
        }
    }
    public class VCard
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendorGuid;
        private string vcpName;
        private string fileNameUploaded;
        private string fileExtention;
        private string fileRenamed;
        private string fileSavedRootPath;
        private string fileSavedAbsolutePath;
        #region "setter"
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
        public string VcpName
        {
            get
            {
                return vcpName;
            }

            set
            {
                vcpName = value;
            }
        }
        public string FileNameUploaded
        {
            get
            {
                return fileNameUploaded;
            }

            set
            {
                fileNameUploaded = value;
            }
        }
        public string FileExtention
        {
            get
            {
                return fileExtention;
            }

            set
            {
                fileExtention = value;
            }
        }
        public string FileRenamed
        {
            get
            {
                return fileRenamed;
            }

            set
            {
                fileRenamed = value;
            }
        }
        public string FileSavedRootPath
        {
            get
            {
                return fileSavedRootPath;
            }

            set
            {
                fileSavedRootPath = value;
            }
        }
        public string FileSavedAbsolutePath
        {
            get
            {
                return fileSavedAbsolutePath;
            }

            set
            {
                fileSavedAbsolutePath = value;
            }
        }
        #endregion "setter"

        public void InsertVCard()
        {
            string query =
                @"DECLARE @name NVARCHAR(50) = '"+ vcpName + @"'
                DECLARE @vendorGuid VARCHAR(36) = '"+ vendorGuid + @"'
                DECLARE @vcpGuid VARCHAR(36)

                SET @vcpGuid = (SELECT
	                VCP.VCP_GUID
                FROM
	                VENDOR V 
	                LEFT JOIN VENDOR_CONTACT_PERSON VCP ON V.VENDOR_GUID = VCP.VENDOR_GUID
                WHERE 
	                V.VENDOR_GUID = @vendorGuid 
	                AND	REPLACE(LTRIM(RTRIM(VCP.VCP_NAME)), ' ' , '') = @name)

                IF(EXISTS(SELECT * FROM VENDOR_CONTACT_PERSON_VCARD WHERE VCP_GUID = @vcpGuid))
                BEGIN
	                UPDATE VENDOR_CONTACT_PERSON_VCARD SET IS_ACTIVE = 'N' WHERE VCP_GUID = @vcpGuid

	                INSERT INTO VENDOR_CONTACT_PERSON_VCARD 
	                (VCP_GUID, FILE_NAME_UPLOADED, FILE_EXTENTION, FILE_RENAMED, FILE_SAVED_ROOT_PATH, FILE_SAVED_ABSOLUTE_PATH)
	                VALUES
	                (@vcpGuid, 'FILE_NAME_UPLOADED', 'FILE_EXTENTION', 'FILE_RENAMED', 'FILE_SAVED_ROOT_PATH', 'FILE_SAVED_ABSOLUTE_PATH')
                END
                ELSE
                BEGIN
	                INSERT INTO VENDOR_CONTACT_PERSON_VCARD 
	                (VCP_GUID, FILE_NAME_UPLOADED, FILE_EXTENTION, FILE_RENAMED, FILE_SAVED_ROOT_PATH, FILE_SAVED_ABSOLUTE_PATH)
	                VALUES
	                (@vcpGuid, '"+ fileNameUploaded + @"', '"+ fileExtention + @"', '"+ fileRenamed + @"', '"+ fileSavedRootPath + @"', '"+ fileSavedAbsolutePath + @"')
                END";
            db.ExecuteNonQuery(query);
        }
    }
    public class PImage
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendorGuid;
        private string pName;
        private string fileNameUploaded;
        private string fileExtention;
        private string fileRenamed;
        private string fileSavedRootPath;
        private string fileSavedAbsolutePath;
        #region "setter"
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
        public string PName
        {
            get
            {
                return pName;
            }

            set
            {
                pName = value;
            }
        }
        public string FileNameUploaded
        {
            get
            {
                return fileNameUploaded;
            }

            set
            {
                fileNameUploaded = value;
            }
        }
        public string FileExtention
        {
            get
            {
                return fileExtention;
            }

            set
            {
                fileExtention = value;
            }
        }
        public string FileRenamed
        {
            get
            {
                return fileRenamed;
            }

            set
            {
                fileRenamed = value;
            }
        }
        public string FileSavedRootPath
        {
            get
            {
                return fileSavedRootPath;
            }

            set
            {
                fileSavedRootPath = value;
            }
        }
        public string FileSavedAbsolutePath
        {
            get
            {
                return fileSavedAbsolutePath;
            }

            set
            {
                fileSavedAbsolutePath = value;
            }
        }
        #endregion "setter"

        public void InsertProductImage()
        {
            string query =
                @"DECLARE @pname NVARCHAR(50) = '" + pName + @"'
                DECLARE @vendorGuid VARCHAR(36) = '" + vendorGuid + @"'
                DECLARE @pGuid VARCHAR(36)

                SET @pGuid = (SELECT
				                p.PRODUCT_GUID
			                FROM
				                VENDOR V 
				                LEFT JOIN PRODUCT_MAP P ON V.VENDOR_GUID = P.VENDOR_GUID
				                LEFT JOIN PRODUCT_CATALOGUE_IMAGE_MAP pc ON p.PRODUCT_GUID = pc.PRODUCT_GUID
			                WHERE 
				                V.VENDOR_GUID = @vendorGuid 
				                AND	REPLACE(LTRIM(RTRIM(p.PRODUCT_NAME)), ' ' , '') = @pname)

                INSERT INTO PRODUCT_CATALOGUE_IMAGE_MAP 
                (PRODUCT_GUID, FILE_NAME_UPLOADED, FILE_EXTENTION, FILE_RENAMED, FILE_SAVED_ROOT_PATH, FILE_SAVED_ABSOLUTE_PATH)
                VALUES
                (@pGuid, '" + fileNameUploaded + @"', '" + fileExtention + @"', '" + fileRenamed + @"', '" + fileSavedRootPath + @"', '" + fileSavedAbsolutePath + @"')";
            db.ExecuteNonQuery(query);
        }
    }










        //public class Model_SIF_Form
        //{
        //    private string vendor_name;
        //    private DateTime date_of_establishment;
        //    private string website;
        //    private IList<TOB_MAP> tob;
        //    private IList<VADDRESS> vaddress;
        //    private IList<PRODUCT> product;
        //    private IList<PRODUCT_CATALOGUE> catalogue;
        //    private IList<CONTACT_PERSON> contact;
        //    private IList<VCARD> vcard;
        //}
        //public class TOB_MAP
        //{
        //    private string vendor_guid;
        //    private string tob_guid;
        //    private string other_name;
        //    private string is_other;

        //    public string Vendor_guid
        //    {
        //        get
        //        {
        //            return vendor_guid;
        //        }

        //        set
        //        {
        //            vendor_guid = value;
        //        }
        //    }
        //    public string Tob_guid
        //    {
        //        get
        //        {
        //            return tob_guid;
        //        }

        //        set
        //        {
        //            tob_guid = value;
        //        }
        //    }
        //    public string Other_name
        //    {
        //        get
        //        {
        //            return other_name;
        //        }

        //        set
        //        {
        //            other_name = value;
        //        }
        //    }
        //    public string Is_other
        //    {
        //        get
        //        {
        //            return is_other;
        //        }

        //        set
        //        {
        //            is_other = value;
        //        }
        //    }
        //    public string Insert_TOB_MAP(TOB_MAP tob)
        //    {
        //        string query = "INSERT INTO ";

        //        DatabaseMSSQL db = new DatabaseMSSQL();
        //        return db.getSingleValue(query).ToString();

        //    }
        //}
        //public class VADDRESS
        //{
        //    private string vendor_guid;
        //    private string va_type_guid;
        //    private string street_address;
        //    private string city;
        //    private string state;
        //    private string country;
        //    private string zip;

        //    public string Vendor_guid
        //    {
        //        get
        //        {
        //            return vendor_guid;
        //        }

        //        set
        //        {
        //            vendor_guid = value;
        //        }
        //    }
        //    public string Va_type_guid
        //    {
        //        get
        //        {
        //            return va_type_guid;
        //        }

        //        set
        //        {
        //            va_type_guid = value;
        //        }
        //    }
        //    public string Street_address
        //    {
        //        get
        //        {
        //            return street_address;
        //        }

        //        set
        //        {
        //            street_address = value;
        //        }
        //    }
        //    public string City
        //    {
        //        get
        //        {
        //            return city;
        //        }

        //        set
        //        {
        //            city = value;
        //        }
        //    }
        //    public string State
        //    {
        //        get
        //        {
        //            return state;
        //        }

        //        set
        //        {
        //            state = value;
        //        }
        //    }
        //    public string Country
        //    {
        //        get
        //        {
        //            return country;
        //        }

        //        set
        //        {
        //            country = value;
        //        }
        //    }
        //    public string Zip
        //    {
        //        get
        //        {
        //            return zip;
        //        }

        //        set
        //        {
        //            zip = value;
        //        }
        //    }
        //}
        //public class PRODUCT
        //{
        //    private string vendor_guid;
        //    private string product_name;
        //    private string descripton;
        //    private string product_category_guid;
        //    private char is_service;
        //    private char is_obsolete;
        //    private char is_active;
        //    private char is_own_production;
        //    private DateTime create_date;
        //    private string create_by;

        //    public string Vendor_guid
        //    {
        //        get
        //        {
        //            return vendor_guid;
        //        }

        //        set
        //        {
        //            vendor_guid = value;
        //        }
        //    }
        //    public string Product_name
        //    {
        //        get
        //        {
        //            return product_name;
        //        }

        //        set
        //        {
        //            product_name = value;
        //        }
        //    }
        //    public string Descripton
        //    {
        //        get
        //        {
        //            return descripton;
        //        }

        //        set
        //        {
        //            descripton = value;
        //        }
        //    }
        //    public string Product_category_guid
        //    {
        //        get
        //        {
        //            return product_category_guid;
        //        }

        //        set
        //        {
        //            product_category_guid = value;
        //        }
        //    }
        //    public char Is_service
        //    {
        //        get
        //        {
        //            return is_service;
        //        }

        //        set
        //        {
        //            is_service = value;
        //        }
        //    }
        //    public char Is_obsolete
        //    {
        //        get
        //        {
        //            return is_obsolete;
        //        }

        //        set
        //        {
        //            is_obsolete = value;
        //        }
        //    }
        //    public char Is_active
        //    {
        //        get
        //        {
        //            return is_active;
        //        }

        //        set
        //        {
        //            is_active = value;
        //        }
        //    }
        //    public char Is_own_production
        //    {
        //        get
        //        {
        //            return is_own_production;
        //        }

        //        set
        //        {
        //            is_own_production = value;
        //        }
        //    }
        //    public DateTime Create_date
        //    {
        //        get
        //        {
        //            return create_date;
        //        }

        //        set
        //        {
        //            create_date = value;
        //        }
        //    }
        //    public string Create_by
        //    {
        //        get
        //        {
        //            return create_by;
        //        }

        //        set
        //        {
        //            create_by = value;
        //        }
        //    }
        //}
        //public class PRODUCT_CATALOGUE
        //{
        //    private string product_guid;
        //    private string file_name_uploaded;
        //    private string file_extention;
        //    private string file_renamed;
        //    private string file_saved_root_path;
        //    private string file_saved_absolute_path;
        //    private DateTime date_uploaded;

        //    public string Product_guid
        //    {
        //        get
        //        {
        //            return product_guid;
        //        }

        //        set
        //        {
        //            product_guid = value;
        //        }
        //    }
        //    public string File_name_uploaded
        //    {
        //        get
        //        {
        //            return file_name_uploaded;
        //        }

        //        set
        //        {
        //            file_name_uploaded = value;
        //        }
        //    }
        //    public string File_extention
        //    {
        //        get
        //        {
        //            return file_extention;
        //        }

        //        set
        //        {
        //            file_extention = value;
        //        }
        //    }
        //    public string File_renamed
        //    {
        //        get
        //        {
        //            return file_renamed;
        //        }

        //        set
        //        {
        //            file_renamed = value;
        //        }
        //    }
        //    public string File_saved_root_path
        //    {
        //        get
        //        {
        //            return file_saved_root_path;
        //        }

        //        set
        //        {
        //            file_saved_root_path = value;
        //        }
        //    }
        //    public string File_saved_absolute_path
        //    {
        //        get
        //        {
        //            return file_saved_absolute_path;
        //        }

        //        set
        //        {
        //            file_saved_absolute_path = value;
        //        }
        //    }
        //    public DateTime Date_uploaded
        //    {
        //        get
        //        {
        //            return date_uploaded;
        //        }

        //        set
        //        {
        //            date_uploaded = value;
        //        }
        //    }
        //}
        //public class CONTACT_PERSON
        //{
        //    private string vendor_guid;
        //    private string designation_guid;
        //    private string vcp_name;
        //    private Int64 tel_no;
        //    private Int64 cell_no;
        //    private string email;
        //    private char is_poc;
        //    private string create_by;

        //    public string Vendor_guid
        //    {
        //        get
        //        {
        //            return vendor_guid;
        //        }

        //        set
        //        {
        //            vendor_guid = value;
        //        }
        //    }
        //    public string Designation_guid
        //    {
        //        get
        //        {
        //            return designation_guid;
        //        }

        //        set
        //        {
        //            designation_guid = value;
        //        }
        //    }
        //    public string Vcp_name
        //    {
        //        get
        //        {
        //            return vcp_name;
        //        }

        //        set
        //        {
        //            vcp_name = value;
        //        }
        //    }
        //    public long Tel_no
        //    {
        //        get
        //        {
        //            return tel_no;
        //        }

        //        set
        //        {
        //            tel_no = value;
        //        }
        //    }
        //    public long Cell_no
        //    {
        //        get
        //        {
        //            return cell_no;
        //        }

        //        set
        //        {
        //            cell_no = value;
        //        }
        //    }
        //    public string Email
        //    {
        //        get
        //        {
        //            return email;
        //        }

        //        set
        //        {
        //            email = value;
        //        }
        //    }
        //    public char Is_poc
        //    {
        //        get
        //        {
        //            return is_poc;
        //        }

        //        set
        //        {
        //            is_poc = value;
        //        }
        //    }
        //    public string Create_by
        //    {
        //        get
        //        {
        //            return create_by;
        //        }

        //        set
        //        {
        //            create_by = value;
        //        }
        //    }
        //}
        //public class VCARD
        //{
        //    private string vcp_guid;
        //    private string file_name_uploaded;
        //    private string file_extention;
        //    private string file_renamed;
        //    private string file_saved_root_path;
        //    private string file_saved_absolute_path;

        //    public string Vcp_guid
        //    {
        //        get
        //        {
        //            return vcp_guid;
        //        }

        //        set
        //        {
        //            vcp_guid = value;
        //        }
        //    }
        //    public string File_name_uploaded
        //    {
        //        get
        //        {
        //            return file_name_uploaded;
        //        }

        //        set
        //        {
        //            file_name_uploaded = value;
        //        }
        //    }
        //    public string File_extention
        //    {
        //        get
        //        {
        //            return file_extention;
        //        }

        //        set
        //        {
        //            file_extention = value;
        //        }
        //    }
        //    public string File_renamed
        //    {
        //        get
        //        {
        //            return file_renamed;
        //        }

        //        set
        //        {
        //            file_renamed = value;
        //        }
        //    }
        //    public string File_saved_root_path
        //    {
        //        get
        //        {
        //            return file_saved_root_path;
        //        }

        //        set
        //        {
        //            file_saved_root_path = value;
        //        }
        //    }
        //    public string File_saved_absolute_path
        //    {
        //        get
        //        {
        //            return file_saved_absolute_path;
        //        }

        //        set
        //        {
        //            file_saved_absolute_path = value;
        //        }
        //    }
        //}
    }

//Organization Name           VENDOR
//Date of Establishment           VENDOR
//Website Address VENDOR
//Type of Business(Multi)        TOB_MAP
//Organization Address(Multi)        VENDOR_ADDRESS_MAP
//Product / Service Name(Multi)      PRODUCT_MAP
//Image of Product or Service(Multi) PRPODUCT CATALOGUE IMAGE MAP
//Organization Contact(Multi)        VENDOR CONTACT PERSON
//Representation Name(Multi)     VENDOR CONTACT PERSON
//Visiting Card(Multi)           VENDOR CONTACT PERSON VCARD