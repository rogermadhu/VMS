namespace VMS.ClassUtility
{
    public class AnnualReportFileAttr
    {
        string fileName;
        string ext;
        string newFileName;
        string path;
        string absPath;

        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }
        public string Ext
        {
            get
            {
                return ext;
            }

            set
            {
                ext = value;
            }
        }
        public string NewFileName
        {
            get
            {
                return newFileName;
            }

            set
            {
                newFileName = value;
            }
        }
        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }
        public string AbsPath
        {
            get
            {
                return absPath;
            }

            set
            {
                absPath = value;
            }
        }
    }
}