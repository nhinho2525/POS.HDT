//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace POS.HDT.Common.Data.Base
//{
//    public abstract class DBAccess : IDBAccess
//    {
//        /// <summary>
//        /// Add a logger
//        /// </summary>
//        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

//        public DBAccess()
//        {
//            if (!log4net.LogManager.GetRepository().Configured)
//            {
//                log4net.Config.XmlConfigurator.Configure();
//            }
//        }

//        public abstract string Name { get; }

//        /// <summary>
//        /// Name of the AppSetting key to the connection string
//        /// </summary>
//        private string ConnStrKey = "ConnStrName";

//        /// <summary>
//        /// Field to hold the connection String Name
//        /// </summary>
//        private string connectStringName;

//        /// <summary>
//        /// Name of the connection string
//        /// </summary>
//        protected string ConnectStringName
//        {
//            get
//            {
//                if (string.IsNullOrEmpty(connectStringName))
//                {
//                    connectStringName = ConfigurationManager.AppSettings[ConnStrKey];
//                }
//                return connectStringName;
//            }
//        }

//        private string connectionString;

//        protected string ConnectionString
//        {
//            get
//            {
//                if (string.IsNullOrEmpty(connectionString))
//                {
//                    connectionString = ConfigurationManager.ConnectionStrings[ConnectStringName].ConnectionString;
//                }
//                return connectionString;
//            }
//            set { connectionString = value; }
//        }

//        protected delegate void TrsVoidDelegate(ITrsDbCommand cmd);


//        protected void TrsVoidConnectCommand(string name, CommandType commandType, string commandText, TrsVoidDelegate trsDelegate)
//        {
//            using (LogTimerFactory.DatabaseRunTimer(Log, name))
//            {
//                try
//                {
//                    using (ITrsDbConnection connection = new TrsDbConnection(ConnectionString))
//                    {
//                        try
//                        {
//                            connection.Open();
//                            using (ITrsDbCommand cmd = connection.CreateTrsCommand(commandType, commandText))
//                            {
//                                trsDelegate(cmd);
//                            }
//                        }
//                        catch (Exception ex1)
//                        {
//                            if (Log.IsErrorEnabled) Log.Error(name + " threw exception on open: ", ex1);
//                            throw;
//                        }
//                        finally
//                        {
//                            connection.Close();
//                        }
//                    }
//                }
//                catch (Exception ex2)
//                {
//                    if (Log.IsErrorEnabled) Log.Error(name + " threw exception on OleDbConnection: ", ex2);
//                    throw;
//                }
//            }
//        }




//        protected delegate T TrsGenericDelegate<T>(ITrsDbCommand cmd);

//        protected T TrsGenericConnectCommand<T>(string name, CommandType commandType, string commandText, TrsGenericDelegate<T> trsDelegate)
//        {
//            using (LogTimerFactory.DatabaseRunTimer(Log, name))
//            {
//                try
//                {
//                    using (ITrsDbConnection connection = new TrsDbConnection(ConnectionString))
//                    {
//                        try
//                        {
//                            connection.Open();
//                            using (ITrsDbCommand cmd = connection.CreateTrsCommand(commandType, commandText))
//                            {
//                                return trsDelegate(cmd);
//                            }
//                        }
//                        catch (Exception ex1)
//                        {
//                            if (Log.IsErrorEnabled) Log.Error(name + " threw exception on open: ", ex1);
//                            throw;
//                        }
//                        finally
//                        {
//                            connection.Close();
//                        }
//                    }
//                }
//                catch (Exception ex2)
//                {
//                    if (Log.IsErrorEnabled) Log.Error(name + " threw exception on OleDbConnection: ", ex2);
//                    throw;
//                }
//            }
//        }

//        protected delegate T TrsGenericDelegate1Param<T, U>(ITrsDbCommand cmd, U param0);

//        protected T TrsGenericConnectCommand1Param<T, U>(string name, CommandType commandType, string commandText, TrsGenericDelegate1Param<T, U> trsDelegate, U param0)
//        {
//            using (LogTimerFactory.DatabaseRunTimer(Log, name))
//            {
//                try
//                {
//                    using (ITrsDbConnection connection = new TrsDbConnection(ConnectionString))
//                    {
//                        try
//                        {
//                            connection.Open();
//                            using (ITrsDbCommand cmd = connection.CreateTrsCommand(commandType, commandText))
//                            {
//                                return trsDelegate(cmd, param0);
//                            }
//                        }
//                        catch (Exception ex1)
//                        {
//                            if (Log.IsErrorEnabled) Log.Error(name + " threw exception on open: ", ex1);
//                            throw;
//                        }
//                        finally
//                        {
//                            connection.Close();
//                        }
//                    }
//                }
//                catch (Exception ex2)
//                {
//                    if (Log.IsErrorEnabled) Log.Error(name + " threw exception on OleDbConnection: ", ex2);
//                    throw;
//                }
//            }
//        }

