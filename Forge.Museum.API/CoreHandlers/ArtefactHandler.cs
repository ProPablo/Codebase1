﻿using System;
using System.Collections.Generic;
using System.Linq;
using Forge.Museum.Interfaces.DataTransferObjects.Artefact;
using Forge.Museum.API.Models;
using AutoMapper;
using Forge.Museum.API.Controllers;

namespace Forge.Museum.API.CoreHandlers
{
    public class ArtefactHandler : BaseApiHandler
    {
		public ArtefactHandler(bool test = false) : base(test)
		{

		}

		#region CRUD
		public ArtefactDto Create(ArtefactDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException("Name");

            Artefact artefact = new Artefact
            {
                Name = dto.Name,
                Description = dto.Description,
                Image = dto.Image,
                ImageFileType = dto.ImageFileType,
                AdditionalComments = dto.AdditionalComments,
                AcquisitionDate = dto.AcquisitionDate,
                Radius_Of_Effect = dto.Radius_Of_Effect,
                Coord_X = dto.Coord_X,
                Coord_Y = dto.Coord_Y,
                Activation = dto.Activation,
                ArtefactStatus = (int)dto.ArtefactStatus,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            //Add Zone 
            if(dto.Zone != null && dto.Zone.Id > 0)
            {
                artefact.Zone = Db.Zones.Find(dto.Zone.Id);
            }

            //Add category
            if(dto.ArtefactCategory != null && dto.ArtefactCategory.Id > 0)
            {
                artefact.ArtefactCategory = Db.ArtefactCategories.Find(dto.ArtefactCategory.Id);
            }

            //Add Beacon
            if (dto.Beacon != null && dto.Beacon.Id > 0)
            {
                artefact.Beacon = Db.Beacons.Find(dto.Beacon.Id);
            }

            Db.Artefacts.Add(artefact);

            Db.SaveChanges();

            return Mapper.Map<ArtefactDto>(artefact);
        }

        public ArtefactDto Update(ArtefactDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException("Name");

            Artefact artefact = Db.Artefacts.FirstOrDefault(m => m.Id == dto.Id);

            if (artefact == null) NotFoundException();

            artefact.Name = dto.Name;
            artefact.Description = dto.Description;
            artefact.Image = dto.Image;
			artefact.ImageFileType = dto.ImageFileType;
            artefact.AdditionalComments = dto.AdditionalComments;
            artefact.AcquisitionDate = dto.AcquisitionDate;
            artefact.Radius_Of_Effect = dto.Radius_Of_Effect;
            artefact.Coord_X = dto.Coord_X;
            artefact.Coord_Y = dto.Coord_Y;
            artefact.Activation = dto.Activation;
            artefact.ArtefactStatus = (int)dto.ArtefactStatus;
            artefact.IsDeleted = dto.IsDeleted;
            artefact.ModifiedDate = DateTime.UtcNow;

            // Process Beacon
            if (dto.Beacon.Id > 0)
            {
                artefact.Beacon = Db.Beacons.Find(dto.Beacon.Id);
            }
            else
            {
                _ = artefact.Beacon;
                artefact.Beacon = null;
            }

            // Process Category
            if (dto.ArtefactCategory.Id > 0)
            {
                artefact.ArtefactCategory = Db.ArtefactCategories.Find(dto.ArtefactCategory.Id);
            }
            else
            {
                _ = artefact.ArtefactCategory;
                artefact.ArtefactCategory = null;
            }

            // Process zone
            if (dto.Zone.Id > 0)
            {
                artefact.Zone = Db.Zones.Find(dto.Zone.Id);
            }
            else
            {
                _ = artefact.Zone;
                artefact.Zone = null;
            }

            Db.SaveChanges();

            return Mapper.Map<ArtefactDto>(artefact);
        }

        public ArtefactDto GetById(int artefactId)
        {
            Artefact artefact = Db.Artefacts.Find(artefactId);

            if (artefact == null) NotFoundException();

            return Mapper.Map<ArtefactDto>(artefact);
        }

        public List<ArtefactDto> GetFiltered(ArtefactFilter filter)
        {
            IQueryable<Artefact> artefacts = Db.Artefacts;

            if (filter.categoryId.HasValue)
                artefacts = artefacts.Where(m => m.ArtefactCategory.Id == filter.categoryId.Value);

            if (filter.zoneId.HasValue)
                artefacts = artefacts.Where(m => m.Zone.Id == filter.zoneId.Value);

            if (filter.beaconId.HasValue)
                artefacts = artefacts.Where(m => m.Beacon.Id == filter.beaconId.Value);

            if (filter.isDeleted.HasValue)
                artefacts = artefacts.Where(m => m.IsDeleted == filter.isDeleted.Value);

            return Mapper.Map<List<ArtefactDto>>(artefacts.OrderBy(m => m.ModifiedDate).Skip(filter.pageSize * filter.page).Take(filter.pageSize));
        }

        public bool Delete(int artefactId)
        {
            Artefact artefact = Db.Artefacts.FirstOrDefault(m => m.Id == artefactId);

            if (artefact == null) NotFoundException();

            artefact.IsDeleted = true;
            artefact.ModifiedDate = DateTime.UtcNow;

            Db.SaveChanges();

            return true;
        }
		#endregion



	}
}