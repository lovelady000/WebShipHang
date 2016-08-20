using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System.Collections.Generic;
using System;

namespace ShipShop.Service
{
    public interface IPageService
    {
        IEnumerable<Page> GetAll(bool status = true);

        Page Add(Page page);

        void Update(Page page);

        Page Delete(int id);

        Page GetById(int id);

        void Save();

        Page GetByAlias(string alias);
    }

    public class PageService : IPageService
    {
        private IPageRepository _pageRepository;
        private IUnitOfWork _unitOfWork;

        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Page> GetAll(bool status = true)
        {
            if (status)
            {
                return _pageRepository.GetMulti(x => x.Status);
            }
            return _pageRepository.GetAll();
        }

        public Page Add(Page page)
        {
            return _pageRepository.Add(page);
        }

        public void Update(Page page)
        {
            _pageRepository.Update(page);
        }

        public Page Delete(int id)
        {
            return _pageRepository.Delete(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public Page GetById(int id)
        {
            return _pageRepository.GetSingleById(id);
        }

        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }
    }
}