using Forma.Domain.Entities;

namespace Forma.Application.Services;

public class HydrationService
{
    public double CalculateDailyTargetMl(User user, Workout? todaysWorkout)
    {
        double baseTarget = user.Weight * 35;

        if (todaysWorkout?.DurationMinutes is double minutes)
        {
            double hours = minutes / 60.0;
            double extraPerHour = 500;
            baseTarget += hours * extraPerHour;
        }

        return baseTarget;
    }
}
