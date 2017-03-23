using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VMS.App_Code;

namespace VMS.Models
{
    public class Model_Admin
    {
    }

    public class VendorList
    {

    }




    public class SIFApprove
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendorId;
        private string userId;
        private string []vendorCategory;
        private string []vendorSubCategory;
        private string []poc;

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
        public string[] VendorCategory
        {
            get
            {
                return vendorCategory;
            }

            set
            {
                vendorCategory = value;
            }
        }
        public string[] VendorSubCategory
        {
            get
            {
                return vendorSubCategory;
            }

            set
            {
                vendorSubCategory = value;
            }
        }
        public string[] Poc
        {
            get
            {
                return poc;
            }

            set
            {
                poc = value;
            }
        }
        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        public bool Approve()
        {
            if (!string.IsNullOrEmpty(vendorId))
            {
                // parent category
                foreach (string category in vendorCategory)
                {
                    if (category.ToLower() != "all selected")
                    {
                        string tempVCGuid = checkInsertParentCategory(category, vendorId);

                        foreach (string subCategory in vendorSubCategory)
                        {
                            if (subCategory.ToLower() != "all selected")
                            {
                                checkInsertSubCategory(vendorId, tempVCGuid, subCategory);
                            }
                        }
                    }
                }
                foreach (string p in poc)
                {
                    setPOC(vendorId, p);
                }
                setVendorApproved(vendorId, userId);
                return true;
            }
            return false;
        }

        private string checkInsertParentCategory(string category, string vendorId)
        {
            string query =
                @"DECLARE @CAT VARCHAR(MAX) = '"+ category + @"'
                DECLARE @VID VARCHAR(MAX) = '"+ vendorId +@"'
                DECLARE @VC VARCHAR(MAX);

                IF(EXISTS(SELECT VC_GUID FROM VENDOR_CATEGORY WHERE	VC_NAME = @CAT AND IS_ACTIVE = 'Y' AND PARENT_CATEGORY_GUID IS NULL))
	                BEGIN SET @VC = (
		                SELECT 
			                VC_GUID
		                FROM 
			                VENDOR_CATEGORY 
		                WHERE
			                VC_NAME = @CAT
			                AND IS_ACTIVE = 'Y'
			                AND PARENT_CATEGORY_GUID IS NULL)
	                END
                ELSE
	                BEGIN
		                INSERT INTO VENDOR_CATEGORY (VC_NAME, IS_INSERTED) VALUES (@CAT, 'Y')
		                SET @VC = (SELECT VC_GUID FROM VENDOR_CATEGORY WHERE VC_ID = SCOPE_IDENTITY())
	                END

                IF(NOT EXISTS(SELECT * FROM VENDOR_VC_MAP WHERE VENDOR_GUID = @VID AND VC_GUID = @VC))
                BEGIN INSERT INTO VENDOR_VC_MAP (VENDOR_GUID, VC_GUID) VALUES (@VID, @VC) END
                SELECT @VC AS 'VC'";

            return db.GetDataTable(query).Rows[0][0].ToString();
        }

        private void checkInsertSubCategory(string vendorId, string category, string subcategory)
        {
            string query =
                @"DECLARE @VID VARCHAR(MAX) = '"+vendorId+@"'
                DECLARE @PARENT VARCHAR(MAX) = '"+category+@"'
                DECLARE @CHILD VARCHAR(MAX) = '"+subcategory+@"'
                DECLARE @TEMPGUID VARCHAR(36)

                IF(NOT EXISTS(SELECT VC_GUID FROM VENDOR_CATEGORY WHERE VC_NAME = @CHILD AND PARENT_CATEGORY_GUID = @PARENT AND IS_ACTIVE = 'Y'))
                BEGIN 
	                INSERT INTO VENDOR_CATEGORY (VC_NAME, PARENT_CATEGORY_GUID, IS_INSERTED) VALUES (@CHILD, @PARENT, 'Y')
	                SET @TEMPGUID = (SELECT VC_GUID FROM VENDOR_CATEGORY WHERE VC_ID = SCOPE_IDENTITY())
                END
                ELSE 
                BEGIN
	                SET @TEMPGUID = (SELECT VC_GUID FROM VENDOR_CATEGORY WHERE VC_NAME = @CHILD AND PARENT_CATEGORY_GUID = @PARENT AND IS_ACTIVE = 'Y')
                END

                IF(NOT EXISTS (SELECT VENDOR_VC_MAP_GUID FROM VENDOR_VC_MAP WHERE VENDOR_GUID = @VID AND VC_GUID = @TEMPGUID AND IS_ACTIVE = 'Y'))
                BEGIN
	                INSERT INTO VENDOR_VC_MAP (VENDOR_GUID, VC_GUID, IS_ACTIVE) VALUES (@VID, @TEMPGUID, 'Y')
                END";
            db.ExecuteNonQuery(query);
        }

