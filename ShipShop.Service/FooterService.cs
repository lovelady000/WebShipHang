using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System.Collections.Generic;

namespace ShipShop.Service
{
    public interface IFooterService
    {
        IEnumerable<Footer> GetAll();

        Footer GetByID(string IDFooter);

        Footer Add(Footer footer);

        void Update(Footer footer);

        Footer Delete(Footer footer);

        void Save();
    }

    public class FooterService : IFooterService
    {
        private IFooterRepository _footerRepository;
        private IUnitOfWork _unitOfWork;

        public FooterService(IFooterRepository footerRepository, IUnitOfWork unitOfWork)
        {
            this._footerRepository = footerRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Footer> GetAll()
        {
            return _footerRepository.GetAll();
        }

        public Footer GetByID(string IDFooter)
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == IDFooter);
        }

        public Footer Add(Footer footer)
        {
            return _footerRepository.Add(footer);
        }

        public void Update(Footer footer)
        {
            _footerRepository.Update(footer);
        }

        public Footer Delete(Footer footer)
        {
            return _footerRepository.Delete(footer);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}