using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    /// <summary>
    /// نوع کاربر
    /// </summary>
    public enum UserType
    {
        [Description("سوپر ادمین")]
        SuperAdmin = 0,

        [Description("سوپروایزر")]
        Supervisor = 1,

        [Description("شاپر")]
        Shopper = 2,

        //[Description("مسئول پیک")]
        //HeadOfDelivery = 3,

        [Description("پیک")]
        Delivery = 4,

        [Description("سایر")]
        Other = 5
    }
}
