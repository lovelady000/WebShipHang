using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System.Collections.Generic;
using System;

namespace ShipShop.Service
{
    public interface ISlideService
    {
        IEnumerable<Slide> GetAll();

        Slide Add(Slide menu);

        void Update(Slide menu);

        Slide Delete(int menuID);

        Slide GetById(int id);

        void Save();
    }

    public class SlideService : ISlideService
    {
        private ISlideRepository _slideRepository;
        private IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            this._slideRepository = slideRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public Slide Add(Slide slide)
        {
            return _slideRepository.Add(slide);
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
        }

        public Slide Delete(int slideID)
        {
            return _slideRepository.Delete(slideID);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public Slide GetById(int id)
        {
            return _slideRepository.GetSingleById(id);
        }
    }
}