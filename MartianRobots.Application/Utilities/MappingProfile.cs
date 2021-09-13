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
        }
    }
}
