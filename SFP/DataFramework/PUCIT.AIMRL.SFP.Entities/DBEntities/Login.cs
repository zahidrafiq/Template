﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PUCIT.AIMRL.SFP.Entities.DBEntities
{

    [Table("sec.Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public Boolean IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Boolean IsDisabledForLogin { get; set; }

    }


    public class UserSearchParam
    {
        public String TextToSearch { get; set; }
        public int IsActive { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    public class UserSmallDTO
    {
        public int UserId { get; set; }
        public String Login { get; set; }
        public String Name { get; set; }
    }

    public class UserSearchResultObj : UserSmallDTO
    {
        public Boolean IsActive { get; set; }
    }
    
    public class UserSearchResult
    {
        public int ResultCount { get; set; }
        public List<UserSearchResultObj> Result { get; set; }
    }

    public class LoginHistorySearchParam
    {
        public LoginHistorySearchParam()
        {
            SDate = new DateTime(1900, 1, 1);
            EDate = DateTime.MaxValue;
            Login = "";
            MachineIp = "";
            PageSize = 10;
            PageIndex = 0;
        }
        public String Login { get; set; }
        public String MachineIp { get; set; }
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
    public class LoginHistorySearchResult
    {
        public int ResultCount { get; set; }
        public List<LoginHistory> Result { get; set; }
    }
}
