using Forge.Museum.API.CoreHandlers;
using Forge.Museum.Interfaces.DataTransferObjects.Artefact;
using Forge.Museum.Interfaces.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Forge.Museum.API.Controllers
{
    public class BeaconController : BaseApiController
    {
        private bool isTest;

        public BeaconController()
        {
            isTest = false;
        }

        public BeaconController(bool test = false)
        {
            isTest = test;
        }

        #region CRUD
        [HttpPost, Route("api/beacon")]
        public BeaconDto Create([FromBody] BeaconDto dto)
        {
            try
            {
                return new BeaconHandler(isTest).Create(dto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut, Route("api/beacon")]
        public BeaconDto Update([FromBody] BeaconDto dto)
        {
            try
            {
                return new BeaconHandler(isTest).Update(dto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet, Route("api/beacon/{beaconId}")]
        public BeaconDto GetById(int beaconId)
        {
            try
            {
                return new BeaconHandler(isTest).GetById(beaconId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet, Route("api/beacon")]
        public List<BeaconDto> GetFiltered([FromUri] ApiFilter filter)
        {
            try
            {
                return new BeaconHandler(isTest).GetFiltered(filter);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete, Route("api/beacon/{beaconId}")]
        public bool Delete(int beaconId)
        {
            try
            {
                return new BeaconHandler(isTest).Delete(beaconId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}