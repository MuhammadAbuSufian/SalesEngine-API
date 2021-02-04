using Project.Model;
using Project.Model.Enums;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{

    public interface IJournalService : IBaseService<Journal, JournalViewModel>
    {
        List<DropdownViewModel> GetJournalTypeDropdownData();
        GridResponseModel<JournalViewModel> GetGridData(GridRequestModel request);
        GridResponseModel<JournalViewModel> GetGridData(GridRequestModel request, DateTime startDate, DateTime endDate);
        GridResponseModel<JournalViewModel> GetGridDataByUser(GridRequestModel request);
        GridResponseModel<JournalTypeViewModel> GetJournalTypeGridData(GridRequestModel request);
        bool Update(Journal model);
        bool UpdateJournalType(JournalType model);
        bool SaveJournalType(JournalType model);
        bool MakeApprove(string id);
        bool MakePaid(string id);
    }

    public class JournalService : BaseService<Journal, JournalViewModel>, IJournalService
    {
        private readonly IJournalRepository _repository;
        private readonly IJournalTypeRepository _journalTypeRepository;

        public JournalService(
            IJournalRepository repository,
            IJournalTypeRepository journalTypeRepository
            ) : base(repository)
        {
            _repository = repository;
            _journalTypeRepository = journalTypeRepository;
        }

        public List<DropdownViewModel> GetJournalTypeDropdownData()
        {
            List<DropdownViewModel> journalTypes = _journalTypeRepository.GetAllActive().Select(
                x => new DropdownViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }
            ).ToList();

            return journalTypes;
        }

        public GridResponseModel<JournalViewModel> GetGridData(GridRequestModel request)
        {
            GridResponseModel<JournalViewModel> gridData = new GridResponseModel<JournalViewModel>();

            gridData.Count = _repository.GetAllActive(getCreatedCompanyId()).Count();

            var query = _repository.GetAllActive(getCreatedCompanyId());

            if (request.IsAscending)
            {
                switch (request.OrderBy)
                {
                    case "Status": query = query.OrderBy(l => l.Status); break;
                    default: query = query.OrderBy(l => l.Created); break;
                }
            }
            else
            {
                switch (request.OrderBy)
                {
                    case "Status": query = query.OrderByDescending(l => l.Status); break;
                    default: query = query.OrderByDescending(l => l.Created); break;
                }
            }

            query = query.Include(x => x.JournalType).Include(x => x.SubmittedBy).Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount);

            List<Journal> journals = query.ToList();

            foreach (var journal in journals)
            {
                gridData.Data.Add(new JournalViewModel(journal));
            }
            return gridData;
        }
        public GridResponseModel<JournalViewModel> GetGridData(GridRequestModel request, DateTime startDate, DateTime endDate)
        {
            GridResponseModel<JournalViewModel> gridData = new GridResponseModel<JournalViewModel>();
            endDate = endDate.AddDays(1);
            gridData.Count = _repository.GetAllActive(getCreatedCompanyId()).Where(x => x.Status == JournalStatus.paid).Where(x => x.Created >= startDate && x.Created <= endDate).Count();
            var totalValue = _repository.GetAllActive(getCreatedCompanyId()).Where( x => x.Status == JournalStatus.paid).Where(x => x.Created >= startDate && x.Created <= endDate).Sum(x=>x.Amount);
            gridData.Value = totalValue;
            var query = _repository.GetAllActive(getCreatedCompanyId());

            if (request.IsAscending)
            {
                switch (request.OrderBy)
                {
                    case "Status": query = query.OrderBy(l => l.Status); break;
                    default: query = query.OrderBy(l => l.Created); break;
                }
            }
            else
            {
                switch (request.OrderBy)
                {
                    case "Status": query = query.OrderByDescending(l => l.Status); break;
                    default: query = query.OrderByDescending(l => l.Created); break;
                }
            }

            query = query.Where(x => x.Status == JournalStatus.paid).Where(x => x.Created >= startDate && x.Created <= endDate).Include(x => x.JournalType).Include(x => x.SubmittedBy).Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount);

            List<Journal> journals = query.ToList();

            foreach (var journal in journals)
            {
                gridData.Data.Add(new JournalViewModel(journal));
            }
            return gridData;
        }

        public GridResponseModel<JournalViewModel> GetGridDataByUser(GridRequestModel request)
        {
            GridResponseModel<JournalViewModel> gridData = new GridResponseModel<JournalViewModel>();
            var user = this.GetUserFromToken();
            gridData.Count = _repository.GetAllActive(getCreatedCompanyId()).Where(x => x.CreatedBy == user.UserId).Count();

            var query = _repository.GetAllActive(getCreatedCompanyId()).Where(x => x.CreatedBy == user.UserId);

            if (request.IsAscending)
            {
                switch (request.OrderBy)
                {
                    case "Status": query = query.OrderBy(l => l.Status); break;
                    default: query = query.OrderBy(l => l.Created); break;
                }
            }
            else
            {
                switch (request.OrderBy)
                {
                    case "Status": query = query.OrderByDescending(l => l.Status); break;
                    default: query = query.OrderByDescending(l => l.Created); break;
                }
            }

            query = query.Include(x => x.JournalType).Include(x => x.SubmittedBy).Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount);

            List<Journal> journals = query.ToList();

            foreach (var journal in journals)
            {
                gridData.Data.Add(new JournalViewModel(journal));
            }
            return gridData;
        }


        public GridResponseModel<JournalTypeViewModel> GetJournalTypeGridData(GridRequestModel request)
        {
            GridResponseModel<JournalTypeViewModel> gridData = new GridResponseModel<JournalTypeViewModel>();

            gridData.Count = _journalTypeRepository.GetAllActive(getCreatedCompanyId()).Count();

            var query = _journalTypeRepository.GetAllActive(getCreatedCompanyId());

            query = query.OrderByDescending(l => l.Created);

            query = query.Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount);

            List<JournalType> journalTypes = query.ToList();

            foreach (var journalTpe in journalTypes)
            {
                gridData.Data.Add(new JournalTypeViewModel(journalTpe));
            }
            return gridData;
        }

        public bool Update(Journal model)
        {
            var updateEntity = _repository.GetById(model.Id);
            updateEntity.Amount = model.Amount;
            updateEntity.Status = model.Status;
            updateEntity.Note = model.Note;
            var user = this.GetUserFromToken();
            updateEntity.Modified = DateTime.Now;
            updateEntity.ModifiedBy = user.Id;
            return _repository.Commit();

        }
        public bool UpdateJournalType(JournalType model)
        {
            var updateEntity = _journalTypeRepository.GetById(model.Id);
            updateEntity.Name = model.Name;
            updateEntity.Note = model.Note;
            var user = this.GetUserFromToken();
            updateEntity.Modified = DateTime.Now;
            updateEntity.ModifiedBy = user.Id;
            return _repository.Commit();
        }

        public bool SaveJournalType (JournalType model)
        {
            var user = GetUserFromToken();
            model = CreateFootPrintForEdit(model, user);
            _journalTypeRepository.Add(model);
            return _journalTypeRepository.Commit();
        }
        public JournalType CreateFootPrintForEdit(JournalType entity, UserViewModel user)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.Modified = DateTime.Now;
            entity.ModifiedBy = user.Id;
            entity.Created = DateTime.Now;
            entity.CreatedBy = user.Id;
            entity.CreatedCompany = user.CreatedCompany;
            entity.Active = true;
            entity.DeletedBy = null;
            entity.DeletionTime = null;
            return entity;
        }
        public bool MakeApprove(string id)
        {
            var updateEntity = _repository.GetById(id);
            updateEntity.Status = JournalStatus.approved;
            var user = this.GetUserFromToken();
            updateEntity.Modified = DateTime.Now;
            updateEntity.ModifiedBy = user.Id;
            return _repository.Commit();
        }
        public bool MakePaid(string id)
        {
            var updateEntity = _repository.GetById(id);
            updateEntity.Status = JournalStatus.paid;
            var user = this.GetUserFromToken();
            updateEntity.Modified = DateTime.Now;
            updateEntity.ModifiedBy = user.Id;
            return _repository.Commit();
        }
        
    }
}
