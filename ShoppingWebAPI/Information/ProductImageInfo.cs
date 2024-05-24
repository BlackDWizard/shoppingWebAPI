using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ShoppingWebAPI.Information
{
    public partial class ProductImageInfo
    {
        /// <summary>
        /// Constructors
        /// </summary>		
        public ProductImageInfo(string connString)
        {
            this.Init();
            _ConnectionString = connString;
        }


        #region Init
        private void Init()
        {
            this._ProductSN = "";                                             //
            this._ProductImage = null;                                        //
            this._Creator = "";                                               //
            this._CreatedDate = null;                                         //
            this._Modifier = "";                                              //
            this._ModifiedDate = null;                                        //
        }
        #endregion


        #region Private Properties
        private string _ConnectionString;
        private string _ProductSN;
        private byte[] _ProductImage;
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
        public byte[] ProductImage
        {
            get { return _ProductImage; }
            set { _ProductImage = value; }
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

                    sbCmd.Append("   SELECT * FROM [ProductImage] WITH (Nolock) ");
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
                        this._ProductImage = (byte[])dr["ProductImage"];
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

                    sbCmd.Append("	INSERT INTO [ProductImage]		");
                    sbCmd.Append("		(				");
                    sbCmd.Append("		ProductSN		");
                    sbCmd.Append("		,ProductImage		");
                    sbCmd.Append("		,Creator		");
                    sbCmd.Append("		,CreatedDate		");
                    sbCmd.Append("		,Modifier		");
                    sbCmd.Append("		,ModifiedDate		");
                    sbCmd.Append("		)				");
                    sbCmd.Append("	VALUES		");
                    sbCmd.Append("		(				");
                    sbCmd.Append("		@ProductSN		");
                    sbCmd.Append("		,@ProductImage		");
                    sbCmd.Append("		,@Creator		");
                    sbCmd.Append("		,@CreatedDate		");
                    sbCmd.Append("		,@Modifier		");
                    sbCmd.Append("		,@ModifiedDate		");
                    sbCmd.Append("		)				");

                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                    #region Add In Parameter

                    command.Parameters.Add("@ProductSN", SqlDbType.Char).Value = this._ProductSN == null ? DBNull.Value : this._ProductSN;
                    command.Parameters.Add("@ProductImage", SqlDbType.VarBinary).Value = this._ProductImage == null ? DBNull.Value : this._ProductImage;
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

                    sbCmd.Append("	UPDATE [ProductImage] SET 		");
                    sbCmd.Append("		ProductImage = @ProductImage 		");
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
                    command.Parameters.Add("@ProductImage", SqlDbType.VarBinary).Value = this._ProductImage;
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

                    sbCmd.Append("	DELETE [ProductImage]		");
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
Vista.Information.ProductImageInfo Info = new Vista.Information.ProductImageInfo();
Info.ProductSN = "";                                   //
Info.ProductImage = null;                              //
Info.Creator = "";                                     //
Info.CreatedDate = null;                               //
Info.Modifier = "";                                    //
Info.ModifiedDate = null;                              //
*/
#endregion

