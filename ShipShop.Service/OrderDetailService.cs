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

    public interface IOrderDetailService
    {
        IEnumerable<OrderDetail> GetAll(string[] include = null);
        IEnumerable<OrderDetail> GetAllByOrder(int idOrder,string[] include = null);
        OrderDetail Add(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
        OrderDetail Delete(int id);
        void Save();
    }
    public class OrderDetailService : IOrderDetailService
    {
        private IUnitOfWork _unitOfWork;
        private IOrderDetailRepository _orderDetailRepository;
        public OrderDetailService(IUnitOfWork unitOfWork, IOrderDetailRepository orderDetailRepository)
        {
            this._unitOfWork = unitOfWork;
            this._orderDetailRepository = orderDetailRepository;
        }
        public OrderDetail Add(OrderDetail orderDetail)
        {
            return _orderDetailRepository.Add(orderDetail);
        }

        public OrderDetail Delete(int id)
        {
            return _orderDetailRepository.Delete(id);
        }

        public IEnumerable<OrderDetail> GetAll(string[] include = null)
        {
            return _orderDetailRepository.GetAll(include);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(OrderDetail orderDetail)
        {
            _orderDetailRepository.Update(orderDetail);
        }

        public IEnumerable<OrderDetail> GetAllByOrder(int idOrder, string[] include = null)
        {
            return _orderDetailRepository.GetMulti(x => x.OrderID == idOrder, include);
        }
    }
}