//        protected DateTime GetCurrentDbTimeDelegate(ITrsDbCommand cmd)
//        {
//            IDbDataParameter param0 = cmd.Parameters.Add("@curDate", OleDbType.Date, 0, "curDate");
//            param0.Direction = ParameterDirection.Output;

//            try
//            {
//                cmd.ExecuteNonQuery();
//                return (DateTime)param0.Value;
//            }
//            catch (Exception ex)
//            {
//                if (Log.IsErrorEnabled) Log.Error("GetCurrentDbTimeDelegate threw exception: ", ex);
//                return DateTime.Now;
//            }
//        }

//        public virtual DateTime GetCurrentDBTime()
//        {
//            if (Log.IsDebugEnabled) Log.Debug("Inside GetCurrentDBTime");

//            try
//            {
//                return TrsGenericConnectCommand<DateTime>("GetCurrentDBTime", CommandType.StoredProcedure, "spGetCurrentTime", GetCurrentDbTimeDelegate);
//            }
//            catch (Exception ex)
//            {
//                if (Log.IsErrorEnabled) Log.Error("GetCurrentDbTime threw exception: ", ex);
//                CustomErrorDetail errorDetail = new CustomErrorDetail("GetCurrentDbTime threw exception", ex.Message);
//                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotFound);
//            }
//        }

//        public virtual long AddNewObject(IDictionary<string, object> param)
//        {
//            string msg = String.Format("AddNewObject is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long UpdateObject(IDictionary<string, object> param)
//        {
//            string msg = String.Format("UpdateObject is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long DeleteObject(IDictionary<string, object> param)
//        {
//            string msg = String.Format("DeleteObject is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Query1ArrayList(object param0)
//        {
//            string msg = String.Format("Query1ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Query2ArrayList(object param0, object param1)
//        {
//            string msg = String.Format("Query2ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Query3ArrayList(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Query3ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Query4ArrayList(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Query4ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Query5ArrayList(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Query5ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Query6ArrayList(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Query6ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Query7ArrayList(object param0, object param1, object param2, object param3, object param4, object param5,
//            object param6)
//        {
//            string msg = String.Format("Query7ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Query8ArrayList(object param0, object param1, object param2, object param3, object param4, object param5,
//            object param6, object param7)
//        {
//            string msg = String.Format("Query8ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Query1Bool(object param0)
//        {
//            string msg = String.Format("Query1Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Query2Bool(object param0, object param1)
//        {
//            string msg = String.Format("Query2Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Query3Bool(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Query3Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Query4Bool(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Query4Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Query5Bool(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Query5Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Query6Bool(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Query6Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Query7Bool(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Query7Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Query8Bool(object param0, object param1, object param2, object param3, object param4, object param5, object param6,
//            object param7)
//        {
//            string msg = String.Format("Query8Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual int Query1Int32(object param0)
//        {
//            string msg = String.Format("Query1Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual int Query2Int32(object param0, object param1)
//        {
//            string msg = String.Format("Query2Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual int Query3Int32(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Query3Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual int Query4Int32(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Query4Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual int Query5Int32(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Query5Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual int Query6Int32(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Query6Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual int Query7Int32(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Query7Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual int Query8Int32(object param0, object param1, object param2, object param3, object param4, object param5, object param6,
//            object param7)
//        {
//            string msg = String.Format("Query8Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long Query1Int64(object param0)
//        {
//            string msg = String.Format("Query1Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long Query2Int64(object param0, object param1)
//        {
//            string msg = String.Format("Query2Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long Query3Int64(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Query3Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long Query4Int64(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Query4Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long Query5Int64(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Query5Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long Query6Int64(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Query6Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long Query7Int64(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Query7Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long Query8Int64(object param0, object param1, object param2, object param3, object param4, object param5, object param6,
//            object param7)
//        {
//            string msg = String.Format("Query8Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long? Query1Long(object param0)
//        {
//            string msg = String.Format("Query1Long is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long? Query2Long(object param0, object param1)
//        {
//            string msg = String.Format("Query2Long is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long? Query3Long(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Query3Long is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long? Query4Long(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Query4Long is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long? Query5Long(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Query5Long is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long? Query6Long(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Query6Long is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long? Query7Long(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Query7Long is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual long? Query8Long(object param0, object param1, object param2, object param3, object param4, object param5, object param6,
//            object param7)
//        {
//            string msg = String.Format("Query8Long is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Query1DataSet(object param0)
//        {
//            string msg = String.Format("Query1DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Query2DataSet(object param0, object param1)
//        {
//            string msg = String.Format("Query2DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Query3DataSet(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Query3DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Query4DataSet(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Query4DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Query5DataSet(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Query5DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Query6DataSet(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Query6DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Query7DataSet(object param0, object param1, object param2, object param3, object param4, object param5,
//            object param6)
//        {
//            string msg = String.Format("Query7DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Query8DataSet(object param0, object param1, object param2, object param3, object param4, object param5,
//            object param6, object param7)
//        {
//            string msg = String.Format("Query8DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Query1DataTable(object param0)
//        {
//            string msg = String.Format("Query1DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Query2DataTable(object param0, object param1)
//        {
//            string msg = String.Format("Query2DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Query3DataTable(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Query3DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Query4DataTable(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Query4DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Query5DataTable(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Query5DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Query6DataTable(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Query6DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Query7DataTable(object param0, object param1, object param2, object param3, object param4, object param5,
//            object param6)
//        {
//            string msg = String.Format("Query7DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Query8DataTable(object param0, object param1, object param2, object param3, object param4, object param5,
//            object param6, object param7)
//        {
//            string msg = String.Format("Query8DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Query1IDictionary(object param0)
//        {
//            string msg = String.Format("Query1IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Query2IDictionary(object param0, object param1)
//        {
//            string msg = String.Format("Query2IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Query3IDictionary(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Query3IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Query4IDictionary(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Query4IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Query5IDictionary(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Query5IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Query6IDictionary(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Query6IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Query7IDictionary(object param0, object param1, object param2, object param3, object param4, object param5,
//            object param6)
//        {
//            string msg = String.Format("Query7IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Query8IDictionary(object param0, object param1, object param2, object param3, object param4, object param5,
//            object param6, object param7)
//        {
//            string msg = String.Format("Query8IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Execute1ArrayList(object param0)
//        {
//            string msg = String.Format("Execute1ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Execute2ArrayList(object param0, object param1)
//        {
//            string msg = String.Format("Execute2ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Execute3ArrayList(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Execute3ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Execute4ArrayList(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Execute4ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Execute5ArrayList(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Execute5ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Execute6ArrayList(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Execute6ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Execute7ArrayList(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Execute7ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList Execute8ArrayList(object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            string msg = String.Format("Execute8ArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList ExecuteScalarArrayList(object param)
//        {
//            string msg = String.Format("ExecuteScalarArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual ArrayList ExecuteNonQueryArrayList(object param)
//        {
//            string msg = String.Format("ExecuteNonQueryArrayList is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }


