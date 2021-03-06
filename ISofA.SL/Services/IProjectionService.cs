﻿using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface IProjectionService
    {
        IEnumerable<ProjectionDTO> GetProjectionsForTheater(int theaterId, DateTime dateStart, int days);
        IEnumerable<ProjectionDTO> GetProjectionsForPlay(int playId, DateTime dateStart);
        ProjectionDTO GetProjectionDetail(int projectionId);
        ProjectionDTO Add(int theaterId, Projection projection);
        IEnumerable<ProjectionDTO> Add(int theaterId, IEnumerable<Projection> projection);
        ProjectionDTO Update(int projectionId, Projection projection);
        void Remove(int projectionId);        
    }
}