using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VMS.App_Code;
namespace VMS.App_Code
{
    public class Utilities
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        public bool checkVendorNameExists(string vendorName)
        {
            string query =
                @"IF(EXISTS(SELECT * FROM VENDOR WHERE VENDOR_NAME = '"+ vendorName +@"'))
                BEGIN
	                SELECT 'EXISTS' AS RESPONSE
                END
                ELSE
                BEGIN
	                SELECT 'NOT EXISTS' AS RESPONSE
                END";
            string ret = db.GetDataTable(query).Rows[0][0].ToString().ToLower();
            if (ret == "exists")
                return true;
            else
                return false;
        }

        public bool checkVendorEmailExists(string vendorEmail)
        {
            string query =
                @"IF(EXISTS(SELECT * FROM VENDOR WHERE EMAIL = '"+vendorEmail+@"'))
                BEGIN
	                SELECT 'EXISTS' AS RESPONSE
                END
                ELSE
                BEGIN
	                SELECT 'NOT EXISTS' AS RESPONSE
                END";
            string ret = db.GetDataTable(query).Rows[0][0].ToString().ToLower();
            if (ret == "exists")
                return true;
            else
                return false;
        }

        public string RandomGenSIF()
        {
            string query =
                @"DECLARE @ON VARCHAR(10) = ISNULL((SELECT TOP (1) VENDOR_ID FROM VENDOR ORDER BY VENDOR_ID DESC),0)
                DECLARE @exclude VARCHAR(50) 
                SET @exclude = '0:;<=>?@O[]`^\/_-'
                DECLARE @char CHAR
                DECLARE @len CHAR
                DECLARE @output VARCHAR(50)
                SET @output = ''
                SET @len = 6

                WHILE @len > 0 BEGIN
                    SELECT @char = CHAR(round(rand() * 74 + 48, 0))
                    IF CHARINDEX(@char, @exclude) = 0 
	                BEGIN
                        SET @output = @output + @char
                        SET @len = @len - 1
                    END
                END

                SELECT 
                'FDL-'+RIGHT(YEAR(GETDATE()),2)+
                '-SIF-'+REPLICATE('0', 6-LEN(CONVERT(VARCHAR(6),CAST((CONVERT(INT, RIGHT(@ON, 6)) +1) AS VARCHAR(6)))))+
                CONVERT(VARCHAR(6),CAST((CONVERT(INT, RIGHT(@ON, 6)) +1) AS VARCHAR(6))) + '-' + UPPER(@output) AS VAL";
            string ret = db.GetDataTable(query).Rows[0][0].ToString();
            string chkQuery =
                @"IF(EXISTS(SELECT * FROM VENDOR WHERE TEMP_ID1 = '"+ ret + @"'))
                BEGIN
	                SELECT 'TRUE'
                END
                ELSE
                BEGIN
	                SELECT 'FALSE'
                END";
            if(db.GetDataTable(chkQuery).Rows[0][0].ToString() == "TRUE")
            { RandomGenSIF(); return false.ToString(); }
            else
            { return ret; }
        }

        public string RandomGenSEF()
        {
            string query =
                @"DECLARE @ON VARCHAR(10) = ISNULL((SELECT TOP (1) VENDOR_ID FROM VENDOR ORDER BY VENDOR_ID DESC),0)
                DECLARE @exclude VARCHAR(50) 
                SET @exclude = '0:;<=>?@O[]`^\/_-'
                DECLARE @char CHAR
                DECLARE @len CHAR
                DECLARE @output VARCHAR(50)
                SET @output = ''
                SET @len = 6

                WHILE @len > 0 BEGIN
                    SELECT @char = CHAR(round(rand() * 74 + 48, 0))
                    IF CHARINDEX(@char, @exclude) = 0 
	                BEGIN
                        SET @output = @output + @char
                        SET @len = @len - 1
                    END
                END

                SELECT 
                'FDL-'+RIGHT(YEAR(GETDATE()),2)+
                '-SEF-'+REPLICATE('0', 6-LEN(CONVERT(VARCHAR(6),CAST((CONVERT(INT, RIGHT(@ON, 6)) +1) AS VARCHAR(6)))))+
                CONVERT(VARCHAR(6),CAST((CONVERT(INT, RIGHT(@ON, 6)) +1) AS VARCHAR(6))) + '-' + UPPER(@output) AS VAL";
            string ret = db.GetDataTable(query).Rows[0][0].ToString();
            string chkQuery =
                @"IF(EXISTS(SELECT * FROM VENDOR WHERE TEMP_ID1 = '" + ret + @"'))
                BEGIN
	                SELECT 'TRUE'
                END
                ELSE
                BEGIN
	                SELECT 'FALSE'
                END";
            if (db.GetDataTable(chkQuery).Rows[0][0].ToString() == "TRUE")
            { RandomGenSIF(); return false.ToString(); }
            else
            { return ret; }
        }

        public string RandPwd()
        {
            string query =
                @"DECLARE @exclude VARCHAR(50) 
                SET @exclude = '0:;<=>?@O[]`^\/_-'
                DECLARE @char CHAR
                DECLARE @len CHAR
                DECLARE @output VARCHAR(50)
                SET @output = ''
                SET @len = 6

                WHILE @len > 0 BEGIN
                    SELECT @char = CHAR(round(rand() * 74 + 48, 0))
                    IF CHARINDEX(@char, @exclude) = 0 
	                BEGIN
                        SET @output = @output + @char
                        SET @len = @len - 1
                    END
                END

                SELECT UPPER(@output) AS PWD";
            return db.GetDataTable(query).Rows[0][0].ToString();
        }
    }
}