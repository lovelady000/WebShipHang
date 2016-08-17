using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System.Collections.Generic;
using System;

namespace ShipShop.Service
{
    public interface INewsService
    {
        IEnumerable<News> GetAll(bool status = true);

        News Add(News news);

        void Update(News news);

        News Delete(int id);

        News GetById(int id);

        void Save();
    }

    public class NewsService : INewsService
    {
        private INewsRepository _newsRepository;
        private IUnitOfWork _unitOfWork;

        public NewsService(INewsRepository newsRepository, IUnitOfWork unitOfWork)
        {
            this._newsRepository = newsRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<News> GetAll(bool status = true)
        {
            if (status)
            {
                return _newsRepository.GetMulti(x => x.Status);
            }
            return _newsRepository.GetAll();
        }

        public News Add(News news)
        {
            return _newsRepository.Add(news);
        }

        public void Update(News news)
        {
            _newsRepository.Update(news);
        }

        public News Delete(int id)
        {
            return _newsRepository.Delete(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public News GetById(int id)
        {
            return _newsRepository.GetSingleById(id);
        }
    }
}