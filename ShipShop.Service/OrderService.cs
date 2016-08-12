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
    public interface IOrderService
    {
        IEnumerable<Order> GetAll(string [] include = null);
        Order Add(Order order);
        void Update(Order order);
        Order Delete(int id);
        void Save();
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
    }
}
