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
    public interface IVisitorStatisticService
    {
        IEnumerable<VisitorStatistic> GetAll();

        VisitorStatistic Add(VisitorStatistic visitorStatistic);

        void Save();

    }
    public class VisitorStatisticService
    {
        private IVisitorStatisticRepository _visitorStatisticRepository;
        private IUnitOfWork _unitOfWork;
        public VisitorStatisticService(IVisitorStatisticRepository visitorStatisticRepository, IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._visitorStatisticRepository = visitorStatisticRepository;
        }
        public IEnumerable<VisitorStatistic> GetAll()
        {
            return _visitorStatisticRepository.GetAll();
        }

        public VisitorStatistic Add(VisitorStatistic visitorStatistic)
        {
            return _visitorStatisticRepository.Add(visitorStatistic);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
