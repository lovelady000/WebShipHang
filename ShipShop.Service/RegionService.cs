using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System.Collections.Generic;

namespace ShipShop.Service
{
    public interface IRegionService
    {
        IEnumerable<Region> GetAll(string[] include = null);

        void Save();
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

        public IEnumerable<Region> GetAll(string[] include = null)
        {
            return _regionRepository.GetAll(include);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}