using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System.Collections.Generic;

namespace ShipShop.Service
{
    public interface IMenuService
    {
        IEnumerable<Menu> GetAll();

        Menu AddMenu(Menu menu);

        void EditMenu(Menu menu);

        Menu DeleteMenu(int menuID);

        void Save();
    }

    public class MenuService : IMenuService
    {
        private IMenuRepository _menuRepository;
        private IUnitOfWork _unitOfWork;

        public MenuService(IMenuRepository menuRepository, IUnitOfWork unitOfWork)
        {
            this._menuRepository = menuRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }

        public Menu AddMenu(Menu menu)
        {
            return _menuRepository.Add(menu);
        }

        public void EditMenu(Menu menu)
        {
            _menuRepository.Update(menu);
        }

        public Menu DeleteMenu(int menuID)
        {
            return _menuRepository.Delete(menuID);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}