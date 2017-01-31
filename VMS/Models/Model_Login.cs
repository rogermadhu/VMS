using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VMS.App_Code;

namespace VMS.Models
{
    public class Model_Login
    {
        private string un;
        private string pw;

        public string Un
        {
            get
            {
                return un;
            }

            set
            {
                un = value;
            }
        }
        public string Pw
        {
            get
            {
                return pw;
            }

            set
            {
                pw = value;
            }
        }

        public DataTable ValidateUser(Model_Login ml)
        {
            DatabaseMSSQL db = new DatabaseMSSQL();

            string query =
                @"DECLARE @un varchar(max) BEGIN SET @un = '"  +ml.un + @"' END
                DECLARE @pw varchar(max) BEGIN SET @pw = '" + ml.pw + @"' END

                DECLARE @IsValid char(1) BEGIN SET @IsValid = 'N' END
                DECLARE @IsVendor char(1) BEGIN SET @IsVendor = '' END

                IF EXISTS(SELECT TOP(1) USERNAME, [PASSWORD] FROM VENDOR WHERE USERNAME = @un AND PASSWORD = @pw AND VENDOR_IS_ACTIVE = 'Y')
                BEGIN 
	                BEGIN SET @IsValid = 'Y' END
	                BEGIN SET @IsVendor = 'Y' END
                END
                ELSE
                IF EXISTS(SELECT TOP(1) USER_EMAIL, USER_PWD FROM [USER] WHERE USER_EMAIL = @un AND USER_PWD = @pw AND USER_IS_ACTIVE = 'Y')
                BEGIN
	                BEGIN SET @IsValid = 'Y' END
	                BEGIN SET @IsVendor = 'N' END
                END
                ELSE
                BEGIN
	                BEGIN SET @IsValid = 'N' END
	                BEGIN SET @IsVendor = '' END
                END
                SELECT @IsValid AS 'VALID', @IsVendor AS 'VENDOR'";

            return db.GetDataTable(query);
        }
    }
}