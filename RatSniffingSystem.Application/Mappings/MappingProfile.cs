using AutoMapper;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Rat
            CreateMap<Rat, RatDto>().ReverseMap();
            CreateMap<CreateRatDto, Rat>();
            CreateMap<UpdateRatDto, Rat>();

            // Trainer
            CreateMap<Trainer, TrainerDto>().ReverseMap();
            CreateMap<CreateTrainerDto, Trainer>();
            CreateMap<UpdateTrainerDto, Trainer>();

            // Session
            CreateMap<Session, SessionDto>().ReverseMap();
            CreateMap<CreateSessionDto, Session>();
            CreateMap<UpdateSessionDto, Session>(); 

            // Trial
            CreateMap<Trial, TrialDto>().ReverseMap();
            CreateMap<TrialOdor, TrialOdorDto>().ReverseMap();
            CreateMap<CreateTrialDto, Trial>();
            CreateMap<CreateTrialOdorDto, TrialOdor>();

            // RatWeight
            CreateMap<RatWeight, RatWeightDto>().ReverseMap();
            CreateMap<CreateRatWeightDto, RatWeight>();

            // Behavior Log
            CreateMap<BehaviorLog, BehaviorLogDto>().ReverseMap();
            CreateMap<CreateBehaviorLogDto, BehaviorLog>();

            // Intervention
            CreateMap<Intervention, InterventionDto>().ReverseMap();
            CreateMap<CreateInterventionDto, Intervention>();

            // Transfer Test    
            CreateMap<TransferTest, TransferTestDto>().ReverseMap();
            CreateMap<TransferTestDto, TransferTest>();

            // Food Intake
            CreateMap<FoodIntakeLog, FoodIntakeLogDto>().ReverseMap();
            CreateMap<CreateFoodIntakeLogDto, FoodIntakeLog>();

            // Experiment Note
            CreateMap<ExperimentalNote, ExperimentalNoteDto>().ReverseMap();
            CreateMap<CreateExperimentalNoteDto, ExperimentalNote>();

            // Rat Weight
            CreateMap<RatWeight, RatWeightDto>().ReverseMap();
            CreateMap<CreateRatWeightDto, RatWeight>();
        }
    }
}
