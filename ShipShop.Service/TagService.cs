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
    public interface ITagService
    {
        IEnumerable<Tag> GetAll();

        Tag Add(Tag tag);
        void Update(Tag tag);
        Tag Delete(Tag tag);
        void Save();
    }
    public class TagService : ITagService
    {
        private ITagRepository _tagRepository;
        private IUnitOfWork _unitOfWork;

        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this._tagRepository = tagRepository;
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }

        public Tag Add(Tag tag)
        {
            return _tagRepository.Add(tag);
        }
        public void Update(Tag tag)
        {
            _tagRepository.Update(tag);
        }
        public Tag Delete(Tag tag)
        {
            return _tagRepository.Delete(tag);
        }
        public void Save()
        {
            _unitOfWork.Commit();
        }

    }
}
