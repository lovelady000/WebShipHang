using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System.Collections.Generic;
using System;
using System.Linq;
namespace ShipShop.Service
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll(string[] include = null);

        IEnumerable<Order> GetAllByUserName(string userName,DateTime dtBeginDate, DateTime dtToDate, int page, int pageSize, out int totalCount, string[] include = null);

        Order Add(Order order);

        void Update(Order order);

        Order Delete(int id);

        void Save();

        Order GetByID(int id, string[] includes = null);

    }

    public class OrderService : IOrderService
    {
        private IUnitOfWork _unitOfWork;
        private IOrderRepository _orderRepository;

        public OrderService(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            this._unitOfWork = unitOfWork;
            this._orderRepository = orderRepository;
        }

        public Order Add(Order order)
        {
            return _orderRepository.Add(order);
        }

        public Order Delete(int id)
        {
            return _orderRepository.Delete(id);
        }

        public IEnumerable<Order> GetAll(string[] include = null)
        {
            return _orderRepository.GetAll(include);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public IEnumerable<Order> GetAllByUserName(string userName, DateTime dtBeginDate, DateTime dtToDate, int page, int pageSize, out int totalCount, string[] include = null)
        {
            var model = _orderRepository.GetMulti(x => x.Username == userName && x.CreatedDate.HasValue && x.CreatedDate.Value.CompareTo(dtBeginDate) != -1 && x.CreatedDate.Value.CompareTo(dtToDate) != 1, include).OrderByDescending(x=>x.CreatedDate);
            totalCount = model.Count();
            return model.Skip(pageSize * (page - 1)).Take(pageSize);
        }

        public Order GetByID(int id,string [] includes = null)
        {
            //return _orderRepository.GetSingleById(id);
            return _orderRepository.GetSingleByCondition(x => x.ID == id, includes);
        }
    }
}