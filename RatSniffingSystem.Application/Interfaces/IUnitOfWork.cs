using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Interfaces;

public interface IUnitOfWork
{
    IRatService<Rat, RatDto,CreateRatDto, UpdateRatDto> Rats { get; }
    ISessionService<Session, SessionDto, CreateSessionDto, UpdateSessionDto> Sessions { get; }

    IInterventionService<Intervention,InterventionDto, CreateInterventionDto> Interventions { get; }
    IBehaviorLogService<BehaviorLog, BehaviorLogDto, CreateBehaviorLogDto> BehaviorLogs { get; }
    IExperimentalNoteService<ExperimentalNote, ExperimentalNoteDto, CreateExperimentalNoteDto> ExperimentalNotes { get; }
    IFoodIntakeLogService<FoodIntakeLog,FoodIntakeLogDto, CreateFoodIntakeLogDto> FoodIntakeLogs { get; }
    ITrainerService<Trainer,TrainerDto, CreateTrainerDto, UpdateTrainerDto> Trainers { get; }
    IOdorService<Odor,OdorDto, CreateOdorDto> Odors { get; }
    IRatWeightService<RatWeight,RatWeightDto, CreateRatWeightDto> RatWeights { get; }
    ITransferTestService<TransferTest,TransferTestDto, CreateTransferTestDto> TransferTests { get; }
    ITrialOdorService<TrialOdor,TrialOdorDto, CreateTrialOdorDto> TrialOdors { get; }
    ITrialService<Trial,TrialDto, CreateTrialDto> Trials { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}