using System;
using System.Configuration;
using System.IO;
using System.Net;

namespace OmPrabha
{
    static public class BLSMS
    {

       
        public static string SendSMS2(string User, string password, string senderid, string Mobile_Number, string Message)
        {
            // use the API URL here  
            string strUrl = "http://wiztechsms.com/http-api.php?username=" + User + "&password=" + password + "&senderid=" + senderid + "&route=2&number=" + Mobile_Number + "&message=" + Message;        // Create a request object  
            WebRequest request = HttpWebRequest.Create(strUrl);
            // Get the response back  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
        }
        static public void SendSMS(string Mobile, string Message, string TempId)
        {
            try
            {
                string SMSAPI = ConfigurationSettings.AppSettings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile);
                SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);
                SMSAPI = SMSAPI.Replace("[TempId]", TempId);
                SMSAPI = SMSAPI.Replace("[Date]", DateTime.Now.ToString());
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI, false));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
            }
            catch (Exception ex)
            {
            }
        }

        static public string Booking(string BookingNo, string BookingAmount, string AssociateName, string Plot, string Plotamout)
        {

            string Message = ConfigurationSettings.AppSettings["Booking"].ToString();


            Message = Message.Replace("[AssociateName]", AssociateName);
            Message = Message.Replace("[BookingNo]", BookingNo);
            Message = Message.Replace("[Plot]", Plot);
            Message = Message.Replace("[BookingAmt]", BookingAmount);
            Message = Message.Replace("[BookingAmt]", BookingAmount);
            Message = Message.Replace("[PlotAmount]", Plotamout);
            return Message;


        }
        static public string HouseBooking(string BookingNo, string BookingAmount, string AssociateName, string Plot)
        {

            string Message = ConfigurationSettings.AppSettings["HouseBooking"].ToString();


            Message = Message.Replace("[AssociateName]", AssociateName);
            Message = Message.Replace("[BookingNo]", BookingNo);
            Message = Message.Replace("[Plot]", Plot);
            Message = Message.Replace("[BookingAmt]", BookingAmount);
            return Message;


        }
        static public string AssociateRegistration(string Name, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["RegistrationAssociate"].ToString();

            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Password]", Password);
            return Message;
        }
        static public string CustomerRegistration(string Name, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["CustomerRegistration"].ToString();

            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Password]", Password);
            return Message;
        }
        static public string PlotAllotment(string name, string Plot, string amt)
        {
            string Message = ConfigurationSettings.AppSettings["PlotAllotment"].ToString();

            Message = Message.Replace("[Name]", name);
            Message = Message.Replace("[Plot]", Plot);
            Message = Message.Replace("[amt]", amt);
            return Message;
        }

        static public string EMIPayment(string name, string Plot, string bookno, string instno, string amt)
        {
            string Message = ConfigurationSettings.AppSettings["EMIPayment"].ToString();

            Message = Message.Replace("[Name]", name);
            Message = Message.Replace("[Plot]", Plot);
            Message = Message.Replace("[amt]", amt);
            Message = Message.Replace("[BookingNo]", bookno);
            //Message = Message.Replace("[InstallmentNo]", instno);
            return Message;
        }



        static public string ForgetPassword(string FirstName, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["ForgetPassword"].ToString();

            Message = Message.Replace("[FirstName]", FirstName);
            Message = Message.Replace("[Password]", Password);

            return Message;
        }


        static public string Registration(string MemberName, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["REGISTRATION"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Password]", Password);

            return Message;
        }
        static public string KycUpload(string FirstName)
        {
            string Message = ConfigurationSettings.AppSettings["KycUpload"].ToString();
            Message = Message.Replace("[FirstName]", FirstName);
            return Message;
        }



    }
}
