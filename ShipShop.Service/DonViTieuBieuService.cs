using ShipShop.Model.Models;
using System.Collections.Generic;
using System;
using ShipShop.Data.Repositories;
using ShipShop.Data.Infrastructure;

namespace ShipShop.Service
{
    public interface IDonViTieuBieuService
    {
        IEnumerable<DonViTieuBieu> GetAll();

        DonViTieuBieu GetByID(int id);

        DonViTieuBieu Add(DonViTieuBieu entity);

        void Update(DonViTieuBieu entity);

        DonViTieuBieu Delete(int id);

        void Save();
    }

    public class DonViTieuBieuService : IDonViTieuBieuService
    {
        private IDonViTieuBieuRepository _donViTieuBieuRepository;
        private IUnitOfWork _unitOfWork;

        public DonViTieuBieuService(IDonViTieuBieuRepository donViTieuBieuRepository, IUnitOfWork unitOfWork)
        {
            this._donViTieuBieuRepository = donViTieuBieuRepository;
            this._unitOfWork = unitOfWork;
        }
        public DonViTieuBieu Add(DonViTieuBieu entity)
        {
            return _donViTieuBieuRepository.Add(entity);
        }

        public DonViTieuBieu Delete(int id)
        {
            return _donViTieuBieuRepository.Delete(id);
        }

        public IEnumerable<DonViTieuBieu> GetAll()
        {
            return _donViTieuBieuRepository.GetAll();
        }

        public DonViTieuBieu GetByID(int id)
        {
            return _donViTieuBieuRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(DonViTieuBieu entity)
        {
            _donViTieuBieuRepository.Update(entity);
        }
    }
}