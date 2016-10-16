using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Data.Base
{
    /*public class CommentDBAccess : DBAccess, ICommentDBAccess
    {
        /// <summary>
        /// Add a logger
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string Name => "COMMENT";

        #region delegates functions

        /// <summary>
        /// Delegate function to execute insert command
        /// </summary>
        /// <param name="insertCommand"></param>
        /// <param name="param"></param>
        /// <returns>object Id</returns>
        protected long? AddNewDelegate(ITrsDbCommand insertCommand, EctdComment param)
        {
            try
            {
                IDbDataParameter keyParameter = insertCommand.Parameters.Add(CommentColumn.CommentKey, OleDbType.BigInt, 0, CommentColumn.CommentKey);
                keyParameter.Direction = ParameterDirection.Output;

                insertCommand.Parameters.Add(CommentColumn.LeafNodeKey, OleDbType.BigInt, 0, CommentColumn.LeafNodeKey).Value = param.LeafNodeKey;
                insertCommand.Parameters.Add(CommentColumn.Subject, OleDbType.VarWChar, 500, CommentColumn.Subject).Value = param.Subject;
                insertCommand.Parameters.Add(CommentColumn.Note, OleDbType.VarWChar, 2000, CommentColumn.Note).Value = param.Note;
                insertCommand.Parameters.Add(CommentColumn.AttachedFile, OleDbType.VarWChar, 2000, CommentColumn.AttachedFile).Value = param.AttachedFile;
                insertCommand.Parameters.Add(CommentColumn.CommentStatus, OleDbType.BigInt, 0, CommentColumn.CommentStatus).Value = param.CommentStatus;
                insertCommand.Parameters.Add(CommentColumn.CommentType, OleDbType.BigInt, 0, CommentColumn.CommentType).Value = param.CommentType;
                insertCommand.Parameters.Add(CommentColumn.UserKey, OleDbType.BigInt, 0, CommentColumn.UserKey).Value = param.UserKey;
                insertCommand.Parameters.Add(CommentColumn.AssignedUserKey, OleDbType.BigInt, 0, CommentColumn.AssignedUserKey).Value = !param.AssignedUserKey.HasValue ? 0 : param.AssignedUserKey;
                insertCommand.Parameters.Add(CommentColumn.ParentKey, OleDbType.BigInt, 0, CommentColumn.ParentKey).Value = param.ParentKey;
                insertCommand.Parameters.Add(CommentColumn.AccessType, OleDbType.Integer, 0, CommentColumn.AccessType).Value = param.AccessType;
                insertCommand.Parameters.Add(CommentColumn.CreationDate, OleDbType.Date, 0, CommentColumn.CreationDate).Value = param.CreationDate;

                insertCommand.ExecuteNonQuery();
                return (long?)keyParameter.Value;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(AddNewDelegate)} threw exception: ", ex);
                throw;
            }
        }

        /// <summary>
        /// Delegate function to execute update command
        /// </summary>
        /// <param name="updateCommand"></param>
        /// <param name="param"></param>
        /// <returns>object Id</returns>
        protected long? UpdateDelegate(ITrsDbCommand updateCommand, EctdComment param)
        {
            try
            {
                updateCommand.Parameters.Add(CommentColumn.CommentKey, OleDbType.BigInt, 0, CommentColumn.CommentKey).Value = param.CommentKey;
                updateCommand.Parameters.Add(CommentColumn.LeafNodeKey, OleDbType.BigInt, 0, CommentColumn.LeafNodeKey).Value = param.LeafNodeKey;
                updateCommand.Parameters.Add(CommentColumn.Subject, OleDbType.VarWChar, 500, CommentColumn.Subject).Value = param.Subject;
                updateCommand.Parameters.Add(CommentColumn.Note, OleDbType.VarWChar, 2000, CommentColumn.Note).Value = param.Note;
                updateCommand.Parameters.Add(CommentColumn.AttachedFile, OleDbType.VarWChar, 2000, CommentColumn.AttachedFile).Value = param.LeafNodeKey;
                updateCommand.Parameters.Add(CommentColumn.CommentStatus, OleDbType.BigInt, 0, CommentColumn.CommentStatus).Value = param.CommentStatus;
                updateCommand.Parameters.Add(CommentColumn.CommentType, OleDbType.BigInt, 0, CommentColumn.CommentType).Value = param.CommentType;
                updateCommand.Parameters.Add(CommentColumn.UserKey, OleDbType.BigInt, 0, CommentColumn.UserKey).Value = param.UserKey;
                object assignUserkey = param.AssignedUserKey.HasValue && param.AssignedUserKey > 0 ? (object)param.AssignedUserKey : DBNull.Value;
                updateCommand.Parameters.Add(CommentColumn.AssignedUserKey, OleDbType.BigInt, 0, CommentColumn.AssignedUserKey).Value = assignUserkey;
                updateCommand.Parameters.Add(CommentColumn.ParentKey, OleDbType.BigInt, 0, CommentColumn.ParentKey).Value = param.ParentKey;
                updateCommand.Parameters.Add(CommentColumn.AccessType, OleDbType.Integer, 0, CommentColumn.AccessType).Value = param.AccessType;
                updateCommand.Parameters.Add(CommentColumn.UpdatedDate, OleDbType.Date, 0, CommentColumn.CreationDate).Value = param.UpdatedDate;

                updateCommand.ExecuteNonQuery();
                return param.CommentKey;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(UpdateDelegate)} threw exception: ", ex);
                throw;
            }
        }

        /// <summary>
        /// Delegate function to execute delete command
        /// </summary>
        /// <param name="deleteCommand"></param>
        /// <param name="param"></param>
        /// <returns>object Id</returns>
        protected long? DeleteDelegate(ITrsDbCommand deleteCommand, long param)
        {
            try
            {
                deleteCommand.Parameters.Add(CommentColumn.CommentKey, OleDbType.BigInt, 0, CommentColumn.CommentKey).Value = param;

                deleteCommand.ExecuteNonQuery();
                return param;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(DeleteDelegate)} threw exception: ", ex);
                throw;
            }
        }

        /// <summary>
        /// Delegate function to execute search command
        /// </summary>
        /// <param name="searchCommand"></param>
        /// <param name="param"></param>
        /// <returns>object Id</returns>
        protected IList<EctdComment> FindDelegate(ITrsDbCommand searchCommand)
        {
            try
            {
                return ReaderToList(searchCommand.ExecuteReader());
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(FindDelegate)} threw exception: ", ex);
                throw;
            }
        }

        #endregion

        /// <summary>
        /// Add new data into EctdComment table
        /// </summary>
        /// <param name="param">domain object EctdComment</param>
        /// <returns>return object id if the add new is successfully</returns>
        public long? AddNew(EctdComment param)
        {
            try
            {
                return TrsGenericConnectCommand1Param(nameof(AddNew), CommandType.StoredProcedure, "spCommentInsert", AddNewDelegate, param);
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(AddNew)} threw exception: ", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(AddNew)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Find EctdComment object by key
        /// </summary>
        /// <param name="param">commentKey</param>
        /// <returns></returns>
        public EctdComment Find(long param)
        {
            var sql = $"select * from ectd_comment where CommentKey={param} ";

            try
            {
                IList<EctdComment> list = TrsGenericConnectCommand(nameof(Find), CommandType.Text, sql, FindDelegate);

                if (list == null || !list.Any())
                {
                    return null;
                }
                else if (list.Count != 1)
                {
                    throw new InvalidOperationException("Find retured more than one record");
                }

                return list[0];
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Find)} threw exception: ", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Find)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Update data EctdComment table
        /// </summary>
        /// <param name="param">domain object EctdComment</param>
        /// <returns>return object id if the add new is successfully</returns>
        public long? Update(EctdComment param)
        {
            try
            {
                return TrsGenericConnectCommand1Param(nameof(Update), CommandType.StoredProcedure, "spCommentUpdate", UpdateDelegate, param);
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Update)} threw exception: ", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Update)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Delete EctdComment table by key
        /// </summary>
        /// <param name="param">commentKey</param>
        /// <returns>return number of records are deleted</returns>
		public long? Delete(long param)
        {
            try
            {
                return TrsGenericConnectCommand1Param(nameof(Delete), CommandType.StoredProcedure, "spCommentDelete", DeleteDelegate, param);
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Delete)} threw exception: ", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Delete)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Get comment by leafnodekey
        /// </summary>
        /// <param name="param0">leafNodeKey</param>
        /// <returns></returns>
        public override DataSet Query1DataSet(object param0)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(Query1DataSet)}");

            try
            {
                DataSet ds = new DataSet();
                string sql = "select a.commentkey, a.leafnodekey, a.submissionkey, a.subject, a.note, a.attachedfile, a.commentstatus,  " +
                            " a.CommentType, a.userkey, a.assigneduserkey," +
                            " a.parentkey, a.accesstype, a.isdeleted, a.creationdate, a.updateddate, b.sequencenumber " +
                            " from ectd_comment a, ectd_submission b" +
                             $" where a.submissionkey = b.submissionkey and a.LeafNodeKey = {Convert.ToInt64(param0)} and a.IsDeleted = 0" +
                             " Order By a.CommentKey ASC ";

                ds.Tables.Add(TrsGenericConnectCommand1Param(nameof(Query1DataSet), CommandType.Text, sql, QueryTableDelegate, "EctdComment"));

                return ds;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Query1DataSet)} threw exception", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Query1DataSet)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        ///  get comment by parent key
        /// </summary>
        /// <param name="param0">ParentKey (long)</param>
        /// <param name="param1"></param>
        /// <returns></returns>
        public override DataSet Query2DataSet(object param0, object param1)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(Query2DataSet)}");

            try
            {
                DataSet ds = new DataSet();
                string sql =
                    "select a.commentkey, a.leafnodekey, a.submissionkey, a.subject, a.note, a.attachedfile, a.commentstatus,  " +
                    " a.CommentType, a.userkey, a.assigneduserkey," +
                    " a.parentkey, a.accesstype, a.isdeleted, a.creationdate, a.updateddate, b.sequencenumber " +
                    " from ectd_comment a, ectd_submission b" +
                    $" where a.submissionkey = b.submissionkey and a.ParentKey = { Convert.ToInt64(param0)} and a.IsDeleted = 0 " +
                    " Order By a.CommentKey ASC ";

                ds.Tables.Add(TrsGenericConnectCommand1Param(nameof(Query2DataSet), CommandType.Text, sql, QueryTableDelegate, "EctdComment"));

                return ds;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Query2DataSet)} threw exception", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Query2DataSet)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Get comments by userkey, isuser and iscombined
        /// </summary>
        /// <param name="param0">user key (long)</param>
        /// <param name="param1">is user (bool)</param>
        /// <param name="param2">is combined (bool)</param>
        /// <returns></returns>
        public override DataSet Query3DataSet(object param0, object param1, object param2)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(Query3DataSet)}");

            try
            {
                DataSet ds = new DataSet();
                string whereclause = "";

                long userid = (long)param0;
                bool isUser = (bool)param1;
                bool combined = (bool)param2;

                if (isUser && combined) // group
                {
                    whereclause = string.Format(" where a.IsDeleted= 0 and ( a.accesstype = {0} or a.userkey = {1} or a.assigneduserkey = {1} or a.assigneduserkey in (select parentkey from ISI_ObjectTree where childkey = {1} and parenttype = {2} and childtype = {3}) )"
                        , CommentAccessType.PublicComment, userid, Convert.ToInt32(ObjectType.Group), Convert.ToInt32(ObjectType.User));
                }
                else
                {
                    whereclause = $" where a.assigneduserkey = {userid} and a.IsDeleted = 0";
                }

                whereclause += " and a.submissionkey = b.submissionkey";

                string sql =
                    "select a.commentkey, a.leafnodekey, a.submissionkey, a.subject, a.note, a.attachedfile, a.commentstatus,  " +
                    " a.CommentType, a.userkey, a.assigneduserkey," +
                    " a.parentkey, a.accesstype, a.isdeleted, a.creationdate, a.updateddate, b.sequencenumber " +
                    " from ectd_comment a, ectd_submission b" +
                    whereclause +
                    " Order By a.CommentKey ASC ";

                ds.Tables.Add(TrsGenericConnectCommand1Param(nameof(Query3DataSet), CommandType.Text, sql, QueryTableDelegate, "EctdComment"));

                return ds;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Query3DataSet)} threw exception", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Query3DataSet)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Get comments by userkey, isuser, iscombined and isdeleted or application key
        /// </summary>
        /// <param name="param0">user id</param>
        /// <param name="param1">is user</param>
        /// <param name="param2">is combined</param>
        /// <param name="param3">is deleted or application key</param>
        /// <returns></returns>
        public override DataSet Query4DataSet(object param0, object param1, object param2, object param3)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(Query4DataSet)}");

            try
            {
                DataSet ds = new DataSet();
                string whereclause = "";
                long userid = (long)param0;
                bool isUser = (bool)param1;
                bool combined = (bool)param2;

                if (param3 is bool)
                {
                    bool isDeleted = (bool)param3;

                    if (isUser && combined) // group
                    {
                        whereclause = string.Format(" where a.IsDeleted= {4} and ( a.accesstype = {0} or a.userkey = {1} or a.assigneduserkey = {1} or a.assigneduserkey in (select parentkey from ISI_ObjectTree where childkey = {1} and parenttype = {2} and childtype = {3}) )"
                            , CommentAccessType.PublicComment, userid, Convert.ToInt32(ObjectType.Group), Convert.ToInt32(ObjectType.User), Convert.ToInt32(isDeleted));
                    }
                    else
                    {
                        whereclause = $" where a.assigneduserkey = {userid} and a.IsDeleted = {Convert.ToInt32(isDeleted)} ";
                    }

                    whereclause += " and a.submissionkey = b.submissionkey";
                }
                else if (param3 is long)
                {
                    long appkey = (long)param3;

                    if (isUser && combined) // group
                    {
                        whereclause = $" where a.submissionkey = b.submissionkey and a.submissionkey in ( select submissionkey from ectd_submission where applicationkey = {appkey} )";
                        whereclause += " and a.IsDeleted = 0";
                        whereclause += string.Format(" and ( a.accesstype = {0} or a.userkey = {1} or a.assigneduserkey = {1} or exists ( select parentkey from ISI_ObjectTree where parentkey=a.assigneduserkey and childkey = {1} and parenttype = {2} and childtype = {3}) )"
                            , CommentAccessType.PublicComment, userid, Convert.ToInt32(ObjectType.Group), Convert.ToInt32(ObjectType.User));
                        //where += string.Format(" and submissionkey in ( select submissionkey from ectd_submission where applicationkey = {0} )",appkey);
                    }
                    else
                    {
                        whereclause = string.Format(" where ( a.assigneduserkey = {0} or a.userkey = {0} ) and a.IsDeleted = 0", userid);
                        whereclause += $" and a.submissionkey in ( select submissionkey from ectd_submission where applicationkey = {appkey} )";
                    }
                }
                else
                {
                    throw new ArgumentException();
                }

                string sql =
                    "select a.commentkey, a.leafnodekey, a.submissionkey, a.subject, a.note, a.attachedfile, a.commentstatus,  " +
                    " a.CommentType, a.userkey, a.assigneduserkey," +
                    " a.parentkey, a.accesstype, a.isdeleted, a.creationdate, a.updateddate, b.sequencenumber " +
                    " from ectd_comment a, ectd_submission b" +
                    whereclause +
                    " Order By a.CommentKey ASC ";

                ds.Tables.Add(TrsGenericConnectCommand1Param(nameof(Query4DataSet), CommandType.Text, sql, QueryTableDelegate, "EctdComment"));

                return ds;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Query4DataSet)} threw exception", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Query4DataSet)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Get comments by userkey, isuser, iscombined and submissionKey
        /// </summary>
        /// <param name="param0">user id</param>
        /// <param name="param1">is user</param>
        /// <param name="param2">is combined</param>
        /// <param name="param3">sub key</param>
        /// <param name="param4">is submission</param>
        public override DataSet Query5DataSet(object param0, object param1, object param2, object param3, object param4)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(Query5DataSet)}");

            try
            {
                DataSet ds = new DataSet();
                string whereclause = "";
                long userid = (long)param0;
                bool isUser = (bool)param1;
                bool combined = (bool)param2;
                long subkey = (long)param3;
                bool isSubmission = (bool)param4;

                if (isUser && combined) // group
                {
                    whereclause = $" where a.submissionkey = b.submissionkey and a.submissionkey = {subkey} ";
                    whereclause += " and a.IsDeleted = 0";
                    whereclause += string.Format(" and ( a.accesstype = {0} or a.userkey = {1} or a.assigneduserkey = {1} or exists ( select parentkey from ISI_ObjectTree where parentkey=a.assigneduserkey and childkey = {1} and parenttype = {2} and childtype = {3}) )"
                        , CommentAccessType.PublicComment, userid, Convert.ToInt32(ObjectType.Group), Convert.ToInt32(ObjectType.User));
                    //where += string.Format(" and submissionkey in ( select submissionkey from ectd_submission where applicationkey = {0} )",appkey);
                }
                else
                {
                    whereclause = $" where ( a.assigneduserkey = {userid} or a.userkey = {userid} ) and a.IsDeleted = 0";
                    whereclause += $" and a.submissionkey = {subkey} ";
                }

                string sql =
                    "select a.commentkey, a.leafnodekey, a.submissionkey, a.subject, a.note, a.attachedfile, a.commentstatus,  " +
                    " a.CommentType, a.userkey, a.assigneduserkey," +
                    " a.parentkey, a.accesstype, a.isdeleted, a.creationdate, a.updateddate, b.sequencenumber " +
                    " from ectd_comment a, ectd_submission b" +
                    whereclause +
                    " Order By a.CommentKey ASC ";

                ds.Tables.Add(TrsGenericConnectCommand1Param(nameof(Query5DataSet), CommandType.Text, sql, QueryTableDelegate, "EctdComment"));

                return ds;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Query5DataSet)} threw exception", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Query5DataSet)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.BadRequest);
            }
        }

        public override int Execute1Int32(object param0)
        {
            try
            {
                string sql = string.Format("Update ECTD_COMMENT Set IsDeleted = 0 where commentkey = {0}", param0.ToString());

                if (sql == null)
                {
                    Log.Error("Execute throwing Invalid Arguement at 'GENERAL' query");
                    throw new Exception("Invalid Arguement at 'GENERAL' query\n");
                }

                return TrsGenericConnectCommand(nameof(Execute1Int32), CommandType.Text, sql, ExecuteNoneQueryDelegate);
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Execute1Int32)} threw exception", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail("Execute2Int32(object param0, object param1) threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param0">user id</param>
        /// <param name="param1">is user</param>
        /// <param name="param2">is combined</param>
        public override DataSet Execute3DataSet(object param0, object param1, object param2)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(Query3DataSet)}");

            try
            {
                DataSet ds = new DataSet();

                long userid = (long)param0;
                bool isUser = (bool)param1;
                bool combined = (bool)param2;
                string whereclause = " where a.submissionkey = b.submissionkey and b.applicationkey = c.applicationkey and d.leafnodekey = a.leafnodekey and a.IsDeleted = 0 and b.isdeleted = 0 and c.isdeleted = 0 ";

                if (isUser && combined) // group
                {
                    whereclause += string.Format(" and ( a.accesstype = {0} or a.userkey = {1} or a.assigneduserkey = {1} or exists ( select parentkey from ISI_ObjectTree where parentkey=a.assigneduserkey and childkey = {1} and parenttype = {2} and childtype = {3}) )"
                        , CommentAccessType.PublicComment, userid, Convert.ToInt32(ObjectType.Group), Convert.ToInt32(ObjectType.User));
                }
                else
                {
                    whereclause = string.Format(" and ( a.assigneduserkey = {0} or a.userkey = {0} ) ", userid);
                }

                // INDEX: ectd_comment, NO, NA, ISDELETED, PARENTKEY, ACCESSTYPE, USERKEY, ASSIGNEDUSERKEY
                //      Index for submissionkey, leafnodekey column should be enough
                //string sql = string.Format("select * from ectd_comment");
                string sql = "select a.commentkey, a.leafnodekey,c.applicationkey, a.submissionkey, a.subject, a.note, a.attachedfile, a.commentstatus,  " +
                    " a.CommentType, a.userkey, a.assigneduserkey, a.parentkey, a.accesstype, a.isdeleted, a.creationdate, a.updateddate, b.sequencenumber, d.*" +
                    " from ectd_comment a, ectd_submission b , ectd_application c,ectd_leafnode d";

                sql += whereclause;
                sql += " Order By c.applicationkey, b.sequencenumber, a.CommentKey ASC ";

                ds.Tables.Add(TrsGenericConnectCommand1Param(nameof(Execute3DataSet), CommandType.Text, sql, QueryTableDelegate, "EctdComment"));

                return ds;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Execute3DataSet)} threw exception", ex);

                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Execute3DataSet)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Get comments by userid, isUser, iscombined and applicationkey
        /// </summary>
        /// <param name="param0">user id</param>
        /// <param name="param1">is user</param>
        /// <param name="param2">is combined</param>
        /// <param name="param3">app key</param>
        public override DataSet Execute4DataSet(object param0, object param1, object param2, object param3)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(Execute4DataSet)}");

            try
            {
                DataSet ds = new DataSet();

                long userid = (long)param0;
                bool isUser = (bool)param1;
                bool combined = (bool)param2;
                long appid = (long)param3;
                string whereclause = $" where a.submissionkey = b.submissionkey and b.applicationkey = c.applicationkey and d.leafnodekey = a.leafnodekey and a.IsDeleted = 0 and b.isdeleted = 0 and c.isdeleted = 0 and c.applicationkey = {appid}";

                if (isUser && combined) // group
                {
                    whereclause += string.Format(" and ( a.accesstype = {0} or a.userkey = {1} or a.assigneduserkey = {1} or exists ( select parentkey from ISI_ObjectTree where parentkey=a.assigneduserkey and childkey = {1} and parenttype = {2} and childtype = {3}) )"
                        , CommentAccessType.PublicComment, userid, Convert.ToInt32(ObjectType.Group), Convert.ToInt32(ObjectType.User));
                }
                else
                {
                    whereclause = string.Format(" and ( a.assigneduserkey = {0} or a.userkey = {0} ) ", userid);
                }

                // INDEX: ectd_comment, NO, NA, ISDELETED, PARENTKEY, ACCESSTYPE, USERKEY, ASSIGNEDUSERKEY
                //      Index for submissionkey, leafnodekey column should be enough
                //string sql = string.Format("select * from ectd_comment");
                string sql = "select a.commentkey, a.leafnodekey,c.applicationkey, a.submissionkey, a.subject, a.note, a.attachedfile, a.commentstatus,  " +
                    " a.CommentType, a.userkey, a.assigneduserkey, a.parentkey, a.accesstype, a.isdeleted, a.creationdate, a.updateddate, b.sequencenumber, d.*" +
                    " from ectd_comment a, ectd_submission b , ectd_application c,ectd_leafnode d";

                sql += whereclause;
                sql += " Order By c.applicationkey, b.sequencenumber, a.CommentKey ASC ";

                ds.Tables.Add(TrsGenericConnectCommand1Param(nameof(Execute4DataSet), CommandType.Text, sql, QueryTableDelegate, "EctdComment"));

                return ds;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Execute4DataSet)} threw exception", ex);

                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Execute4DataSet)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.BadRequest);
            }
        }

        private IList<EctdComment> ReaderToList(IDataReader reader)
        {
            IList<EctdComment> list = null;
            while (reader.Read())
            {
                if (list == null) list = new List<EctdComment>();
                EctdComment rec = new EctdComment();
                rec.CommentKey = long.Parse(reader[CommentColumn.CommentKey].ToString());
                rec.LeafNodeKey = long.Parse(reader[CommentColumn.LeafNodeKey].ToString());
                rec.SubmissionKey = reader[CommentColumn.SubmissionKey] == DBNull.Value ? (long?)null : long.Parse(reader[CommentColumn.SubmissionKey].ToString());
                rec.Subject = (string)reader[CommentColumn.Subject];
                rec.Note = (string)reader[CommentColumn.Note];
                rec.AttachedFile = (string)reader[CommentColumn.AttachedFile];
                rec.CommentStatus = reader[CommentColumn.CommentStatus] == DBNull.Value ? (long?)null : long.Parse(reader[CommentColumn.CommentStatus].ToString());
                rec.CommentType = reader[CommentColumn.CommentType] == DBNull.Value ? (long?)null : long.Parse(reader[CommentColumn.CommentType].ToString());
                rec.UserKey = reader[CommentColumn.UserKey] == DBNull.Value ? (long?)null : long.Parse(reader[CommentColumn.UserKey].ToString());
                rec.AssignedUserKey = reader[CommentColumn.AssignedUserKey] == DBNull.Value ? (long?)null : long.Parse(reader[CommentColumn.AssignedUserKey].ToString());
                rec.ParentKey = reader[CommentColumn.ParentKey] == DBNull.Value ? (long?)null : long.Parse(reader[CommentColumn.ParentKey].ToString());
                rec.AccessType = reader[CommentColumn.AccessType] == DBNull.Value ? (long?)null : long.Parse(reader[CommentColumn.AccessType].ToString());
                rec.IsDeleted = reader[CommentColumn.IsDeleted] == DBNull.Value ? (long?)null : long.Parse(reader[CommentColumn.IsDeleted].ToString());
                rec.CreationDate = reader[CommentColumn.CreationDate] == DBNull.Value ? (DateTime?)null : (DateTime)reader[CommentColumn.CreationDate];
                rec.UpdatedDate = reader[CommentColumn.UpdatedDate] == DBNull.Value ? (DateTime?)null : (DateTime)reader[CommentColumn.UpdatedDate];
                list.Add(rec);
            }
            return list;
        }

        /// <summary>
        /// Get all Ectd Comments belong to current application
        /// </summary>
        /// <param name="param0">Application Id</param>
        /// <returns>A collection of Ectd Comment objects</returns>
        public override DataSet Execute1DataSet(object param0)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(Execute1DataSet)}");
            try
            {
                var ds = new DataSet("ectd_comment");
                //var sqlQuery = "select * from ectd_comment where IsDeleted = 0 and leafnodekey in (" +
                //				"select distinct leafnodekey from ectd_leafnode where IsDeleted = 0 and foldernodekey in (" +
                //				"select foldernodekey from ectd_foldernode where IsDeleted = 0 and nodegroupkey in (" +
                //				"select nodegroupkey from ectd_nodegroup where IsDeleted = 0 and submissionkey in (" +
                //				"select submissionkey from ectd_submission where applicationkey = {0} and IsDeleted = 0))))";

                //rewrite sql statement to replace subquery by join to speed up the performance. need to check the result.
                var sqlQuery = "select distinct c.* from ectd_comment c join ectd_leafnode l " +
                                    "on c.leafnodekey = l.leafnodekey and l.IsDeleted = 0 " +
                                "join ectd_foldernode f on l.foldernodekey = f.foldernodekey and f.IsDeleted = 0 " +
                                "join ectd_nodegroup n on f.nodegroupkey = n.nodegroupkey and n.IsDeleted = 0 " +
                                $"join ectd_submission s on n.submissionkey = s.submissionkey and s.IsDeleted = 0 and s.applicationkey = {param0}";

                var tbl = TrsGenericConnectCommand1Param(nameof(Execute1DataSet), CommandType.Text, sqlQuery, QueryTableDelegate, "ectd_comment");

                ds.Tables.Add(tbl);

                return ds;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Execute1DataSet)} threw exception: ", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Execute1DataSet)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Get all Comments by Application Id and Id of parent Template
        /// </summary>
        /// <param name="param0">Application Id</param>
        /// <param name="param1">Id of parent Template</param>
        /// <returns>A collection of Comment objects</returns>
        public override DataSet Execute2DataSet(object param0, object param1)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(Execute2DataSet)}");
            try
            {
                string templateNodeId = $"{param1}";
                string condition = StringHelper.IsNullOrEmpty(templateNodeId, true) ?
                                    " and (n.parenttemplatenodeid is null or n.parenttemplatenodeid='') " :
                                    $" and n.parenttemplatenodeid='{templateNodeId}' ";

                var ds = new DataSet("ectd_comment");
                //var sqlQuery = string.Format(@"select c.* from ectd_comment c 
                //join ectd_leafnode l on l.leafnodekey = c.leafnodekey and l.IsDeleted = 0 and l.foldernodekey in 
                //	(select foldernodekey from ectd_foldernode where isdeleted = 0  and nodegroupkey in 
                //		(select nodegroupkey from ectd_nodegroup where isdeleted = 0 { 0}
                //and submissionkey in 
                //			(select submissionkey from ectd_submission where isdeleted = 0 and applicationkey = { 1 })
                //		)
                //	)
                //where c.IsDeleted = 0 ",condition,_appid);

                //rewrite sql statement to replace subquery by join to speed up the performance. need to check the result.
                var sqlQuery = "select distinct c.* from ectd_comment c join ectd_leafnode l on c.leafnodekey = l.leafnodekey " +
                                    "and l.IsDeleted = 0 " +
                                "join ectd_foldernode f on l.foldernodekey = f.foldernodekey and f.IsDeleted = 0 " +
                                $"join ectd_nodegroup n on f.nodegroupkey = n.nodegroupkey and n.IsDeleted = 0 {condition} " +
                                $"join ectd_submission s on n.submissionkey = s.submissionkey and s.IsDeleted = 0 and s.applicationkey = {param0}";

                var tbl = TrsGenericConnectCommand1Param(nameof(Execute2DataSet), CommandType.Text, sqlQuery, QueryTableDelegate, "ectd_comment");

                ds.Tables.Add(tbl);

                return ds;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Execute2DataSet)} threw exception: ", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Execute2DataSet)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Get all Comments by Application Id and Template Node Id
        /// </summary>
        /// <param name="applicationId">Application Id</param>
        /// <param name="templateNodeId">Template Node Id</param>
        /// <returns>A collection of Comment objects</returns>
        public DataSet GetCommentsbyTemplate(long applicationId, string templateNodeId)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(GetCommentsbyTemplate)}");
            try
            {
                string condition = StringHelper.IsNullOrEmpty(templateNodeId, true) ?
                                    " and (f.templatenodeid is null or f.templatenodeid='') " :
                                    $" and f.templatenodeid='{templateNodeId}' ";

                var ds = new DataSet("ectd_comment");
                //var sqlQuery = string.Format(@"select c.* from ectd_comment c 
                //join ectd_leafnode l on l.leafnodekey = c.leafnodekey and l.IsDeleted = 0 and l.foldernodekey in 
                //	(select foldernodekey from ectd_foldernode where isdeleted = 0 {0} and submissionkey in 
                //			(select submissionkey from ectd_submission where isdeleted = 0 and applicationkey = {1})
                //		)
                //where c.IsDeleted = 0 ",condition,_appid);

                //rewrite sql statement to replace subquery by join to speed up the performance. need to check the result.
                var sqlQuery = "select distinct c.* from ectd_comment c join ectd_leafnode l on c.leafnodekey = l.leafnodekey " +
                                    "and l.IsDeleted = 0 " +
                                $"join ectd_foldernode f on l.foldernodekey = f.foldernodekey and f.IsDeleted = 0 {condition} " +
                                $"join ectd_submission s on f.submissionkey = s.submissionkey and s.IsDeleted = 0 and s.applicationkey = {applicationId}";

                var tbl = TrsGenericConnectCommand1Param(nameof(GetCommentsbyTemplate), CommandType.Text, sqlQuery, QueryTableDelegate, "ectd_comment");

                ds.Tables.Add(tbl);

                return ds;
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(GetCommentsbyTemplate)} threw exception: ", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(GetCommentsbyTemplate)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Get Data for Comment and Comment status for a Report from specific user
        /// </summary>
        /// <param name="application"></param>
        /// <param name="sequence"></param>
        /// <param name="commentType"></param>
        /// <param name="status"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public override DataTable Query5DataTable(object param0, object param1, object param2, object param3, object param4)
        {
            if (Log.IsDebugEnabled) Log.Debug($"Inside {nameof(Query5DataTable)}");

            try
            {
                string application = (string)param0;
                string sequence = (string)param1;
                long commentTypeKey = (long)param2;
                long commentStatusKey = (long)param3;
                string userName = (string)param4;

                string sql = "SELECT CAST(ECTD_COMMENT.CommentKey AS VARCHAR(2000)) \"ID\"," +
                                "CAST(ECTD_SUBMISSION.SequenceNumber AS VARCHAR(2000)) \"Sequence\"," +
                                "CAST(ECTD_LEAFNODE.XlinkHref AS VARCHAR(2000)) \"Object Name\"," +
                                "CAST(ECTD_COMMENT.Subject AS VARCHAR(2000)) \"Subject\"," +
                                "CAST(ECTD_COMMENT.Note AS VARCHAR(2000)) \"Description\"," +
                                "CAST(STATUS1.StatusName AS VARCHAR(2000)) \"Comment Type\"," +
                                "CAST(STATUS2.StatusName AS VARCHAR(2000)) \"Comment Status\"," +
                                "CAST(ECTD_APPLICATION.ApplicationName AS VARCHAR(2000)) \"Application\"," +
                                "CAST(ECTD_COMMENT.ParentKey AS VARCHAR(2000)) \"ParentKey\"," +
                                "CAST(ECTD_COMMENT.CreationDate AS VARCHAR(2000)) \"Creation Date\"," +
                                "CAST(ISI_USERGROUP.UserName AS VARCHAR(2000)) \"Author\"," +
                                "CAST(ECTD_COMMENT.IsDeleted AS VARCHAR(2000)) \"IsDeleted\"," +
                                "CAST(ECTD_COMMENT.AccessType AS VARCHAR(2000)) \"AccessType\"" +
                                "FROM ECTD_APPLICATION " +
                                "INNER JOIN ECTD_SUBMISSION ON ECTD_APPLICATION.ApplicationKey = ECTD_SUBMISSION.ApplicationKey " +
                                "INNER JOIN ECTD_LEAFNODE ON ECTD_SUBMISSION.SubmissionKey = ECTD_LEAFNODE.SubmissionKey " +
                                "INNER JOIN ECTD_COMMENT ON ECTD_LEAFNODE.LeafNodeKey = ECTD_COMMENT.LeafNodeKey " +
                                "INNER JOIN ISI_USERGROUP ON ECTD_COMMENT.UserKey = ISI_USERGROUP.UserKey " +
                                "INNER JOIN ISI_CUSTOMSTATUS STATUS1 ON ECTD_COMMENT.CommentType = STATUS1.CustomStatusKey " +
                                "INNER JOIN ISI_CUSTOMSTATUS STATUS2 ON ECTD_COMMENT.CommentStatus = STATUS2.CustomStatusKey ";

                if (application.ToLower() != "[ALL]".ToLower())
                    sql += string.Format("WHERE ECTD_APPLICATION.ApplicationName = '{0}' ", application);

                if (sequence != "[ALL]")
                {
                    if (sql.IndexOf("WHERE") == -1)
                        sql += string.Format("WHERE ECTD_SUBMISSION.SequenceNumber = '{0}' ", sequence);
                    else
                        sql += string.Format("and ECTD_SUBMISSION.SequenceNumber = '{0}' ", sequence);
                }

                if (commentTypeKey != 0)
                {
                    if (sql.IndexOf("WHERE") == -1)
                        sql += string.Format("WHERE STATUS1.CustomStatusKey = {0}", commentTypeKey);
                    else
                        sql += string.Format("and status1.customstatuskey = {0} ", commentTypeKey);
                }

                if (commentStatusKey != 0)
                {
                    if (sql.IndexOf("WHERE") == -1)
                        sql += string.Format("WHERE STATUS2.CustomStatusKey = {0}", commentStatusKey);
                    else
                        sql += string.Format("and status2.customstatuskey = {0} ", commentStatusKey);
                }

                if (userName != "[ALL]")
                {
                    if (sql.IndexOf("WHERE") == -1)
                        sql += string.Format("WHERE ISI_USERGROUP.UserName = '{0}' ", userName);
                    else
                        sql += string.Format("and ISI_USERGROUP.UserName = '{0}' ", userName);
                }

                return TrsGenericConnectCommand1Param(nameof(Query5DataTable), CommandType.Text, sql, QueryTableDelegate, "Comments");
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled) Log.Error($"{nameof(Query5DataTable)} threw exception: ", ex);
                CustomErrorDetail errorDetail = new CustomErrorDetail($"{nameof(Query5DataTable)} threw exception", ex.Message);
                throw new WebFaultException<CustomErrorDetail>(errorDetail, HttpStatusCode.NotFound);
            }
        }
    }*/
}
