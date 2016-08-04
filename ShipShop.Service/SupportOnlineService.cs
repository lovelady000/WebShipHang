using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Service
{
    public interface ISupportOnlineService
    {
        IEnumerable<SupportOnline> GetAll();
        SupportOnline Add(SupportOnline supportOnline);
        void Update(SupportOnline supportOnline);
        SupportOnline Delete(int id);
        void Save();

    }
    public class SupportOnlineService : ISupportOnlineService
    {
        private ISupportOnlineRepository _supportOnlineRepository;
        private IUnitOfWork _unitOfWork;

        public SupportOnlineService(ISupportOnlineRepository supportOnlineRepository, IUnitOfWork unitOfWork)
        {
            this._supportOnlineRepository = supportOnlineRepository;
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<SupportOnline> GetAll()
        {
            return _supportOnlineRepository.GetAll();
        }
        public SupportOnline Add(SupportOnline supportOnline)
        {
            return _supportOnlineRepository.Add(supportOnline);
        }
        public void Update(SupportOnline supportOnline)
        {
            _supportOnlineRepository.Update(supportOnline);
        }
        public SupportOnline Delete(int id)
        {
            return _supportOnlineRepository.Delete(id);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
