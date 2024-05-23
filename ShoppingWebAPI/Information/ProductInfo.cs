using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Xml;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Buffers.Text;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;

namespace ShoppingWebAPI.Information
{
    public partial class ProductInfo
    {

        /// <summary>
        /// Constructors
        /// </summary>		
        public ProductInfo()
        {
            this.Init();
            this.GetConnection();
        }


        #region Init
        private void Init()
        {
            this._ProductSN = "";                                             //
            this._ProductName = "";                                           //
            this._ProductPrice = 0;                                           //
            this._ProductDescription = "";                                    //
            this._Creator = "";                                               //
            this._CreatedDate = null;                                         //
            this._Modifier = "";                                              //
            this._ModifiedDate = null;                                        //
        }
        #endregion

        #region GetConnection
        private void GetConnection()
        {
            string filePath = "ConnectionConfig.json";
            string jsonContent = File.ReadAllText(filePath);
            JObject jsonObject = JObject.Parse(jsonContent);
            _ConnectionString = jsonObject["ConnectionString"].ToString();
        }
        #endregion


        #region Private Properties
        private string _ConnectionString;
        private string _ProductSN;
        private string _ProductName;
        private int _ProductPrice;
        private string _ProductDescription;
        private string _Creator;
        private DateTime? _CreatedDate;
        private string _Modifier;
        private DateTime? _ModifiedDate;
        #endregion


        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string ProductSN
        {
            get { return _ProductSN; }
            set { _ProductSN = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ProductPrice
        {
            get { return _ProductPrice; }
            set { _ProductPrice = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductDescription
        {
            get { return _ProductDescription; }
            set { _ProductDescription = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Creator
        {
            get { return _Creator; }
            set { _Creator = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Modifier
        {
            get { return _Modifier; }
            set { _Modifier = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }
        #endregion


        #region Methods

        /// <summary>
        /// 依據PK載入一筆資料
        /// </summary>
        /// <returns>true代表成功載入，false代表找不到任何資料</returns>
        public bool Load(string iProductSN)
        {
            bool Result = false;

            this._ProductSN = iProductSN;

            using (SqlCommand command = new SqlCommand())
            {
                SqlConnection connection = new SqlConnection(_ConnectionString);
                connection.Open();

                try
                {
                    StringBuilder sbCmd = new StringBuilder();

                    sbCmd.Append("   SELECT * FROM [Product] WITH (Nolock) ");
                    sbCmd.Append("   WHERE(1 = 1) ");
                    sbCmd.Append("       AND ProductSN = @ProductSN      ");
                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                    #region Add In Parameter

                    SqlParameter param = command.Parameters.Add("@ProductSN", SqlDbType.Char);
                    param.Value = _ProductSN;

                    #endregion
                    IDataReader dataReader = command.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dataReader);

                    if (dt.Rows.Count == 0)
                    {
                        Result = false;
                    }
                    else
                    {
                        Result = true;

                        DataRow dr = dt.Rows[0];
                        this._ProductSN = Convert.ToString(dr["ProductSN"]);
                        this._ProductName = Convert.ToString(dr["ProductName"]);
                        this._ProductPrice = Convert.ToInt32(dr["ProductPrice"]);
                        this._ProductDescription = Convert.ToString(dr["ProductDescription"]);
                        this._Creator = Convert.ToString(dr["Creator"]);
                        this._CreatedDate = dr["CreatedDate"] == DBNull.Value ? new Nullable<DateTime>() : Convert.ToDateTime(dr["CreatedDate"]);
                        this._Modifier = Convert.ToString(dr["Modifier"]);
                        this._ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? new Nullable<DateTime>() : Convert.ToDateTime(dr["ModifiedDate"]);
                    }
                }
                catch (Exception ex)
                {
                    StringBuilder sbCmd = new StringBuilder();

                    sbCmd.Append("   Insert into * FROM [Product] WITH (Nolock) ");
                    sbCmd.Append("   WHERE(1 = 1) ");
                    sbCmd.Append("       AND ProductSN = @ProductSN      ");
                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                }
                connection.Close();
            }

            return Result;
        }

        #endregion
    }
}


#region Use Sample
/*
Vista.Information.ProductInfo Info = new Vista.Information.ProductInfo();
Info.ProductSN = "";                                   //
Info.ProductName = "";                                 //
Info.ProductPrice = 0;                                 //
Info.ProductDescription = "";                          //
Info.Creator = "";                                     //
Info.CreatedDate = null;                               //
Info.Modifier = "";                                    //
Info.ModifiedDate = null;                              //
*/
#endregion

