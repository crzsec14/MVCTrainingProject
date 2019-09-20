using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingSystem.Data.Helpers
{
    public class SweetAlertHelper
    {
        public static string ShowMessage(string messageCaption, string messageContent, SweetAlertMessageType messageType)
        {
            return $"swal('{messageCaption}','{messageContent}','{messageType}');";
        }
        public static string Delete(string messageCaption, string messageContent, SweetAlertMessageType messageType)
        {
            return $"swal('{messageCaption}','{messageContent}','{messageType}','buttons:true', 'dangerMode:true');";
        }
    }

    public enum SweetAlertMessageType
    {
        warning, error, success, info
    }
}
