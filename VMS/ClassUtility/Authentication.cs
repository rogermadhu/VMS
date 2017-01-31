using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VMS.Models;
using VMS.App_Code;

namespace VMS.ClassUtility
{
    public static class Authentication
    {

        public static bool SetSessionLogin(string username, string password, string type)
        {
            bool ret = false;
            DataTable get = new DataTable();
            DatabaseMSSQL db = new DatabaseMSSQL();
            string query = string.Empty;

            if (type == "ADMIN")
            {
                query =
                    @"SELECT TOP(1) 
	                    [USER_GUID]
	                    ,[USER_ID]
	                    ,[USER_NAME_DISPLAY]
	                    ,[USER_EMAIL]
	                    ,[PRIV_ROLE_GUID]
	                    ,[PRIV_ROLE_GROUP_GUID] 
                    FROM 
	                    [USER] 
                    WHERE 
	                    USER_EMAIL = '" + username + @"' 
	                    AND USER_PWD = '" + password + @"' 
                        AND USER_IS_ACTIVE = 'Y'";
                get = db.GetDataTable(query);

                HttpContext.Current.Session["IS_VENDOR"] = "N";
                HttpContext.Current.Session["USER_GUID"] = get.Rows[0].ItemArray[0].ToString();
                HttpContext.Current.Session["USER_ID"] = get.Rows[0].ItemArray[1].ToString();
                HttpContext.Current.Session["USER_NAME_DISPLAY"] = get.Rows[0].ItemArray[2].ToString();
                HttpContext.Current.Session["USER_EMAIL"] = get.Rows[0].ItemArray[3].ToString();
                HttpContext.Current.Session["PRIV_ROLE_GUID"] = get.Rows[0].ItemArray[3].ToString();
                HttpContext.Current.Session["PRIV_ROLE_GROUP_GUID"] = get.Rows[0].ItemArray[3].ToString();
                ret = true;
            }
            else if (type == "VENDOR")
            {
                query =
                    @"SELECT
                        [VENDOR_GUID]
                        ,[VENDOR_ID]
                        ,[VENDOR_NAME]
                        ,[VENDOR_CODE]
                    FROM
                        [VENDOR]
                    WHERE
                        VENDOR_IS_ACTIVE = 'Y'";
                get = db.GetDataTable(query);

                HttpContext.Current.Session["IS_VENDOR"] = "Y";
                HttpContext.Current.Session["VENDOR_GUID"] = get.Rows[0].ItemArray[0].ToString();
                HttpContext.Current.Session["VENDOR_ID"] = get.Rows[0].ItemArray[1].ToString();
                HttpContext.Current.Session["VENDOR_NAME"] = get.Rows[0].ItemArray[2].ToString();
                HttpContext.Current.Session["VENDOR_CODE"] = get.Rows[0].ItemArray[3].ToString();
                ret = true;
            }
            return ret;
        }

        public static bool IsSetSession()
        {
            bool ret = false;
            if (HttpContext.Current.Session["IS_VENDOR"] != null)
            {
                ret = true;
            }
            else
            {
                ret = false;
            }
            return ret;
        }

        public static void UssetSession()
        {
            if (HttpContext.Current.Session["IS_VENDOR"] != null)
            {
                HttpContext.Current.Session.Abandon();
            }
        }
    }
}