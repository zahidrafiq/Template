using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUCIT.AIMRL.SFP.Entities.DBEntities
{
    [Table("sec.Project")]
    public class Project
    {
      [Key]
      public int ProjectId { get; set; }
      public String ProjectTitle{get; set; }
      public String Description { get; set; }
      public int Type { get; set; } 
      public int TotalUpVote { get; set; }
      public int TotalDownVote { get; set; }
      public int BidCount { get; set; }
      public String FileName { get; set; }
      public int UserId { get; set; }
      public DateTime CreatedOn { set; get; }
      public DateTime? ModifiedOn { get; set; }
      public Boolean IsActive { set; get; }
      public int ProjectState { get; set; }
      [NotMapped]
      public string InitiatedBy { set; get; }
      
    }
    public class projectIdea : Project
    {       
        public List<String>   MemberList { set; get; }
        public List<int> SectionList { set; get; }
    }
}