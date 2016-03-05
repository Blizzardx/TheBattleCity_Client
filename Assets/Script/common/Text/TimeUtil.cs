using System;

    public class TimeUtil
    {
        public static DateTime GetDateTime(long timestamp)
        {
            var start = new DateTime(2016, 1, 1, 0, 0, 0, 0);
            return start.AddSeconds(timestamp/1000);
        }
        public static string TimeFormat(DateTime time)
        {            
            return time.Year + "年" + time.Month + "月" + time.Day + "日 " + (time.Hour < 10 ? ("0" + time.Hour) : time.Hour.ToString()) + ":" + (time.Minute < 10 ? ("0" + time.Minute) : time.Minute.ToString()) + ":" + (time.Second < 10 ? ("0" + time.Second) : time.Second.ToString());
        }
        public static string TimeFormat(long timestamp)
        {
            return TimeFormat(GetDateTime(timestamp));
        }
		public static string DateFormat (DateTime time)
		{
			return time.Month + "月" + time.Day + "日";
		}
		public static string DateFormat (long timestamp)
		{
			return DateFormat(GetDateTime(timestamp));
		}
        public static long Now()
        {
            TimeSpan ts = DateTime.Now - new DateTime(2016, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
          //return  ConvertDateTimeInt(DateTime.UtcNow);   
        }
        public static long ConvertDateTimeInt(System.DateTime time)
        {
            //double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970,1,1,0,0,0,0));
            //intResult = (time- startTime).TotalMilliseconds;
            long t = (time.Ticks - startTime.Ticks)/10000;            //除10000调整为13位
            return t;
        }
    }


