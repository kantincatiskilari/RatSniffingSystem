using System.Threading;
using System.Threading.Tasks;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Persistence.Context;

namespace RatSniffingSystem.Persistence.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(
            IRatService<Rat, RatDto, CreateRatDto, UpdateRatDto> rats,
            IBehaviorLogService<BehaviorLog, BehaviorLogDto, CreateBehaviorLogDto> behaviorLogs,
            IExperimentalNoteService<ExperimentalNote, ExperimentalNoteDto, CreateExperimentalNoteDto> experimentalNotes,
            ISessionService<Session, SessionDto, CreateSessionDto, UpdateSessionDto> sessions,
            ITrainerService<Trainer, TrainerDto, CreateTrainerDto, UpdateTrainerDto> trainers,
            ITrialService<Trial, TrialDto, CreateTrialDto> trials,
            ITrialOdorService<TrialOdor, TrialOdorDto, CreateTrialOdorDto> trialOdors,
            IOdorService<Odor, OdorDto, CreateOdorDto> odors,
            IInterventionService<Intervention, InterventionDto, CreateInterventionDto> interventions,
            IFoodIntakeLogService<FoodIntakeLog, FoodIntakeLogDto, CreateFoodIntakeLogDto> foodIntakeLogs,
            IRatWeightService<RatWeight, RatWeightDto, CreateRatWeightDto> ratWeights,
            ITransferTestService<TransferTest, TransferTestDto, CreateTransferTestDto> transferTests,
            AppDbContext context)
        {
            Rats = rats;
            BehaviorLogs = behaviorLogs;
            ExperimentalNotes = experimentalNotes;
            Sessions = sessions;
            Trainers = trainers;
            Trials = trials;
            TrialOdors = trialOdors;
            Odors = odors;
            Interventions = interventions;
            FoodIntakeLogs = foodIntakeLogs;
            RatWeights = ratWeights;
            TransferTests = transferTests;
            _context = context;
        }

        public IRatService<Rat, RatDto, CreateRatDto, UpdateRatDto> Rats { get; }
        public IBehaviorLogService<BehaviorLog, BehaviorLogDto, CreateBehaviorLogDto> BehaviorLogs { get; }
        public IExperimentalNoteService<ExperimentalNote, ExperimentalNoteDto, CreateExperimentalNoteDto> ExperimentalNotes { get; }
        public ISessionService<Session, SessionDto, CreateSessionDto, UpdateSessionDto> Sessions { get; }
        public ITrainerService<Trainer, TrainerDto, CreateTrainerDto, UpdateTrainerDto> Trainers { get; }
        public ITrialService<Trial, TrialDto, CreateTrialDto> Trials { get; }
        public ITrialOdorService<TrialOdor, TrialOdorDto, CreateTrialOdorDto> TrialOdors { get; }
        public IOdorService<Odor, OdorDto, CreateOdorDto> Odors { get; }
        public IInterventionService<Intervention, InterventionDto, CreateInterventionDto> Interventions { get; }
        public IFoodIntakeLogService<FoodIntakeLog, FoodIntakeLogDto, CreateFoodIntakeLogDto> FoodIntakeLogs { get; }
        public IRatWeightService<RatWeight, RatWeightDto, CreateRatWeightDto> RatWeights { get; }
        public ITransferTestService<TransferTest, TransferTestDto, CreateTransferTestDto> TransferTests { get; }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _context.SaveChangesAsync(ct);
    }
}
