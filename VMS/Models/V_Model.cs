using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VMS.App_Code;

namespace VMS.Models
{
    
    public class Vendor_Model
    {
        DatabaseMSSQL db = new DatabaseMSSQL();

        private string vendor_guid;
        private Int64 vendor_id;
        private string vendor_name;
        private string username;
        private string password;
        private string website;
        private string trade_license;
        private string tin_no;
        private Int64 tel_no;
        private Int64 cell_no;
        private Int64 fax_no;
        private string email;
        private DateTime date_of_establishment;
        private int total_no_of_employee;
        private string vendor_code;
        private char is_agreed;
        private char is_approved;
        private char is_active;
        private DateTime create_date;
        private DateTime modify_date;
        private string modify_by;
        #region "SetterGetter"
        protected string Vendor_guid
        {
            get
            {
                return vendor_guid;
            }

            set
            {
                vendor_guid = value;
            }
        }
        protected long Vendor_id
        {
            get
            {
                return vendor_id;
            }

            set
            {
                vendor_id = value;
            }
        }
        protected string Vendor_name
        {
            get
            {
                return vendor_name;
            }

            set
            {
                vendor_name = value;
            }
        }
        protected string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }
        protected string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }
        protected string Website
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
        protected string Trade_license
        {
            get
            {
                return trade_license;
            }

            set
            {
                trade_license = value;
            }
        }
        protected string Tin_no
        {
            get
            {
                return tin_no;
            }

            set
            {
                tin_no = value;
            }
        }
        protected long Tel_no
        {
            get
            {
                return tel_no;
            }

            set
            {
                tel_no = value;
            }
        }
        protected long Cell_no
        {
            get
            {
                return cell_no;
            }

            set
            {
                cell_no = value;
            }
        }
        protected long Fax_no
        {
            get
            {
                return fax_no;
            }

            set
            {
                fax_no = value;
            }
        }
        protected string Email
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
        protected DateTime Date_of_establishment
        {
            get
            {
                return date_of_establishment;
            }

            set
            {
                date_of_establishment = value;
            }
        }
        protected int Total_no_of_employee
        {
            get
            {
                return total_no_of_employee;
            }

            set
            {
                total_no_of_employee = value;
            }
        }
        protected string Vendor_code
        {
            get
            {
                return vendor_code;
            }

            set
            {
                vendor_code = value;
            }
        }
        protected char Is_agreed
        {
            get
            {
                return is_agreed;
            }

            set
            {
                is_agreed = value;
            }
        }
        protected char Is_approved
        {
            get
            {
                return is_approved;
            }

            set
            {
                is_approved = value;
            }
        }
        protected char Is_active
        {
            get
            {
                return is_active;
            }

            set
            {
                is_active = value;
            }
        }
        protected DateTime Create_date
        {
            get
            {
                return create_date;
            }

            set
            {
                create_date = value;
            }
        }
        protected DateTime Modify_date
        {
            get
            {
                return modify_date;
            }

            set
            {
                modify_date = value;
            }
        }
        protected string Modify_by
        {
            get
            {
                return modify_by;
            }

            set
            {
                modify_by = value;
            }
        }
        #endregion "SetterGetter"

        public string InsertVendor(Vendor_Model vendor)
        {
            //string vendor_name, string username, string password, string website, string trade_license, string tin_no, Int64 tel_no, Int64 cell_no, Int64 fax_no, string email, DateTime date_of_establishment, int total_no_of_employee, string vendor_code, char is_agreed, char is_approved, char is_active, DateTime create_date, DateTime modify_date, string modify_by
            string ret = string.Empty;
            try
            {
                string query = @"";
                //string query = @"INSERT INTO VENDOR 
                //    (VENDOR_NAME ,WEBSITE ,TRADE_LICENSE ,TIN_NO ,TEL_NO ,CELL_NO ,FAX_NO ,EMAIL ,DATE_OF_ESTABLISHMENT ,TOTAL_NO_OF_EMPLOYEE ,VENDOR_CODE ,IS_AGREED ,IS_APPROVED ,IS_ACTIVE)
                //    VALUES (
                //        '"+vendor.vendor_name+"','"+ vendor.website +"','"+vendor.trade_license+"','"+vendor.tin_no+"','"+tel_no+"','"+cell_no+ "','"+fax_no+ "','"+email+ "','"+date_of_establishment+ "','"+total_no_of_employee+ "','"++"'
                //        )";
                db.GetDataTable(query);
            }
            catch(Exception ex)
            {
                ret = null;
            }
            return ret;
        }
    }


}