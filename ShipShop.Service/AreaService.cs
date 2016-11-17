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
    public interface IAreaService
    {
        IEnumerable<Area> GetAll(bool status = true);

        Area Add(Area area);

        void Update(Area area);

        Area Delete(int id);

        Area GetById(int id);

        void Save();
    }
    public class AreaService : IAreaService
    {
        private IAreaRepository _areaRepository;
        private IUnitOfWork _unitOfWork;

        public AreaService(IAreaRepository areaRepository, IUnitOfWork unitOfWork)
        {
            this._areaRepository = areaRepository;
            this._unitOfWork = unitOfWork;
        }

        public Area Add(Area area)
        {
            return _areaRepository.Add(area);
           
        }

        public Area Delete(int id)
        {
            return _areaRepository.Delete(id);
        }

        public IEnumerable<Area> GetAll(bool status = true)
        {
            return _areaRepository.GetAll();
        }

        public Area GetById(int id)
        {
            return _areaRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Area area)
        {
            _areaRepository.Update(area);
        }
    }
}
