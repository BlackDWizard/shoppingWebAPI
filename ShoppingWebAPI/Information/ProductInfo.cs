using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ShoppingWebAPI.Information
{
    public partial class ProductInfo
    {
        /// <summary>
        /// Constructors
        /// </summary>		
        public ProductInfo(string connString)
        {
            this.Init();
            _ConnectionString = connString;
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
                    sbCmd.Append("	   AND ProductSN = @ProductSN	  ");

                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                    #region Add In Parameter

                    command.Parameters.Add("@ProductSN", SqlDbType.Char).Value = this._ProductSN;

                    #endregion

                    IDataReader dataReader = command.ExecuteReader();
                    DataTable dtTemp = new DataTable();
                    dtTemp.Load(dataReader);

                    if (dtTemp.Rows.Count == 0)
                    {
                        Result = false;
                    }
                    else
                    {
                        Result = true;

                        DataRow dr = dtTemp.Rows[0];
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
                    StackTrace stack = new StackTrace();
                    StackFrame frame = stack.GetFrame(0);
                    string className = frame.GetMethod().DeclaringType.FullName;
                    string methodName = frame.GetMethod().Name;

                    ExceptionLogInfo exceptionLog = new ExceptionLogInfo(_ConnectionString);
                    exceptionLog.ExceptionClass = className;
                    exceptionLog.ExceptionMethod = methodName;
                    exceptionLog.ExceptionReason = ex.ToString();
                    exceptionLog.ExceptionDate = DateTime.Now;

                    exceptionLog.Insert();
                }
                connection.Close();
            }
            return Result;
        }


        /// <summary>
        /// Insert
        /// </summary>
        public void Insert()
        {
            using (SqlCommand command = new SqlCommand())
            {
                SqlConnection connection = new SqlConnection(_ConnectionString);
                connection.Open();

                try
                {
                    StringBuilder sbCmd = new StringBuilder();

                    sbCmd.Append("	INSERT INTO [Product]		");
                    sbCmd.Append("		(				");
                    sbCmd.Append("		ProductSN		");
                    sbCmd.Append("		,ProductName		");
                    sbCmd.Append("		,ProductPrice		");
                    sbCmd.Append("		,ProductDescription		");
                    sbCmd.Append("		,Creator		");
                    sbCmd.Append("		,CreatedDate		");
                    sbCmd.Append("		,Modifier		");
                    sbCmd.Append("		,ModifiedDate		");
                    sbCmd.Append("		)				");
                    sbCmd.Append("	VALUES		");
                    sbCmd.Append("		(				");
                    sbCmd.Append("		@ProductSN		");
                    sbCmd.Append("		,@ProductName		");
                    sbCmd.Append("		,@ProductPrice		");
                    sbCmd.Append("		,@ProductDescription		");
                    sbCmd.Append("		,@Creator		");
                    sbCmd.Append("		,@CreatedDate		");
                    sbCmd.Append("		,@Modifier		");
                    sbCmd.Append("		,@ModifiedDate		");
                    sbCmd.Append("		)				");

                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                    #region Add In Parameter

                    command.Parameters.Add("@ProductSN", SqlDbType.Char).Value = this._ProductSN == null ? DBNull.Value : this._ProductSN;
                    command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = this._ProductName == null ? DBNull.Value : this._ProductName;
                    command.Parameters.Add("@ProductPrice", SqlDbType.Int).Value = this._ProductPrice == null ? DBNull.Value : this._ProductPrice;
                    command.Parameters.Add("@ProductDescription", SqlDbType.NVarChar).Value = this._ProductDescription == null ? DBNull.Value : this._ProductDescription;
                    command.Parameters.Add("@Creator", SqlDbType.Char).Value = this._Creator == null ? DBNull.Value : this._Creator;
                    command.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = this._CreatedDate == null ? DBNull.Value : this._CreatedDate;
                    command.Parameters.Add("@Modifier", SqlDbType.Char).Value = this._Modifier == null ? DBNull.Value : this._Modifier;
                    command.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = this._ModifiedDate == null ? DBNull.Value : this._ModifiedDate;

                    #endregion

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    StackTrace stack = new StackTrace();
                    StackFrame frame = stack.GetFrame(0);
                    string className = frame.GetMethod().DeclaringType.FullName;
                    string methodName = frame.GetMethod().Name;

                    ExceptionLogInfo exceptionLog = new ExceptionLogInfo(_ConnectionString);
                    exceptionLog.ExceptionClass = className;
                    exceptionLog.ExceptionMethod = methodName;
                    exceptionLog.ExceptionReason = ex.ToString();
                    exceptionLog.ExceptionDate = DateTime.Now;

                    exceptionLog.Insert();
                }
                connection.Close();
            }
        }


        /// <summary>
        /// Update
        /// </summary>
        public void Update()
        {
            using (SqlCommand command = new SqlCommand())
            {
                SqlConnection connection = new SqlConnection(_ConnectionString);
                connection.Open();

                try
                {
                    StringBuilder sbCmd = new StringBuilder();

                    sbCmd.Append("	UPDATE [Product] SET 		");
                    sbCmd.Append("		ProductName = @ProductName 		");
                    sbCmd.Append("		,ProductPrice = @ProductPrice 		");
                    sbCmd.Append("		,ProductDescription = @ProductDescription 		");
                    sbCmd.Append("		,Creator = @Creator 		");
                    sbCmd.Append("		,CreatedDate = @CreatedDate 		");
                    sbCmd.Append("		,Modifier = @Modifier 		");
                    sbCmd.Append("		,ModifiedDate = @ModifiedDate 		");
                    sbCmd.Append("	WHERE (1=1) ");
                    sbCmd.Append("		AND ProductSN = @ProductSN 		");

                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                    #region Add In Parameter

                    command.Parameters.Add("@ProductSN", SqlDbType.Char).Value = this._ProductSN;
                    command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = this._ProductName;
                    command.Parameters.Add("@ProductPrice", SqlDbType.Int).Value = this._ProductPrice;
                    command.Parameters.Add("@ProductDescription", SqlDbType.NVarChar).Value = this._ProductDescription;
                    command.Parameters.Add("@Creator", SqlDbType.Char).Value = this._Creator;
                    command.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = this._CreatedDate;
                    command.Parameters.Add("@Modifier", SqlDbType.Char).Value = this._Modifier;
                    command.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = this._ModifiedDate;

                    #endregion

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    StackTrace stack = new StackTrace();
                    StackFrame frame = stack.GetFrame(0);
                    string className = frame.GetMethod().DeclaringType.FullName;
                    string methodName = frame.GetMethod().Name;

                    ExceptionLogInfo exceptionLog = new ExceptionLogInfo(_ConnectionString);
                    exceptionLog.ExceptionClass = className;
                    exceptionLog.ExceptionMethod = methodName;
                    exceptionLog.ExceptionReason = ex.ToString();
                    exceptionLog.ExceptionDate = DateTime.Now;

                    exceptionLog.Insert();
                }
                connection.Close();
            }
        }


        /// <summary>
        /// Delete
        /// </summary>
        public void Delete(string iProductSN)
        {
            this._ProductSN = iProductSN;

            using (SqlCommand command = new SqlCommand())
            {
                SqlConnection connection = new SqlConnection(_ConnectionString);
                connection.Open();

                try
                {
                    StringBuilder sbCmd = new StringBuilder();

                    sbCmd.Append("	DELETE [Product]		");
                    sbCmd.Append("	WHERE (1=1) 		");
                    sbCmd.Append("		AND ProductSN = @ProductSN 		");

                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                    #region Add In Parameter

                    command.Parameters.Add("@ProductSN", SqlDbType.Char).Value = this._ProductSN;

                    #endregion

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    StackTrace stack = new StackTrace();
                    StackFrame frame = stack.GetFrame(0);
                    string className = frame.GetMethod().DeclaringType.FullName;
                    string methodName = frame.GetMethod().Name;

                    ExceptionLogInfo exceptionLog = new ExceptionLogInfo(_ConnectionString);
                    exceptionLog.ExceptionClass = className;
                    exceptionLog.ExceptionMethod = methodName;
                    exceptionLog.ExceptionReason = ex.ToString();
                    exceptionLog.ExceptionDate = DateTime.Now;

                    exceptionLog.Insert();
                }
                connection.Close();
            }
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

