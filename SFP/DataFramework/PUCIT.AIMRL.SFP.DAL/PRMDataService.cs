﻿using System;
using PUCIT.AIMRL.SFP.UI.Common;
using System.Collections.Generic;
using System.Linq;

using System.Data.SqlClient;
using System.Data.Entity.Validation;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PUCIT.AIMRL.Common;
using PUCIT.AIMRL.SFP.DAL;
using PUCIT.AIMRL.SFP.Entities;
using PUCIT.AIMRL.SFP.Entities.DBEntities;
using PUCIT.AIMRL.Common.Logger;
using PUCIT.AIMRL.SFP.Entities.Enum;
using System.Data.Common;
using PUCIT.AIMRL.SFP.Entities.Security;
using PUCIT.AIMRL.SFP.Entities.DBEntities;
using System.Threading.Tasks;

namespace PUCIT.AIMRL.SFP.DAL
{
    public class PRMDataService
    {
        public static TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");

        #region Stored Procedures

        private const String SP_DBO_SAVESTUDENT = "dbo.SaveStudent";
        private const String SP_DBO_DEACTIVATESTUDENT = "[dbo].[DeactivateStudent]";
        private const String SP_DBO_SEARCHSTUDENTS = "dbo.SearchStudents";

        private const String SP_DBO_GETAPPROVERHERIRACHYS = "dbo.GetApproverHerirachy";


        #endregion

        public PRMDataService()
        {
            Database.SetInitializer<PRMDataContext>(null);
        }

        #region Roles
        public int SaveRole(Roles role, DateTime pActivityTime, int pActivityBy)
        {

            using (var ctx = new PRMDataContext())
            {
                string query = "execute sec.SaveRoles @0, @1, @2,@3,@4";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = role.Id },
                    new SqlParameter { ParameterName = "@1", Value = role.Name},
                    new SqlParameter { ParameterName = "@2", Value = role.Description},
                    new SqlParameter { ParameterName = "@3", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@4", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();
                return data;
            }
        }
        public bool EnableDisableRole(int pRoleID, Boolean pIsActiv, DateTime pActivityTime, int pActivityBy)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute sec.EnableDisableRole @0, @1, @2, @3";

