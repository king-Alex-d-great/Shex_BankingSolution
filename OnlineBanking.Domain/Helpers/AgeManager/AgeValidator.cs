using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Domain.Helpers.AgeManager
{
    public static class AgeValidator
    {
        public static (bool isAgeMinimumAge, bool isAgeMaximumAge) isAgeValid(DateTime userDob)
        {
            int minimumAge = default;
            var maximumAge = 100;
            var currentYear = DateTime.Now.Year;
            int differenceInAge = currentYear - userDob.Year;
            return (differenceInAge > minimumAge, differenceInAge < maximumAge);
        }
    }
}