//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <returns></returns>
//        public virtual ArrayList ExecuteQuery1ArrayList(QueryType queryType, object param0)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query1ArrayList(param0);
//                case QueryType.Execute:
//                    return Execute1ArrayList(param0);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <returns></returns>
//        public virtual ArrayList ExecuteQuery2ArrayList(QueryType queryType, object param0, object param1)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query2ArrayList(param0, param1);
//                case QueryType.Execute:
//                    return Execute2ArrayList(param0, param1);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <returns></returns>
//        public virtual ArrayList ExecuteQuery3ArrayList(QueryType queryType, object param0, object param1, object param2)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query3ArrayList(param0, param1, param2);
//                case QueryType.Execute:
//                    return Execute3ArrayList(param0, param1, param2);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <returns></returns>
//        public virtual ArrayList ExecuteQuery4ArrayList(QueryType queryType, object param0, object param1, object param2, object param3)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query4ArrayList(param0, param1, param2, param3);
//                case QueryType.Execute:
//                    return Execute4ArrayList(param0, param1, param2, param3);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <returns></returns>
//        public virtual ArrayList ExecuteQuery5ArrayList(QueryType queryType, object param0, object param1, object param2, object param3, object param4)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query5ArrayList(param0, param1, param2, param3, param4);
//                case QueryType.Execute:
//                    return Execute5ArrayList(param0, param1, param2, param3, param4);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <returns></returns>
//        public virtual ArrayList ExecuteQuery6ArrayList(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query6ArrayList(param0, param1, param2, param3, param4, param5);
//                case QueryType.Execute:
//                    return Execute6ArrayList(param0, param1, param2, param3, param4, param5);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <returns></returns>
//        public virtual ArrayList ExecuteQuery7ArrayList(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query7ArrayList(param0, param1, param2, param3, param4, param5, param6);
//                case QueryType.Execute:
//                    return Execute7ArrayList(param0, param1, param2, param3, param4, param5, param6);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <param name="param7"></param>
//        /// <returns></returns>
//        public virtual ArrayList ExecuteQuery8ArrayList(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query8ArrayList(param0, param1, param2, param3, param4, param5, param6, param7);
//                case QueryType.Execute:
//                    return Execute8ArrayList(param0, param1, param2, param3, param4, param5, param6, param7);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        public virtual bool ExecuteBool(object param)
//        {
//            string msg = String.Format("ExecuteBool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Execute1Bool(object param0)
//        {
//            string msg = String.Format("Execute1Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Execute2Bool(object param0, object param1)
//        {
//            string msg = String.Format("Execute2Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Execute3Bool(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Execute3Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Execute4Bool(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Execute4Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Execute5Bool(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Execute5Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Execute6Bool(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Execute6Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Execute7Bool(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Execute7Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool Execute8Bool(object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            string msg = String.Format("Execute8Bool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool ExecuteScalarBool(object param)
//        {
//            string msg = String.Format("ExecuteScalarBool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual bool ExecuteNonQueryBool(object param)
//        {
//            string msg = String.Format("ExecuteNonQueryBool is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <returns></returns>
//        public virtual bool ExecuteQuery1Bool(QueryType queryType, object param0)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query1Bool(param0);
//                case QueryType.Execute:
//                    return Execute1Bool(param0);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <returns></returns>
//        public virtual bool ExecuteQuery2Bool(QueryType queryType, object param0, object param1)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query2Bool(param0, param1);
//                case QueryType.Execute:
//                    return Execute2Bool(param0, param1);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <returns></returns>
//        public virtual bool ExecuteQuery3Bool(QueryType queryType, object param0, object param1, object param2)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query3Bool(param0, param1, param2);
//                case QueryType.Execute:
//                    return Execute3Bool(param0, param1, param2);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <returns></returns>
//        public virtual bool ExecuteQuery4Bool(QueryType queryType, object param0, object param1, object param2, object param3)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query4Bool(param0, param1, param2, param3);
//                case QueryType.Execute:
//                    return Execute4Bool(param0, param1, param2, param3);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <returns></returns>
//        public virtual bool ExecuteQuery5Bool(QueryType queryType, object param0, object param1, object param2, object param3, object param4)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query5Bool(param0, param1, param2, param3, param4);
//                case QueryType.Execute:
//                    return Execute5Bool(param0, param1, param2, param3, param4);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <returns></returns>
//        public virtual bool ExecuteQuery6Bool(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query6Bool(param0, param1, param2, param3, param4, param5);
//                case QueryType.Execute:
//                    return Execute6Bool(param0, param1, param2, param3, param4, param5);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <returns></returns>
//        public virtual bool ExecuteQuery7Bool(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query7Bool(param0, param1, param2, param3, param4, param5, param6);
//                case QueryType.Execute:
//                    return Execute7Bool(param0, param1, param2, param3, param4, param5, param6);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <param name="param7"></param>
//        /// <returns></returns>
//        public virtual bool ExecuteQuery8Bool(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query8Bool(param0, param1, param2, param3, param4, param5, param6, param7);
//                case QueryType.Execute:
//                    return Execute8Bool(param0, param1, param2, param3, param4, param5, param6, param7);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        public virtual Int32 Execute1Int32(object param0)
//        {
//            string msg = String.Format("Execute1Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int32 Execute2Int32(object param0, object param1)
//        {
//            string msg = String.Format("Execute2Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int32 Execute3Int32(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Execute3Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int32 Execute4Int32(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Execute4Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int32 Execute5Int32(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Execute5Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int32 Execute6Int32(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Execute6Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int32 Execute7Int32(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Execute7Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int32 Execute8Int32(object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            string msg = String.Format("Execute8Int32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int32 ExecuteScalarInt32(object param)
//        {
//            string msg = String.Format("ExecuteScalarInt32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int32 ExecuteNonQueryInt32(object param)
//        {
//            string msg = String.Format("ExecuteNonQueryInt32 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <returns></returns>
//        public virtual Int32 ExecuteQuery1Int32(QueryType queryType, object param0)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    if (!(param0 is IDictionary<string, object>)) throw new ArgumentException("AddNewObject requires an parameter of type IDictionary<string, object>");
//                    return (Int32)AddNewObject(param0 as IDictionary<string, object>);
//                case QueryType.Delete:
//                    if (!(param0 is IDictionary<string, object>)) throw new ArgumentException("Delete requires an parameter of type IDictionary<string, object>");
//                    return (Int32)DeleteObject(param0 as IDictionary<string, object>);
//                case QueryType.Update:
//                    if (!(param0 is IDictionary<string, object>)) throw new ArgumentException("Update requires an parameter of type IDictionary<string, object>");
//                    return (Int32)UpdateObject(param0 as IDictionary<string, object>);
//                case QueryType.Query:
//                    return Query1Int32(param0);
//                case QueryType.Execute:
//                    return Execute1Int32(param0);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <returns></returns>
//        public virtual Int32 ExecuteQuery2Int32(QueryType queryType, object param0, object param1)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query2Int32(param0, param1);
//                case QueryType.Execute:
//                    return Execute2Int32(param0, param1);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <returns></returns>
//        public virtual Int32 ExecuteQuery3Int32(QueryType queryType, object param0, object param1, object param2)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query3Int32(param0, param1, param2);
//                case QueryType.Execute:
//                    return Execute3Int32(param0, param1, param2);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <returns></returns>
//        public virtual Int32 ExecuteQuery4Int32(QueryType queryType, object param0, object param1, object param2, object param3)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query4Int32(param0, param1, param2, param3);
//                case QueryType.Execute:
//                    return Execute4Int32(param0, param1, param2, param3);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <returns></returns>
//        public virtual Int32 ExecuteQuery5Int32(QueryType queryType, object param0, object param1, object param2, object param3, object param4)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query5Int32(param0, param1, param2, param3, param4);
//                case QueryType.Execute:
//                    return Execute5Int32(param0, param1, param2, param3, param4);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <returns></returns>
//        public virtual Int32 ExecuteQuery6Int32(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query6Int32(param0, param1, param2, param3, param4, param5);
//                case QueryType.Execute:
//                    return Execute6Int32(param0, param1, param2, param3, param4, param5);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <returns></returns>
//        public virtual Int32 ExecuteQuery7Int32(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query7Int32(param0, param1, param2, param3, param4, param5, param6);
//                case QueryType.Execute:
//                    return Execute7Int32(param0, param1, param2, param3, param4, param5, param6);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <param name="param7"></param>
//        /// <returns></returns>
//        public virtual Int32 ExecuteQuery8Int32(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query8Int32(param0, param1, param2, param3, param4, param5, param6, param7);
//                case QueryType.Execute:
//                    return Execute8Int32(param0, param1, param2, param3, param4, param5, param6, param7);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        public virtual Int64 Execute1Int64(object param0)
//        {
//            string msg = String.Format("Execute1Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int64 Execute2Int64(object param0, object param1)
//        {
//            string msg = String.Format("Execute2Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int64 Execute3Int64(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Execute3Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int64 Execute4Int64(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Execute4Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int64 Execute5Int64(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Execute5Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int64 Execute6Int64(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Execute6Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int64 Execute7Int64(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Execute7Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int64 Execute8Int64(object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            string msg = String.Format("Execute8Int64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int64 ExecuteScalarInt64(object param)
//        {
//            string msg = String.Format("ExecuteScalarInt64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual Int64 ExecuteNonQueryInt64(object param)
//        {
//            string msg = String.Format("ExecuteNonQueryInt64 is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <returns></returns>
//        public virtual Int64 ExecuteQuery1Int64(QueryType queryType, object param0)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    if (!(param0 is IDictionary<string, object>)) throw new ArgumentException("AddNewObject requires an parameter of type IDictionary<string, object>");
//                    return (Int64)AddNewObject(param0 as IDictionary<string, object>);
//                case QueryType.Delete:
//                    if (!(param0 is IDictionary<string, object>)) throw new ArgumentException("AddNewObject requires an parameter of type IDictionary<string, object>");
//                    return (Int64)DeleteObject(param0 as IDictionary<string, object>);
//                case QueryType.Update:
//                    if (!(param0 is IDictionary<string, object>)) throw new ArgumentException("AddNewObject requires an parameter of type IDictionary<string, object>");
//                    return (Int64)UpdateObject(param0 as IDictionary<string, object>);
//                case QueryType.Query:
//                    return Query1Int64(param0);
//                case QueryType.Execute:
//                    return Execute1Int64(param0);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <returns></returns>
//        public virtual Int64 ExecuteQuery2Int64(QueryType queryType, object param0, object param1)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query2Int64(param0, param1);
//                case QueryType.Execute:
//                    return Execute2Int64(param0, param1);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <returns></returns>
//        public virtual Int64 ExecuteQuery3Int64(QueryType queryType, object param0, object param1, object param2)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query3Int64(param0, param1, param2);
//                case QueryType.Execute:
//                    return Execute3Int64(param0, param1, param2);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <returns></returns>
//        public virtual Int64 ExecuteQuery4Int64(QueryType queryType, object param0, object param1, object param2, object param3)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query4Int64(param0, param1, param2, param3);
//                case QueryType.Execute:
//                    return Execute4Int64(param0, param1, param2, param3);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <returns></returns>
//        public virtual Int64 ExecuteQuery5Int64(QueryType queryType, object param0, object param1, object param2, object param3, object param4)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query5Int64(param0, param1, param2, param3, param4);
//                case QueryType.Execute:
//                    return Execute5Int64(param0, param1, param2, param3, param4);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <returns></returns>
//        public virtual Int64 ExecuteQuery6Int64(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query6Int64(param0, param1, param2, param3, param4, param5);
//                case QueryType.Execute:
//                    return Execute6Int64(param0, param1, param2, param3, param4, param5);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <returns></returns>
//        public virtual Int64 ExecuteQuery7Int64(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query7Int64(param0, param1, param2, param3, param4, param5, param6);
//                case QueryType.Execute:
//                    return Execute7Int64(param0, param1, param2, param3, param4, param5, param6);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <param name="param7"></param>
//        /// <returns></returns>
//        public virtual Int64 ExecuteQuery8Int64(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only have one parameter");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only have one parameter");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only have one parameter");
//                case QueryType.Query:
//                    return Query8Int64(param0, param1, param2, param3, param4, param5, param6, param7);
//                case QueryType.Execute:
//                    return Execute8Int64(param0, param1, param2, param3, param4, param5, param6, param7);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        public virtual DataSet Execute1DataSet(object param0)
//        {
//            string msg = String.Format("Execute1DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Execute2DataSet(object param0, object param1)
//        {
//            string msg = String.Format("Execute2DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Execute3DataSet(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Execute3DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Execute4DataSet(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Execute4DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Execute5DataSet(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Execute5DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Execute6DataSet(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Execute6DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Execute7DataSet(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Execute7DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet Execute8DataSet(object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            string msg = String.Format("Execute8DataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet ExecuteScalarDataSet(object param)
//        {
//            string msg = String.Format("ExecuteScalarDataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataSet ExecuteNonQueryDataSet(object param)
//        {
//            string msg = String.Format("ExecuteNonQueryDataSet is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <returns></returns>
//        public virtual DataSet ExecuteQuery1DataSet(QueryType queryType, object param0)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query1DataSet(param0);
//                case QueryType.Execute:
//                    return Execute1DataSet(param0);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <returns></returns>
//        public virtual DataSet ExecuteQuery2DataSet(QueryType queryType, object param0, object param1)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query2DataSet(param0, param1);
//                case QueryType.Execute:
//                    return Execute2DataSet(param0, param1);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <returns></returns>
//        public virtual DataSet ExecuteQuery3DataSet(QueryType queryType, object param0, object param1, object param2)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query3DataSet(param0, param1, param2);
//                case QueryType.Execute:
//                    return Execute3DataSet(param0, param1, param2);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <returns></returns>
//        public virtual DataSet ExecuteQuery4DataSet(QueryType queryType, object param0, object param1, object param2, object param3)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query4DataSet(param0, param1, param2, param3);
//                case QueryType.Execute:
//                    return Execute4DataSet(param0, param1, param2, param3);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <returns></returns>
//        public virtual DataSet ExecuteQuery5DataSet(QueryType queryType, object param0, object param1, object param2, object param3, object param4)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query5DataSet(param0, param1, param2, param3, param4);
//                case QueryType.Execute:
//                    return Execute5DataSet(param0, param1, param2, param3, param4);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <returns></returns>
//        public virtual DataSet ExecuteQuery6DataSet(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query6DataSet(param0, param1, param2, param3, param4, param5);
//                case QueryType.Execute:
//                    return Execute6DataSet(param0, param1, param2, param3, param4, param5);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <returns></returns>
//        public virtual DataSet ExecuteQuery7DataSet(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query7DataSet(param0, param1, param2, param3, param4, param5, param6);
//                case QueryType.Execute:
//                    return Execute7DataSet(param0, param1, param2, param3, param4, param5, param6);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <param name="param7"></param>
//        /// <returns></returns>
//        public virtual DataSet ExecuteQuery8DataSet(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query8DataSet(param0, param1, param2, param3, param4, param5, param6, param7);
//                case QueryType.Execute:
//                    return Execute8DataSet(param0, param1, param2, param3, param4, param5, param6, param7);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        public virtual DataTable Execute1DataTable(object param0)
//        {
//            string msg = String.Format("Execute1DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Execute2DataTable(object param0, object param1)
//        {
//            string msg = String.Format("Execute2DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Execute3DataTable(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Execute3DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Execute4DataTable(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Execute4DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Execute5DataTable(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Execute5DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Execute6DataTable(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Execute6DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Execute7DataTable(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Execute7DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable Execute8DataTable(object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            string msg = String.Format("Execute8DataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable ExecuteScalarDataTable(object param)
//        {
//            string msg = String.Format("ExecuteScalarDataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual DataTable ExecuteNonQueryDataTable(object param)
//        {
//            string msg = String.Format("ExecuteNonQueryDataTable is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <returns></returns>
//        public virtual DataTable ExecuteQuery1DataTable(QueryType queryType, object param0)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query1DataTable(param0);
//                case QueryType.Execute:
//                    return Execute1DataTable(param0);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <returns></returns>
//        public virtual DataTable ExecuteQuery2DataTable(QueryType queryType, object param0, object param1)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query2DataTable(param0, param1);
//                case QueryType.Execute:
//                    return Execute2DataTable(param0, param1);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <returns></returns>
//        public virtual DataTable ExecuteQuery3DataTable(QueryType queryType, object param0, object param1, object param2)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query3DataTable(param0, param1, param2);
//                case QueryType.Execute:
//                    return Execute3DataTable(param0, param1, param2);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <returns></returns>
//        public virtual DataTable ExecuteQuery4DataTable(QueryType queryType, object param0, object param1, object param2, object param3)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query4DataTable(param0, param1, param2, param3);
//                case QueryType.Execute:
//                    return Execute4DataTable(param0, param1, param2, param3);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <returns></returns>
//        public virtual DataTable ExecuteQuery5DataTable(QueryType queryType, object param0, object param1, object param2, object param3, object param4)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query5DataTable(param0, param1, param2, param3, param4);
//                case QueryType.Execute:
//                    return Execute5DataTable(param0, param1, param2, param3, param4);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <returns></returns>
//        public virtual DataTable ExecuteQuery6DataTable(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query6DataTable(param0, param1, param2, param3, param4, param5);
//                case QueryType.Execute:
//                    return Execute6DataTable(param0, param1, param2, param3, param4, param5);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <returns></returns>
//        public virtual DataTable ExecuteQuery7DataTable(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query7DataTable(param0, param1, param2, param3, param4, param5, param6);
//                case QueryType.Execute:
//                    return Execute7DataTable(param0, param1, param2, param3, param4, param5, param6);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <param name="param7"></param>
//        /// <returns></returns>
//        public virtual DataTable ExecuteQuery8DataTable(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query8DataTable(param0, param1, param2, param3, param4, param5, param6, param7);
//                case QueryType.Execute:
//                    return Execute8DataTable(param0, param1, param2, param3, param4, param5, param6, param7);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        public virtual IDictionary<string, object> Execute1IDictionary(object param0)
//        {
//            string msg = String.Format("Execute1IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Execute2IDictionary(object param0, object param1)
//        {
//            string msg = String.Format("Execute2IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Execute3IDictionary(object param0, object param1, object param2)
//        {
//            string msg = String.Format("Execute3IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Execute4IDictionary(object param0, object param1, object param2, object param3)
//        {
//            string msg = String.Format("Execute4IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Execute5IDictionary(object param0, object param1, object param2, object param3, object param4)
//        {
//            string msg = String.Format("Execute5IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Execute6IDictionary(object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            string msg = String.Format("Execute6IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Execute7IDictionary(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            string msg = String.Format("Execute7IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> Execute8IDictionary(object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            string msg = String.Format("Execute8IDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> ExecuteScalarIDictionary(object param)
//        {
//            string msg = String.Format("ExecuteScalarIDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        public virtual IDictionary<string, object> ExecuteNonQueryIDictionary(object param)
//        {
//            string msg = String.Format("ExecuteNonQueryIDictionary is not implemented in dbaccess ({0})", this.GetType().FullName);
//            if (Log.IsErrorEnabled) Log.Error(msg);
//            CustomErrorDetail errorDetail = new CustomErrorDetail(msg, String.Empty);
//            throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotImplemented);
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <returns></returns>
//        public virtual IDictionary<string, object> ExecuteQuery1IDictionary(QueryType queryType, object param0)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query1IDictionary(param0);
//                case QueryType.Execute:
//                    return Execute1IDictionary(param0);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <returns></returns>
//        public virtual IDictionary<string, object> ExecuteQuery2IDictionary(QueryType queryType, object param0, object param1)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query2IDictionary(param0, param1);
//                case QueryType.Execute:
//                    return Execute2IDictionary(param0, param1);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <returns></returns>
//        public virtual IDictionary<string, object> ExecuteQuery3IDictionary(QueryType queryType, object param0, object param1, object param2)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query3IDictionary(param0, param1, param2);
//                case QueryType.Execute:
//                    return Execute3IDictionary(param0, param1, param2);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <returns></returns>
//        public virtual IDictionary<string, object> ExecuteQuery4IDictionary(QueryType queryType, object param0, object param1, object param2, object param3)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query4IDictionary(param0, param1, param2, param3);
//                case QueryType.Execute:
//                    return Execute4IDictionary(param0, param1, param2, param3);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <returns></returns>
//        public virtual IDictionary<string, object> ExecuteQuery5IDictionary(QueryType queryType, object param0, object param1, object param2, object param3, object param4)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query5IDictionary(param0, param1, param2, param3, param4);
//                case QueryType.Execute:
//                    return Execute5IDictionary(param0, param1, param2, param3, param4);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <returns></returns>
//        public virtual IDictionary<string, object> ExecuteQuery6IDictionary(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query6IDictionary(param0, param1, param2, param3, param4, param5);
//                case QueryType.Execute:
//                    return Execute6IDictionary(param0, param1, param2, param3, param4, param5);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <returns></returns>
//        public virtual IDictionary<string, object> ExecuteQuery7IDictionary(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query7IDictionary(param0, param1, param2, param3, param4, param5, param6);
//                case QueryType.Execute:
//                    return Execute7IDictionary(param0, param1, param2, param3, param4, param5, param6);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        /// <summary>
//        /// This is a wrapper from DBAccess2
//        /// </summary>
//        /// <param name="queryType"></param>
//        /// <param name="param0"></param>
//        /// <param name="param1"></param>
//        /// <param name="param2"></param>
//        /// <param name="param3"></param>
//        /// <param name="param4"></param>
//        /// <param name="param5"></param>
//        /// <param name="param6"></param>
//        /// <param name="param7"></param>
//        /// <returns></returns>
//        public virtual IDictionary<string, object> ExecuteQuery8IDictionary(QueryType queryType, object param0, object param1, object param2, object param3, object param4, object param5, object param6, object param7)
//        {
//            switch (queryType)
//            {
//                case QueryType.AddNew:
//                    throw new ArgumentException("QueryType.AddNewObject should only return int");
//                case QueryType.Delete:
//                    throw new ArgumentException("QueryType.DeleteObject should only return int");
//                case QueryType.Update:
//                    throw new ArgumentException("QueryType.UpdateObject should only return int");
//                case QueryType.Query:
//                    return Query8IDictionary(param0, param1, param2, param3, param4, param5, param6, param7);
//                case QueryType.Execute:
//                    return Execute8IDictionary(param0, param1, param2, param3, param4, param5, param6, param7);
//                default:
//                    throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
//            }
//        }

