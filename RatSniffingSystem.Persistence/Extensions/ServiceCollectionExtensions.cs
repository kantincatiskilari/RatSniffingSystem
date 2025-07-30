using Microsoft.Extensions.DependencyInjection;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Persistence.Persistence;
using RatSniffingSystem.Persistence.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IRatService<Rat, RatDto, CreateRatDto, UpdateRatDto>, RatService>();
        services.AddScoped<IBehaviorLogService<BehaviorLog, BehaviorLogDto, CreateBehaviorLogDto>, BehaviorLogService>();
        services.AddScoped<IExperimentalNoteService<ExperimentalNote, ExperimentalNoteDto, CreateExperimentalNoteDto>, ExperimentalNoteService>();
        services.AddScoped<IFoodIntakeLogService<FoodIntakeLog, FoodIntakeLogDto, CreateFoodIntakeLogDto>, FoodIntakeLogService>();
        services.AddScoped<IInterventionService<Intervention, InterventionDto, CreateInterventionDto>, InterventionService>();
        services.AddScoped<IOdorService<Odor, OdorDto, CreateOdorDto>, OdorService>();
        services.AddScoped<IRatWeightService<RatWeight, RatWeightDto, CreateRatWeightDto>, RatWeightService>();
        services.AddScoped<ISessionService<Session, SessionDto, CreateSessionDto, UpdateSessionDto>, SessionService>();
        services.AddScoped<ITrainerService<Trainer, TrainerDto, CreateTrainerDto, UpdateTrainerDto>, TrainerService>();
        services.AddScoped<ITransferTestService<TransferTest, TransferTestDto, CreateTransferTestDto>, TransferTestService>();
        services.AddScoped<ITrialOdorService<TrialOdor, TrialOdorDto, CreateTrialOdorDto>, TrialOdorService>();
        services.AddScoped<ITrialService<Trial, TrialDto, CreateTrialDto>, TrialService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
