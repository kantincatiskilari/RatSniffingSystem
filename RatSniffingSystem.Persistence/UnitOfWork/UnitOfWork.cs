using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Persistence.Context;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(
            IRatService<RatDto, CreateRatDto, UpdateRatDto> rats,
            IBehaviorLogService<BehaviorLogDto, CreateBehaviorLogDto> behaviorLogs,
            IExperimentalNoteService<ExperimentalNoteDto, CreateExperimentalNoteDto> experimentalNotes,
            ISessionService<SessionDto, CreateSessionDto, UpdateSessionDto> sessions,
            ITrainerService<TrainerDto, CreateTrainerDto, UpdateTrainerDto> trainers,
            ITrialService<TrialDto, CreateTrialDto> trials,
            ITrialOdorService<TrialOdorDto, CreateTrialOdorDto> trialOdors,
            IOdorService<OdorDto, CreateOdorDto> odors,
            IInterventionService<InterventionDto, CreateInterventionDto> interventions,
            IFoodIntakeLogService<FoodIntakeLogDto, CreateFoodIntakeLogDto> foodIntakeLogs,
            IRatWeightService<RatWeightDto, CreateRatWeightDto> ratWeights,
            ITransferTestService<TransferTestDto, CreateTransferTestDto> transferTests,
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
            TransferTests = transferTests;
            _context = context;
        }

        public IRatService<RatDto, CreateRatDto, UpdateRatDto> Rats { get; }
        public IBehaviorLogService<BehaviorLogDto, CreateBehaviorLogDto> BehaviorLogs { get; }
        public IExperimentalNoteService<ExperimentalNoteDto, CreateExperimentalNoteDto> ExperimentalNotes { get; }
        public ISessionService<SessionDto, CreateSessionDto, UpdateSessionDto> Sessions { get; }
        public ITrainerService<TrainerDto, CreateTrainerDto, UpdateTrainerDto> Trainers { get; }
        public ITrialService<TrialDto, CreateTrialDto> Trials { get; }
        public ITrialOdorService<TrialOdorDto, CreateTrialOdorDto> TrialOdors { get; }
        public IOdorService<OdorDto, CreateOdorDto> Odors { get; }
        public IInterventionService<InterventionDto, CreateInterventionDto> Interventions { get; set; }
        public IFoodIntakeLogService<FoodIntakeLogDto, CreateFoodIntakeLogDto> FoodIntakeLogs { get; set; }
        public IRatWeightService<RatWeightDto, CreateRatWeightDto> RatWeights { get; set; }
        public ITransferTestService<TransferTestDto, CreateTransferTestDto> TransferTests { get; set; }

   

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