//        public virtual string ServiceIsAlive(string msg)
//        {
//            return String.Format("Service ({0}) is alive at {1} with message ({2})", this.GetType().FullName, DateTime.Now.ToLongDateString(), msg);
//        }

//        protected DataTable QueryTableDelegate(ITrsDbCommand cmd, string tableName)
//        {
//            try
//            {
//                var dr = cmd.ExecuteReader();
//                // need to add a name to the table to make it serializable
//                // http://stackoverflow.com/questions/12702/returning-datatables-in-wcf-net
//                DataTable dt = new DataTable(tableName);
//                dt.Load(dr);
//                foreach (DataColumn col in dt.Columns)
//                    col.ReadOnly = false;
//                return dt;
//            }
//            catch (Exception ex)
//            {
//                if (Log.IsErrorEnabled) Log.Error("QueryTableDelegate threw exception: ", ex);
//                throw;
//            }
//        }

//        /// <summary>
//        /// Execute a non query
//        /// </summary>
//        /// <param name="cmd">command</param>
//        /// <returns>number of rows affected</returns>
//        protected int ExecuteNoneQueryDelegate(ITrsDbCommand cmd)
//        {
//            try
//            {
//                return cmd.ExecuteNonQuery();
//            }
//            catch (Exception ex)
//            {
//                if (Log.IsErrorEnabled) Log.Error("ExecuteNoneQueryDelegate threw exception: ", ex);
//                throw;
//            }
//        }

