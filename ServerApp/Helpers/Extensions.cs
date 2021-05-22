using System;
namespace ServerApp.Helpers
{
    public static class Extensions
    {
        public static int CalculateAge ( this DateTime birtDate){

            var age =0; 

            age= DateTime.Now.Year - birtDate.Year;

            if(DateTime.Now.DayOfYear < birtDate.DayOfYear)
                age-=1;

            return age ;
        }   
    }
}