using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace Accounting
{
    /// <summary>
    /// ����� �������� ��������
    /// </summary>
    public class Options : Object
    {
        /// <summary>
        /// ����������� ������ ��������.
        /// </summary>
        protected Options()
        {
            if (!Directory.Exists(ApplicationDataPath))
                Directory.CreateDirectory(ApplicationDataPath);
            if (!Directory.Exists(SavedApiFiles))
                Directory.CreateDirectory(SavedApiFiles);
            mCharacterId = 0;
            mCharacterName = "***";
            mUserId = 0;
            mApiKey = "apiKey";
            mDefaultDaysInterval = 1;
            mContinueParsing = false;
            mSaveDownloadedDataOnDisk = true;
            mLanguage = "default";
            mUserIdApiKeyPairs = new string[] { "0,00,0,charName" };
            mLoadImagesFromWeb = true;
        }
        #region Load/Save
        /// <summary>
        /// ������� ���������� �������� �� ���������� xml-����� ������� ��������������.
        /// </summary>
        /// <param name="fileName">��� ����� ��������</param>
        /// <returns>����������� ������ �������</returns>
        public static Options Load(string fileName)
        {
            //bool res = false;
            Options opts = null;
            XmlSerializer ser = new XmlSerializer(typeof(Options));
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                try
                {
                    opts = (Options)ser.Deserialize(fs);
                    //res = true;
                }
                catch
                {
                    opts = new Options();
                }
                finally
                {
                    fs.Close();
                }
            }
            catch
            {
                opts = new Options();
            }
            return opts;
        }
        /// <summary>
        /// ������� ���������� �������� �� xml-����� ������� ��������������.
        /// </summary>
        /// <returns>����������� ������ �������</returns>
        public static Options Load()
        {
            return Load(OptionsFileName);
        }
        /// <summary>
        /// ������� ���������� �������� � ��������� xml-���� ������� ������������.
        /// </summary>
        /// <param name="fileName">��� ����� ��� ����������</param>
        public void Save(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Options));
            TextWriter fs = new StreamWriter(fileName);
            ser.Serialize(fs, this);
            fs.Close();
        }
        /// <summary>
        /// ������� ���������� �������� � xml-���� ������� ������������.
        /// </summary>
        public void Save()
        {
            Save(OptionsFileName);
        }
        #endregion
        #region Paths
        /// <summary>
        /// ���� � ����� �������� ����������.
        /// </summary>
        public static string OptionsFileName
        {
            get
            {
                string AssemblyName = System.IO.Path.GetFileName(
                    System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.Name);
                return Path.Combine(ApplicationDataPath, Path.ChangeExtension(AssemblyName, ".xml"));
            }
        }
        /// <summary>
        /// ���� � �������� ������ ���������.
        /// </summary>
        public static string ApplicationDataPath
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                path = Path.Combine(path, Application.CompanyName);
                path = Path.Combine(path, Application.ProductName);
                path = Path.Combine(path, "1.0.0.0");
                return path;
            }
        }
        public static string ExePath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            }
        }
        public static string SavedApiFiles
        {
            get
            {
                return Path.Combine(ApplicationDataPath, "download");
            }
        }
        #endregion
        #region Data
        private int mCharacterId;
        private string mCharacterName;
        private int mUserId;
        private string mApiKey;
        private int mDefaultDaysInterval;
        private bool mContinueParsing;
        private bool mSaveDownloadedDataOnDisk;
        private string mLanguage;
        private string[] mUserIdApiKeyPairs;
        private bool mLoadImagesFromWeb;

        public int CharacterId
        {
            get { return mCharacterId; }
            set { mCharacterId = value; }
        }
        public string CharacterName
        {
            get { return mCharacterName; }
            set { mCharacterName = value; }
        }
        public int UserId
        {
            get { return mUserId; }
            set { mUserId = value; }
        }
        public string ApiKey
        {
            get { return mApiKey; }
            set { mApiKey = value; }
        }
        public int DefaultDaysInterval
        {
            get { return mDefaultDaysInterval; }
            set { mDefaultDaysInterval = value; }
        }
        public bool ContinueParsing
        {
            get { return mContinueParsing; }
            set { mContinueParsing = value; }
        }
        public bool SaveDownloadedDataOnDisk
        {
            get { return mSaveDownloadedDataOnDisk; }
            set { mSaveDownloadedDataOnDisk = value; }
        }
        public string Language
        {
            get { return mLanguage; }
            set { mLanguage = value; }
        }
        public string[] UserIdApiKeyPairs
        {
            get { return mUserIdApiKeyPairs; }
            set { mUserIdApiKeyPairs = value; }
        }
        public bool LoadImagesFromWeb
        {
            get { return mLoadImagesFromWeb; }
            set { mLoadImagesFromWeb = value; }
        }
        #endregion
    }
}
