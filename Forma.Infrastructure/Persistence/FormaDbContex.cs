using Forma.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Forma.Infrastructure.Persistence;

public class FormaDbContext : DbContext
{
    public FormaDbContext(DbContextOptions<FormaDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<WorkoutExercise> WorkoutExercises => Set<WorkoutExercise>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<ExerciseMuscleInvolvement> ExerciseMuscleInvolvements => Set<ExerciseMuscleInvolvement>();
    public DbSet<Set> Sets => Set<Set>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<FoodItem> FoodItems => Set<FoodItem>();
    public DbSet<MealFoodItem> MealFoodItems => Set<MealFoodItem>();
    public DbSet<WaterIntake> WaterIntakes => Set<WaterIntake>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Workout.DurationMinutes is computed in C#, not a real column
        modelBuilder.Entity<Workout>()
            .Ignore(w => w.DurationMinutes);

        // Workout -> WorkoutExercise (1:N)
        modelBuilder.Entity<WorkoutExercise>()
            .HasOne(we => we.Workout)
            .WithMany(w => w.WorkoutExercises)
            .HasForeignKey(we => we.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);

        // WorkoutExercise -> Set (1:N)
        modelBuilder.Entity<Set>()
            .HasOne(s => s.WorkoutExercise)
            .WithMany(we => we.Sets)
            .HasForeignKey(s => s.WorkoutExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Exercise -> WorkoutExercise (N:1) — keep history if Exercise is deleted
        modelBuilder.Entity<WorkoutExercise>()
            .HasOne(we => we.Exercise)
            .WithMany(e => e.WorkoutExercises)
            .HasForeignKey(we => we.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Exercise -> ExerciseMuscleInvolvement (1:N)
        modelBuilder.Entity<ExerciseMuscleInvolvement>()
            .HasOne(emi => emi.Exercise)
            .WithMany(e => e.MuscleInvolvements)
            .HasForeignKey(emi => emi.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Meal -> MealFoodItem (1:N)
        modelBuilder.Entity<MealFoodItem>()
            .HasOne(mfi => mfi.Meal)
            .WithMany(m => m.MealFoodItems)
            .HasForeignKey(mfi => mfi.MealId)
            .OnDelete(DeleteBehavior.Cascade);

        // FoodItem -> MealFoodItem (N:1) — keep history if FoodItem is deleted
        modelBuilder.Entity<MealFoodItem>()
            .HasOne(mfi => mfi.FoodItem)
            .WithMany(fi => fi.MealFoodItems)
            .HasForeignKey(mfi => mfi.FoodItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}