//        protected object ExecuteScalarQueryDelegate(ITrsDbCommand cmd)
//        {
//            try
//            {
//                return cmd.ExecuteScalar();
//            }
//            catch (Exception ex)
//            {
//                if (Log.IsErrorEnabled) Log.Error("ExecuteScalarQueryDelegate threw exception: ", ex);
//                return null;
//            }
//        }

//        public void AssignValue(IDbDataParameter dbParam, object value, object defaultValue)
//        {
//            dbParam.Value = value ?? defaultValue;
//        }
//        public DbProvider GetDbProvider()
//        {
//            if (ConnectionString.IndexOf("ORAOLEDB.ORACLE") > 0)
//            {
//                return DbProvider.Oracle;
//            }
//            else if (ConnectionString.IndexOf("SQLOLEDB") > 0)
//            {
//                return DbProvider.MsSQL;
//            }
//            else
//            {
//                return DbProvider.Unknown;
//            }
//        }

//        public void Dispose()
//        {
//            // Nothing to dispose
//        }

//        public string CreateDateString(DbProvider dbProvider, DateTime dateTime)
//        {
//            string strDate = dateTime.ToString("G", System.Globalization.DateTimeFormatInfo.InvariantInfo);

//            if (dbProvider == DbProvider.Oracle)
//                return string.Format(" to_date('{0}','mm/dd/yyyy hh24:mi:ss')", strDate);

//            else if (dbProvider == DbProvider.MsSQL)
//            {
//                string dateString = dateTime.ToString("yyyyMMdd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
//                return string.Format(" CAST('{0}' AS DATETIME)", dateString);
//            }

//            return string.Format("'{0}'", strDate);
//        }
//    }
//}