                var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = pRoleID},
                     new SqlParameter { ParameterName = "@1", Value = pIsActiv},
                    new SqlParameter { ParameterName = "@2", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@3", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();

                return true;
            }
        }
        public List<Roles> GetAllRoles()
        {
            using (var db = new PRMDataContext())
            {
                return db.Roles.ToList();
            }
        }

        public List<int> GetRolesByUserID(int pUserID)
        {
            using (var db = new PRMDataContext())
            {
                var result = db.UserRoles.Where(p => p.UserId == pUserID).Select(p => p.RoleId).ToList();
                return result;
            }
        }

        public int SaveUserRoleMapping(int pUserID, List<int> pRoles)
        {
            using (var db = new PRMDataContext())
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("ID");

                foreach (var p in pRoles)
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(p);
                }

                string query = "execute sec.SaveUserRoleMapping @0, @1";

                var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = pUserID},
                     new SqlParameter { ParameterName = "@1", Value = dt, SqlDbType = SqlDbType.Structured, TypeName = "dbo.ArrayInt" },
                };

                var data = db.Database.SqlQuery<int>(query, args).FirstOrDefault();

                return data;
            }
        }

        #endregion

        #region Permission
        public int SavePermission(PermissionsWithRoleID per, DateTime pActivityTime, int pActivityBy)
        {

            using (var ctx = new PRMDataContext())
            {
                string query = "execute sec.SavePermission @0, @1,@2,@3,@4";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = per.Id},
                    new SqlParameter { ParameterName = "@1", Value = per.Name},
                    new SqlParameter { ParameterName = "@2", Value = per.Description},
                    new SqlParameter { ParameterName = "@3", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@4", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();
                return data;
            }
        }
        public bool EnableDisablePermission(int pPermissionID, Boolean pIsActiv, DateTime pActivityTime, int pActivityBy)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute sec.EnableDisablePermission @0, @1, @2, @3";

                var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = pPermissionID},
                     new SqlParameter { ParameterName = "@1", Value = pIsActiv},
                    new SqlParameter { ParameterName = "@2", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@3", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();

                return true;
            }

        }
        public List<Permissions> GetAllPermissions()
        {
            using (var db = new PRMDataContext())
            {
                string query = "execute sec.GetAllPermissions ";
                var list = db.Database.SqlQuery<Permissions>(query).ToList();
                return list;
            }
        }

        public List<int> GetPermissionsByRoleID(int pRoleID)
        {
            using (var db = new PRMDataContext())
            {
                var result = db.PermissionsMapping.Where(p => p.RoleId == pRoleID).Select(p => p.PermissionId).ToList();
                return result;
            }
        }

        public int SaveRolePermissionMapping(int pRoleID, List<int> pPermissionsList)
        {
            using (var db = new PRMDataContext())
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("ID");

                foreach (var p in pPermissionsList)
                {
                    DataRow row = dt.NewRow();
                    dt.Rows.Add(p);
                }

                string query = "execute sec.SaveRolePermissionMapping @0, @1";

                var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = pRoleID},
                     new SqlParameter { ParameterName = "@1", Value = dt, SqlDbType = SqlDbType.Structured, TypeName = "dbo.ArrayInt" },
                };

                var data = db.Database.SqlQuery<int>(query, args).FirstOrDefault();

                return data;
            }
        }

        #endregion

        #region Users
        public int SaveUsers(User u, DateTime pActivityTime, int pActivityBy)
        {

            using (var ctx = new PRMDataContext())
            {
                string query = "execute sec.SaveUsers @0, @1, @2, @3,@4, @5,@6";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = u.UserId },
                    new SqlParameter { ParameterName = "@1", Value = u.Login},
                    new SqlParameter { ParameterName = "@2", Value = "123" },
                    new SqlParameter { ParameterName = "@3", Value = u.Name },
                    new SqlParameter { ParameterName = "@4", Value = u.Email },
                    new SqlParameter { ParameterName = "@5", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@6", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();
                return data;
            }
        }
        public bool EnableDisableUser(int pUserID, Boolean pIsActiv, DateTime pActivityTime, int pActivityBy)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute sec.EnableDisableUser @0, @1, @2, @3";

                var args = new DbParameter[] {
                     new SqlParameter { ParameterName = "@0", Value = pUserID},
                     new SqlParameter { ParameterName = "@1", Value = pIsActiv},
                    new SqlParameter { ParameterName = "@2", Value = pActivityTime.YYYYMMDD()},
                    new SqlParameter { ParameterName = "@3", Value = pActivityBy}
                };

                var data = ctx.Database.SqlQuery<int>(query, args).FirstOrDefault();

                return true;
            }
        }
        public List<User> GetAllUsers()
        {
            using (var db = new PRMDataContext())
            {
                return db.Users.ToList();
            }
        }
        public User GetUserByEmail(string emailAddress)
        {
            using (var db = new PRMDataContext())
            {
                var result = (from data in db.Users
                              where data.Email == emailAddress && data.IsActive == true
                              select data).FirstOrDefault();
                return result;
            }
        }

        public UserSearchResult SearchUsers(UserSearchParam entity)
        {
            using (var ctx = new PRMDataContext())
            {
                UserSearchResult result = new Entities.DBEntities.UserSearchResult();

                string query = "execute sec.SearchUsers @0, @1, @2,@3";

                var cmd = ctx.Database.Connection.CreateCommand();
                cmd.CommandText = query;

                cmd.Parameters.Add(new SqlParameter { ParameterName = "@0", Value = entity.TextToSearch });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = entity.IsActive });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = entity.PageSize });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = entity.PageIndex });

                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();

                result.ResultCount = ((IObjectContextAdapter)ctx)
                   .ObjectContext
                   .Translate<int>(reader).FirstOrDefault();

                reader.NextResult();
                result.Result = ((IObjectContextAdapter)ctx)
                                .ObjectContext
                                .Translate<UserSearchResultObj>(reader).ToList();

                return result;
            }
        }

        public List<UserSmallDTO> SearchUser(string key)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute sec.SearchUserForAutoComplete @0";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = key }
                };

                var list = ctx.Database.SqlQuery<UserSmallDTO>(query, args).ToList();
                return list;
            }
        }
        public LoginHistorySearchResult SearchLoginHistory(LoginHistorySearchParam entity)
        {
            using (var ctx = new PRMDataContext())
            {
                LoginHistorySearchResult result = new Entities.DBEntities.LoginHistorySearchResult();

                string query = "execute sec.SearchLoginHistory @0, @1, @2,@3,@4,@5";

                var cmd = ctx.Database.Connection.CreateCommand();
                cmd.CommandText = query;

                cmd.Parameters.Add(new SqlParameter { ParameterName = "@0", Value = entity.Login });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = entity.MachineIp });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = entity.SDate });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = entity.EDate });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@4", Value = entity.PageSize });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@5", Value = entity.PageIndex });

                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();

                result.ResultCount = ((IObjectContextAdapter)ctx)
                   .ObjectContext
                   .Translate<int>(reader).FirstOrDefault();

                reader.NextResult();
                result.Result = ((IObjectContextAdapter)ctx)
                                .ObjectContext
                                .Translate<LoginHistory>(reader).ToList();
                foreach (var d in result.Result)
                {
                    d.LoginTime = d.LoginTime.ToTimeZoneTime(tzi);
                }
                return result;
            }
        }
        public ForgotPasswordSearchResult SearchForgotPasswordLog(ForgotPasswordSearchParam entity)
        {
            using (var ctx = new PRMDataContext())
            {
                ForgotPasswordSearchResult result = new ForgotPasswordSearchResult();

                string query = "execute sec.SearchForgotPasswordLog @0, @1, @2,@3,@4";

                var cmd = ctx.Database.Connection.CreateCommand();
                cmd.CommandText = query;

                cmd.Parameters.Add(new SqlParameter { ParameterName = "@0", Value = entity.Login });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = entity.SDate });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = entity.EDate });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = entity.PageSize });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@4", Value = entity.PageIndex });

                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();

                result.ResultCount = ((IObjectContextAdapter)ctx)
                   .ObjectContext
                   .Translate<int>(reader).FirstOrDefault();

                reader.NextResult();
                result.Result = ((IObjectContextAdapter)ctx)
                                .ObjectContext
                                .Translate<ForgotPasswordLogDTO>(reader).ToList();
                foreach (var d in result.Result)
                {
                    d.EntyDate = d.EntyDate.ToTimeZoneTime(tzi);
                }
                return result;
            }
        }

        //public int changePassword(PasswordEntity pass)
        //{
        //    var username = SessionManager.GetUserLogin();

        //    using (var db = new PRMDataContext())
        //    {
        //        var query = db.Users.Where(x => (x.Login == username) && (x.Password == pass.CurrentPassword)).FirstOrDefault();

        //        if (query != null)
        //        {
        //            query.Password = pass.NewPassword;

        //            db.SaveChanges();
        //            return 1;

        //        }
        //        else return 0;

        //    }
        //}
        public Boolean UpdatePassword(String emailAddress_login, String currentPassword, String Newpassword, int pModifiedBy, DateTime pModifiedOn, Boolean IsChangePassword)
        {
            try
            {
                using (var db = new PRMDataContext())
                {
                    var query = String.Format("Execute sec.UpdatePassword '{0}','{1}','{2}','{3}','{4}','{5}'", emailAddress_login, currentPassword, Newpassword, pModifiedOn, pModifiedBy, IsChangePassword);
                    var result = db.Database.SqlQuery<Boolean>(query).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int resetPassword(String emailAddress, String password)
        {
            using (var db = new PRMDataContext())
            {

                var query = (from data in db.Users
                             where data.Email == emailAddress
                             select data).SingleOrDefault();

                query.Password = password;

                db.SaveChanges();
                return 1;
            }
        }

        public String UpdateResetToken(String emailAddress_login, String pGuid, String pIPAddress, DateTime pCurrTime, string pUrl)
        {
            try
            {
                using (var db = new PRMDataContext())
                {
                    var query = String.Format("Execute sec.UpdateResetPasswordToken '{0}','{1}','{2}','{3}','{4}'", emailAddress_login, pGuid, pIPAddress, pCurrTime, pUrl);
                    var result = db.Database.SqlQuery<String>(query).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public Boolean IsValidResetToken(String pReset_Token)
        {
            try
            {
                using (var db = new PRMDataContext())
                {
                    var query = String.Format("Execute sec.IsValidResetToken '{0}'", pReset_Token);
                    var result = db.Database.SqlQuery<Boolean>(query).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public SecUserDTO ValidateUserSP(String pLogin, String pPassword, String pEmail, DateTime pCurrTime, String pMachineIP, Boolean pIgnorePassword, String pLoggerLoginID)
        {

            using (var ctx = new PRMDataContext())
            {
                string query = "execute sec.ValidateUser @0, @1, @2, @3,@4,@5,@6";

                var cmd = ctx.Database.Connection.CreateCommand();
                cmd.CommandText = query;

                cmd.Parameters.Add(new SqlParameter { ParameterName = "@0", Value = pLogin });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@1", Value = pPassword });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@2", Value = pCurrTime.YYYYMMDD() });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@3", Value = pMachineIP });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@4", Value = pIgnorePassword });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@5", Value = pLoggerLoginID });
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@6", Value = pEmail });

                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();


                // Read User from the first result set
                var user = ((IObjectContextAdapter)ctx)
                    .ObjectContext
                    .Translate<User>(reader).FirstOrDefault();

                if (user != null)
                {
                    var secUserForSession = new SecUserDTO();
                    if (user.IsActive == false || user.IsDisabledForLogin == true)
                    {
                        secUserForSession.IsActive = user.IsActive;
                        secUserForSession.IsDisabledForLogin = user.IsDisabledForLogin;
                    }
                    else
                    {
                        reader.NextResult();
                        var roles = ((IObjectContextAdapter)ctx)
                            .ObjectContext
                            .Translate<Roles>(reader).ToList();

                        reader.NextResult();
                        var permissions = ((IObjectContextAdapter)ctx)
                            .ObjectContext
                            .Translate<PermissionsWithRoleID>(reader).ToList();

                        reader.Close();

                        secUserForSession.Login = user.Login;
                        secUserForSession.UserFullName = user.Name;
                        secUserForSession.UserId = user.UserId;
                        secUserForSession.Email = user.Email;
                        secUserForSession.IsActive = user.IsActive;
                        secUserForSession.Permissions = new List<string>();
                        secUserForSession.Roles = new List<string>();
                        

                        foreach (var r in roles)
                        {
                            secUserForSession.Roles.Add(r.Name);
                        }

                        foreach (var p in permissions)
                        {
                            secUserForSession.Permissions.Add(p.Name.ToUpper());
                        }
                    }
                    return secUserForSession;
                }

                reader.Close();
                return null;
            }
        }

        public List<String> GetRolePermissionById(int pUserID, out List<String> pRoles)
        {
            using (var ctx = new PRMDataContext())
            {
                string query = "execute sec.GetRolePermissionById @0";
                var cmd = ctx.Database.Connection.CreateCommand();
                cmd.CommandText = query;

                cmd.Parameters.Add(new SqlParameter { ParameterName = "@0", Value = pUserID });

                ctx.Database.Connection.Open();
                var reader = cmd.ExecuteReader();


                var roles = ((IObjectContextAdapter)ctx)
                        .ObjectContext
                        .Translate<Roles>(reader).ToList();

                reader.NextResult();
                var permissions = ((IObjectContextAdapter)ctx)
                    .ObjectContext
                    .Translate<PermissionsWithRoleID>(reader).ToList();

                reader.Close();

                var rolesList = new List<String>();
                var permList = new List<String>();

                foreach (var r in roles)
                {
                    rolesList.Add(r.Name);
                }

                foreach (var p in permissions)
                {
                    permList.Add(p.Name.ToUpper());
                }

                pRoles = rolesList;

                return permList;
            }
        }
        #endregion

        #region EmailRequests

        public List<EmailRequest> GetEmailRequestsForProcessing()
        {

            try
            {
                using (var ctx = new PRMDataContext())
                {
                    var list = ctx.EmailRequests.Where(p => p.EmailRequestStatus == (int)EmailRequestStatus.Pending).OrderBy(p => p.EmailRequestID).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                return new List<EmailRequest>();
            }
        }
        public void ProcessEmailRequests(List<long> list)
        {
            using (var ctx = new PRMDataContext())
            {
                foreach (var id in list)
                {
                    var dto = new EmailRequest() { EmailRequestID = id, EmailRequestStatus = (int)EmailRequestStatus.Processed };
                    ctx.EmailRequests.Attach(dto);
                    var entry = ctx.Entry(dto);
                    entry.State = EntityState.Unchanged;
                    entry.Property(e => e.EmailRequestStatus).IsModified = true;
                }

                ctx.SaveChanges();

            }
        }
        public List<EmailRequest> GetEmailRequestsByUniqueID(PRMDataContext ctx, String pUniqueID)
        {
            try
            {
                var data = new List<EmailRequest>();
                string query = "execute dbo.GetEmailRequestsByUniqueID @0";
                var args = new DbParameter[] {
                    new SqlParameter { ParameterName = "@0", Value = pUniqueID}
                    };

                if (ctx != null)
                {
                    data = ctx.Database.SqlQuery<EmailRequest>(query, args).ToList();
                }
                else
                {
                    using (ctx = new PRMDataContext())
                    {
                        data = ctx.Database.SqlQuery<EmailRequest>(query, args).ToList();

                    }
                }
                return data;
            }
            catch (Exception)
            {
                return new List<EmailRequest>();
            }
        }
        private void ProcessEmails(List<EmailRequest> pEmails)
        {
            System.Threading.Thread th = new System.Threading.Thread(delegate(Object a)
            {

                try
                {
                    var pEmailRequests = (List<EmailRequest>)a;
                    var list = new List<long>();
                    foreach (var email in pEmailRequests)
                    {
                        //EmailHandler.SendEmail(email.EmailTo, email.Subject, email.MessageBody);

                        GlobalDataManager._emailhandler.SendEmail(new EmailMessageParam()
                        {
                            ToIDs = email.EmailTo,
                            Subject = email.Subject,
                            Body = email.MessageBody
                        });

                        list.Add(email.EmailRequestID);
                    }
                    ProcessEmailRequests(list);
                }
                catch (Exception)
                {
                }
            });

            th.Start(pEmails);

        }

        #endregion


        




    }
}
