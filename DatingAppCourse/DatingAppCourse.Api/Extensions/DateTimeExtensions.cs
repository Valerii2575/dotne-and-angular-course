namespace DatingAppCourse.Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly dateOfBirth)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = today.Year - dateOfBirth.Year;
            // Adjust for the case where the birthday hasn't occurred yet this year
            if (dateOfBirth > today.AddYears(-age)) age--;
            return age;
        }
    }
}
