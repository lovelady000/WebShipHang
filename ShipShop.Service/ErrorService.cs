using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System.Collections.Generic;
using System;

namespace ShipShop.Service
{
    public interface IErrorService
    {
        Error Create(Error error);

        IEnumerable<Error> GetAll(int page, int pageSize);

        void Save();
    }

    public class ErrorService : IErrorService
    {
        private IErrorRepository _errorRepository;
        private IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }

        public Error Create(Error error)
        {
            return _errorRepository.Add(error);
        }

        public IEnumerable<Error> GetAll(int page, int pageSize)
        {
            return _errorRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}