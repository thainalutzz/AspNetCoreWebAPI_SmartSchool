using System;

namespace SmartSchool.WebAPI.Helpers
{
    public static class DateTimeExtentions
    {
        public static int GetCurrentAge(this DateTime dateTime)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTime.Year;

            if(currentDate < dateTime.AddYears(age))
                age--;

                return age;            
        }
    }
}