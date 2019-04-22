using PUCIT.AIMRL.SFP.Entities.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUCIT.AIMRL.SFP.Entities.DBEntities
{
    public class Request
    {
        public int RequestId { set; get; }
        public int ProjectId { set; get; }
        public string RequestBody { set; get; }
        public int SendBy { set; get; }
        public string Status { set; get; }
        public string Type { set; get; }
        public DateTime CreatedOn { set; get; }
        public int RequestCount { set; get; }
    }
}
public class Notification : Request
{
    public string NotificationBody { get; set; }
    public string Title { get; set; }
    public int NotificationId { get; set; }

}