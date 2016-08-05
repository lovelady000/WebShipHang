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
    public interface IMenuGroupService
    {
        IEnumerable<MenuGroup> GetAll();

        MenuGroup GetByID(int id);
        MenuGroup Add(MenuGroup menuGroup);

        void Update(MenuGroup menuGroup);
        MenuGroup Delete(int id);
        void Save();

    }
    public class MenuGroupService : IMenuGroupService
    {
        private IMenuGroupRepository _menuGroupRepository;
        private IUnitOfWork _unitOfWork;

        public MenuGroupService(IMenuGroupRepository menuGroupRepository, IUnitOfWork unitOfWork)
        {
            this._menuGroupRepository = menuGroupRepository;
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<MenuGroup> GetAll()
        {
            return _menuGroupRepository.GetAll();
        }

        public MenuGroup GetByID(int id)
        {
            return _menuGroupRepository.GetSingleById(id);
        }
        public MenuGroup Add(MenuGroup menuGroup)
        {
            return _menuGroupRepository.Add(menuGroup);
        }

        public void Update(MenuGroup menuGroup)
        {
            _menuGroupRepository.Update(menuGroup);
        }
        public MenuGroup Delete(int id)
        {
            return _menuGroupRepository.Delete(id);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

    }
}
