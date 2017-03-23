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

            /*
             * DATABASE FLAG STATUS OPTIONS
                1.  SIF after submit 
                        VENDOR_IS_ACTIVE = Y
                        VENDOR_STATUS = P
                    1a  SIF approved
                            VENDOR_STATUS = A       <-- login redirect to SEF for vendor
                    1b  SIF rejected
                            VENDOR_STATUS = R
                2.  SEF after submit
                        IS_AGREED = Y
                        VENDOR_STATUS = S
                    2a  SEF approved
                            VENDOR_STATUS = E
                    2b  SEF rejected
                            VENDOR_STATUS = F
                3.  AUDIT
                    3a  AUDIT accepted
                            VENDOR_STATUS = Y       <-- vendor login portal if required
                    3b  AUDIT rejected
                            IS_APPROVED
                            VENDOR_STATUS = N
                            VENDOR_IS_ACTIVE = N
            */
            string query =
                @"DECLARE @un varchar(max) BEGIN SET @un = '"  +ml.un + @"' END
                DECLARE @pw varchar(max) BEGIN SET @pw = '" + ml.pw + @"' END
                DECLARE @IsValid char(1) BEGIN SET @IsValid = 'N' END
                DECLARE @IsVendor char(1) BEGIN SET @IsVendor = '' END
                DECLARE @Status CHAR(1) BEGIN SET @STATUS = '' END

                IF EXISTS(SELECT TOP(1) 1 FROM [USER] WHERE USER_EMAIL = @un AND USER_PWD = @pw AND USER_IS_ACTIVE = 'Y')
                BEGIN
	                BEGIN SET @IsValid = 'Y' END
	                BEGIN SET @IsVendor = 'N' END
                END
                ELSE
                BEGIN
	                SET @Status = (
		                SELECT TOP(1) 
			                VENDOR_STATUS
		                FROM 
			                VENDOR 
		                WHERE 
			                [PASSWORD] = @pw 
			                AND VENDOR_IS_ACTIVE = 'Y'
			                AND (
				                TEMP_ID1 = @un
				                OR VENDOR_CODE = @un
				                OR USERNAME = @un
			                )
	                )
	                BEGIN SET @IsValid = 'Y' END
	                BEGIN SET @IsVendor = 'Y' END
                END

                SELECT @IsValid AS 'VALID', @IsVendor AS 'VENDOR', @Status AS 'STATUS'";

            return db.GetDataTable(query);
        }
    }
}