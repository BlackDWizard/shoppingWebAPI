using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;

namespace
{
    public partial class ExceptionLogInfo
    {

        /// <summary>
        /// Constructors
        /// </summary>		
        public ExceptionLogInfo(string connString)
        {
            this.Init();
            _ConnectionString = connString;
        }


        #region Init
        private void Init()
        {
            this._ExceptionSN = 0;                                            //
            this._ExceptionClass = "";                                        //
            this._ExceptionMethod = "";                                       //
            this._ExceptionReason = "";                                       //
            this._ExceptionDate = null;                                       //
        }
        #endregion


        #region Private Properties
        private string _ConnectionString;
        private int _ExceptionSN;
        private string _ExceptionClass;
        private string _ExceptionMethod;
        private string _ExceptionReason;
        private DateTime? _ExceptionDate;
        #endregion


        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public int ExceptionSN
        {
            get { return _ExceptionSN; }
            set { _ExceptionSN = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExceptionClass
        {
            get { return _ExceptionClass; }
            set { _ExceptionClass = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExceptionMethod
        {
            get { return _ExceptionMethod; }
            set { _ExceptionMethod = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExceptionReason
        {
            get { return _ExceptionReason; }
            set { _ExceptionReason = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ExceptionDate
        {
            get { return _ExceptionDate; }
            set { _ExceptionDate = value; }
        }
        #endregion


        #region Methods

        /// <summary>
        /// 依據PK載入一筆資料
        /// </summary>
        /// <returns>true代表成功載入，false代表找不到任何資料</returns>
        public bool Load(int iExceptionSN)
        {
            bool Result = false;

            this._ExceptionSN = iExceptionSN;

            using (SqlCommand command = new SqlCommand())
            {
                SqlConnection connection = new SqlConnection(_ConnectionString);
                connection.Open();

                try
                {
                    StringBuilder sbCmd = new StringBuilder();

                    sbCmd.Append("   SELECT * FROM [ExceptionLog] WITH (Nolock) ");
                    sbCmd.Append("   WHERE(1 = 1) ");
                    sbCmd.Append("       AND ExceptionSN = @ExceptionSN      ");

                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                    #region Add In Parameter

                    command.Parameters.Add("@ExceptionSN", SqlDbType.Int).Value = this._ExceptionSN;

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
                        this._ExceptionSN = Convert.ToInt32(dr["ExceptionSN"]);
                        this._ExceptionClass = Convert.ToString(dr["ExceptionClass"]);
                        this._ExceptionMethod = Convert.ToString(dr["ExceptionMethod"]);
                        this._ExceptionReason = Convert.ToString(dr["ExceptionReason"]);
                        this._ExceptionDate = dr["ExceptionDate"] == DBNull.Value ? new Nullable<DateTime>() : Convert.ToDateTime(dr["ExceptionDate"]);
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

                    sbCmd.Append("	INSERT INTO [ExceptionLog]		");
                    sbCmd.Append("		(				");
                    sbCmd.Append("		ExceptionSN		");
                    sbCmd.Append("		,ExceptionClass		");
                    sbCmd.Append("		,ExceptionMethod		");
                    sbCmd.Append("		,ExceptionReason		");
                    sbCmd.Append("		,ExceptionDate		");
                    sbCmd.Append("		)				");
                    sbCmd.Append("	VALUES		");
                    sbCmd.Append("		(				");
                    sbCmd.Append("		@ExceptionSN		");
                    sbCmd.Append("		,@ExceptionClass		");
                    sbCmd.Append("		,@ExceptionMethod		");
                    sbCmd.Append("		,@ExceptionReason		");
                    sbCmd.Append("		,@ExceptionDate		");
                    sbCmd.Append("		)				");


                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                    #region Add In Parameter
                    command.Parameters.Add("@ExceptionSN", SqlDbType.Int).Value = this._ExceptionSN;
                    command.Parameters.Add("@ExceptionClass", SqlDbType.Varchar).Value = this._ExceptionClass;
                    command.Parameters.Add("@ExceptionMethod", SqlDbType.Varchar).Value = this._ExceptionMethod;
                    command.Parameters.Add("@ExceptionReason", SqlDbType.Nvarchar).Value = this._ExceptionReason;
                    command.Parameters.Add("@ExceptionDate", SqlDbType.Datetime).Value = this._ExceptionDate;

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

                    sbCmd.Append("	UPDATE [ExceptionLog] SET 		");
                    sbCmd.Append("		ExceptionClass = @ExceptionClass 		");
                    sbCmd.Append("		,ExceptionMethod = @ExceptionMethod 		");
                    sbCmd.Append("		,ExceptionReason = @ExceptionReason 		");
                    sbCmd.Append("		,ExceptionDate = @ExceptionDate 		");
                    sbCmd.Append("	WHERE (1=1) ");
                    sbCmd.Append("		AND ExceptionSN = @ExceptionSN 		");

                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                    #region Add In Parameter
                    command.Parameters.Add("@ExceptionSN", SqlDbType.Int).Value = this._ExceptionSN;
                    command.Parameters.Add("@ExceptionClass", SqlDbType.Varchar).Value = this._ExceptionClass;
                    command.Parameters.Add("@ExceptionMethod", SqlDbType.Varchar).Value = this._ExceptionMethod;
                    command.Parameters.Add("@ExceptionReason", SqlDbType.Nvarchar).Value = this._ExceptionReason;
                    command.Parameters.Add("@ExceptionDate", SqlDbType.Datetime).Value = this._ExceptionDate;
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
        public void Delete(int iExceptionSN)
        {
            this._ExceptionSN = iExceptionSN;

            using (SqlCommand command = new SqlCommand())
            {
                SqlConnection connection = new SqlConnection(_ConnectionString);
                connection.Open();

                try
                {
                    StringBuilder sbCmd = new StringBuilder();

                    sbCmd.Append("	DELETE [ExceptionLog]		");
                    sbCmd.Append("	WHERE (1=1) 		");
                    sbCmd.Append("		AND ExceptionSN = @ExceptionSN 		");

                    command.Connection = connection;
                    command.CommandText = sbCmd.ToString();

                    #region Add In Parameter

                    command.Parameters.Add("@ExceptionSN", SqlDbType.Int).Value = this._ExceptionSN;

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
Vista.Information.ExceptionLogInfo Info = new Vista.Information.ExceptionLogInfo();
Info.ExceptionSN = 0;                                  //
Info.ExceptionClass = "";                              //
Info.ExceptionMethod = "";                             //
Info.ExceptionReason = "";                             //
Info.ExceptionDate = null;                             //
*/
#endregion

