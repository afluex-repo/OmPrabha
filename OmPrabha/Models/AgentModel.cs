using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmPrabha.Models
{
    public class AgentModel
    {
        public string AllBusinessLeft { get; set; }
        public string AllBusinessRight { get; set; }
        public string Fk_UserId { get; set; }
        public string SessionId { get; set; }
        public string ParentId { get; set; }
        public string prefixText { get; set; }

        public string Leg { get; set; }
        public string Status { get; set; }
        public string MemberName { get; set; }
        public string ProductRightBusiness { get; set; }
        public string ProductLeftBusiness { get; set; }
        public string LoginId { get; set; }
        public string ParentLoginId { get; set; }
        public string BlockStatus { get; set; }
        public string CssClass { get; set; }
        public string Href { get; set; }
        public string SponsorId { get; set; }
        public string SelfBusiness { get; set; }
        public string TeamBusiness { get; set; }
        public string JoiningDate { get; set; }
        public string ProductName { get; set; }
        public string MainId { get; set; }

        public string AllLeg1 { get; set; }
        public string AllLeg2 { get; set; }
        public string PermanentLeg1 { get; set; }
        public string PermanentLeg2 { get; set; }
        public string InactiveLeft { get; set; }
        public string InactiveRight { get; set; }

        public string Spillby { get; set; }
        public string PCountLeg1 { get; set; }
        public string PCountLeg2 { get; set; }
        public string BalanceLeft { get; set; }
        public string BalanceRight { get; set; }
        public string PaidLeg1 { get; set; }
        public string PaidLeg2 { get; set; }

        public string ActivationDate { get; set; }
 public string ProfilePic { get; set; }
        public string Gender { get; set; }
    }
}