        private void setPOC(string vendorGuid, string pocId)
        {
            string query =
                @"DECLARE @vendorGuid VARCHAR(36) = '"+ vendorGuid +@"'
                DECLARE @poc CHAR(1) = '"+pocId+@"'

                UPDATE VENDOR_CONTACT_PERSON SET IS_POC = 'N' WHERE VENDOR_GUID = @vendorGuid;
                UPDATE VENDOR_CONTACT_PERSON SET IS_POC = 'Y', MODIFY_DATE = GETDATE() WHERE VCP_GUID = @poc;";
            db.ExecuteNonQuery(query);
        }

        private void setVendorApproved(string vendorId, string userId)
        {
            string query = "UPDATE VENDOR SET IS_APPROVED = 'Y', VENDOR_STATUS = 'A', APPROVED_DATE = GETDATE(), MODIFY_DATE = GETDATE(), MODIFY_BY = '"+userId+"' WHERE VENDOR_GUID = '" + vendorId+"' AND VENDOR_IS_ACTIVE = 'Y'";
            db.ExecuteNonQuery(query);
        }

        public string setCredentials(string vendorId, string tempId, string pwd )
        {
            // USERNAME is email set from SIF.
            string query =
                @"UPDATE VENDOR SET TEMP_ID1 = '"+ tempId +@"', [PASSWORD] = '"+ pwd + @"' WHERE VENDOR_GUID = '" + vendorId + @"'
                SELECT USERNAME FROM VENDOR WHERE VENDOR_GUID = '" + vendorId +@"'";
            return db.GetDataTable(query).Rows[0][0].ToString();
        }
    }

    class SIFReject
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendorId;
        private string remarks;
        private string userId;

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
        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        public void setReject()
        {
            string query = "UPDATE VENDOR SET IS_APPROVED = 'N', MODIFY_DATE = GETDATE(), MODIFY_BY = '"+userId+"', REMARKS = '"+remarks+ "', VENDOR_IS_ACTIVE = 'N' WHERE VENDOR_GUID = '" + vendorId+"'";
            db.ExecuteNonQuery(query);
        }
    }




    public class SEFApprove
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendorId;
        private string userId;

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
        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }
        #endregion "setter"

        public string Approve()
        {
            string ret = "";

            try
            {
                string query =
                    @"UPDATE VENDOR SET VENDOR_STATUS = 'E', IS_AGREED = 'Y', MODIFY_BY = '" + userId + "', MODIFY_DATE = GETDATE() WHERE VENDOR_GUID = '" + vendorId + @"';
                    SELECT USERNAME FROM VENDOR WHERE VENDOR_GUID = '"+ vendorId +"'";
                ret = db.GetDataTable(query).Rows[0].ItemArray[0].ToString();
            }
            catch (Exception ex)
            { }
            return ret;
        }
    }
    class SEFReject
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendorId;
        private string remarks;
        private string userId;

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
        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }

        public void setReject()
        {
            string query = "UPDATE VENDOR SET IS_APPROVED = 'N', VENDOR_STATUS = 'F', MODIFY_DATE = GETDATE(), MODIFY_BY = '" + userId + "', REMARKS = '" + remarks + "', VENDOR_IS_ACTIVE = 'N' WHERE VENDOR_GUID = '" + vendorId + "'";
            db.ExecuteNonQuery(query);
        }
    }
}