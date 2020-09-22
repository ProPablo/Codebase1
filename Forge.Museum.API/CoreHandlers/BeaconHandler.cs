using Forge.Museum.API.Models;
using Forge.Museum.Interfaces.DataTransferObjects.Artefact;
using Forge.Museum.Interfaces.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Forge.Museum.API.CoreHandlers
{
    public class BeaconHandler : BaseApiHandler
    {
        public BeaconHandler(bool test = false) : base(test) { }

        #region CRUD
        public BeaconDto Create(BeaconDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException("Name");

            Beacon beacon = new Beacon
            {
                Name = dto.Name,
                Description = dto.Description,
                Coord_X = dto.Coord_X,
                Coord_Y = dto.Coord_Y,
                MAC_address = dto.MAC_address,
                Visits = dto.Visits,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            Db.Beacons.Add(beacon);

            Db.SaveChanges();

            return Mapper.Map<BeaconDto>(beacon);
        }

        public BeaconDto Update(BeaconDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException("Name");

            Beacon beacon = Db.Beacons.FirstOrDefault(m => m.Id == dto.Id);

            if (beacon == null) NotFoundException();

            beacon.Name = dto.Name;
            beacon.Description = dto.Description;
            beacon.ModifiedDate = DateTime.UtcNow;
            beacon.IsDeleted = dto.IsDeleted;
            beacon.Coord_X = dto.Coord_X;
            beacon.Coord_Y = dto.Coord_Y;
            beacon.MAC_address = dto.MAC_address;
            beacon.Visits = dto.Visits;

            Db.SaveChanges();

            return Mapper.Map<BeaconDto>(beacon);
        }

        public BeaconDto GetById(int BeaconId)
        {
            Beacon beacon = Db.Beacons.Find(BeaconId);

            if (beacon == null) NotFoundException();

            return Mapper.Map<BeaconDto>(beacon);
        }

        public List<BeaconDto> GetFiltered(ApiFilter filter)
        {
            IQueryable<Beacon> beacons = Db.Beacons;

            if (filter.isDeleted.HasValue)
                beacons = beacons.Where(m => m.IsDeleted == filter.isDeleted.Value);

            return Mapper.Map<List<BeaconDto>>(beacons.OrderBy(m => m.ModifiedDate).Skip(filter.pageSize * filter.page).Take(filter.pageSize));
        }

        public bool Delete(int BeaconId)
        {
            Beacon beacon = Db.Beacons.FirstOrDefault(m => m.Id == BeaconId);

            if (beacon == null) NotFoundException();

            beacon.IsDeleted = true;
            beacon.ModifiedDate = DateTime.UtcNow;

            Db.SaveChanges();

            return true;
        }
        #endregion
    }
}