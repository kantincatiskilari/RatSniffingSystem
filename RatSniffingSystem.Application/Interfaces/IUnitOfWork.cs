using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Contracts.Interfaces;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Interfaces;

public interface IUnitOfWork
{
    IRatService<RatDto,CreateRatDto, UpdateRatDto> Rats { get; }
    ISessionService<SessionDto, CreateSessionDto, UpdateSessionDto> Sessions { get; }

    IInterventionService<InterventionDto, CreateInterventionDto> Interventions { get; }
    IBehaviorLogService<BehaviorLogDto, CreateBehaviorLogDto> BehaviorLogs { get; }
    IExperimentalNoteService<ExperimentalNoteDto, CreateExperimentalNoteDto> ExperimentalNotes { get; }
    IFoodIntakeLogService<FoodIntakeLogDto, CreateFoodIntakeLogDto> FoodIntakeLogs { get; }
    ITrainerService<TrainerDto, CreateTrainerDto, UpdateTrainerDto> Trainers { get; }
    IOdorService<OdorDto, CreateOdorDto> Odors { get; }
    IRatWeightService<RatWeightDto, CreateRatWeightDto> RatWeights { get; }
    ITransferTestService<TransferTestDto, CreateTransferTestDto> TransferTests { get; }
    ITrialOdorService<TrialOdorDto, CreateTrialOdorDto> TrialOdors { get; }
    ITrialService<TrialDto, CreateTrialDto> Trials { get; }

    Task<int> SaveChangesAsync();
}