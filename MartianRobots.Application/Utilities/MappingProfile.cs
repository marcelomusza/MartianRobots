using AutoMapper;
using MartianRobots.Application.DTOs;
using MartianRobots.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobots.Application.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RobotDTO, Robot>();
            CreateMap<PositionDTO, Position>();
            CreateMap<GridCoordinatesDTO, GridCoordinate>();
            CreateMap<RobotScentDTO, RobotScent>();

            CreateMap<RobotMovements, RobotMovementsOutputDTO>()
                .ForMember(dest => dest.Name, x => x.MapFrom(src => src.Robot.Name))
                .ForMember(dest => dest.Orientation, x => x.MapFrom(src => src.Orientation))
                .ForMember(dest => dest.Instruction, x => x.MapFrom(src => src.Instruction))
                .ForMember(dest => dest.PositionX, x => x.MapFrom(src => src.Position.X))
                .ForMember(dest => dest.PositionY, x => x.MapFrom(src => src.Position.Y))
                .ForMember(dest => dest.Dead, x => x.MapFrom(src => src.Dead));
        }
    }
}
