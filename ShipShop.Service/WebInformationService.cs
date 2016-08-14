using ShipShop.Data.Infrastructure;
using ShipShop.Data.Repositories;
using ShipShop.Model.Models;
using System.Linq;

namespace ShipShop.Service
{
    public interface IWebInformationService
    {
        void Update(WebInformation webInformation);

        WebInformation GetSingle();

        void Save();
    }

    public class WebInformationService : IWebInformationService
    {
        private IWebInformationRepository _webInformationRepository;
        private IUnitOfWork _unitOfWork;

        public WebInformationService(IWebInformationRepository webInformationRepository, IUnitOfWork unitOfWork)
        {
            this._webInformationRepository = webInformationRepository;
            this._unitOfWork = unitOfWork;
        }

        public WebInformation GetSingle()
        {
            return _webInformationRepository.GetAll().First();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(WebInformation webInformation)
        {
            _webInformationRepository.Update(webInformation);
        }
    }
}