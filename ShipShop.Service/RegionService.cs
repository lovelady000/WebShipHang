using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System.Collections.Generic;
using System;

namespace ShipShop.Service
{
    public interface IRegionService
    {
        IEnumerable<Region> GetAll(string[] include = null);

        void Save();

        Region Add(Region region);

        void Update(Region region);

        Region Delete(int id);

        Region GetById(int id);

    }

    internal class RegionService : IRegionService
    {
        private IRegionRepository _regionRepository;
        private IUnitOfWork _unitOfWork;

        public RegionService(IRegionRepository regionRepository, IUnitOfWork unitOfWork)
        {
            this._regionRepository = regionRepository;
            this._unitOfWork = unitOfWork;
        }

        public Region Add(Region region)
        {
            return _regionRepository.Add(region);
        }

        public Region Delete(int id)
        {
            return _regionRepository.Delete(id);
        }

        public IEnumerable<Region> GetAll(string[] include = null)
        {
            return _regionRepository.GetAll(include);
        }

        public Region GetById(int id)
        {
            return _regionRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Region region)
        {
            _regionRepository.Update(region);
        }
    }
}