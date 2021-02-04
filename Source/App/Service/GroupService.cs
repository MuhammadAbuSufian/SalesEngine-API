using System;
using System.Collections.Generic;
using System.Linq;
using Project.Model;
using Project.Repository;
using Project.RequestModel;
using Project.ViewModel;

namespace Project.Service
{
    public interface IGroupService : IBaseService<Group, GroupViewModel>
    {
        GridResponseModel<GroupViewModel> GetGridData(GridRequestModel request);

        List<DropdownViewModel> GetDropdownData(string key);

        bool UpdateGroup(Group group);

        string RestoreGroup(string groupName);
    }

    public class GroupService : BaseService<Group, GroupViewModel>, IGroupService
    {
        private readonly IGroupRepository _repository;

        public GroupService(IGroupRepository repository) : base(repository)
        {
            _repository = repository;

        }

        public GridResponseModel<GroupViewModel> GetGridData(GridRequestModel request)
        {
            GridResponseModel<GroupViewModel> gridData = new GridResponseModel<GroupViewModel>();

            gridData.Count = _repository.GetAllActive(getCreatedCompanyId()).Count();

            var query = _repository.GetAllActive(getCreatedCompanyId());

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            if (request.IsAscending)
            {
                switch (request.OrderBy)
                {
                    case "Name": query = query.OrderBy(l => l.Name); break;
                    default: query = query.OrderBy(l => l.Created); break;
                }
            }
            else
            {
                switch (request.OrderBy)
                {
                    case "Name": query = query.OrderByDescending(l => l.Name); break;
                    default: query = query.OrderByDescending(l => l.Created); break;
                }
            }


            query = query.Skip(((request.Page - 1) * request.PerPageCount)).Take(request.PerPageCount);

            List<Group> groups = query.ToList();

            foreach (var group in groups)
            {
                gridData.Data.Add(new GroupViewModel(group));
            }

            return gridData;
        }

        public List<DropdownViewModel> GetDropdownData(string key)
        {
            return _repository.GetAllActive(getCreatedCompanyId()).Where(x => x.Name.Contains(key)).Select(
                x =>
                    new DropdownViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }
            ).Take(10).ToList();
        }

        public bool UpdateGroup(Group group)
        {
            var updateGroup = _repository.GetById(group.Id);
            updateGroup.Name = group.Name;
            updateGroup.Description = group.Description;

            var user = this.GetUserFromToken();

            updateGroup.Modified = DateTime.Now;
            updateGroup.ModifiedBy = user.Id;

            return _repository.Commit();

        }

        public string RestoreGroup(string groupName)
        {
            var group = _repository.GetAllActive().FirstOrDefault(x => x.Name == groupName);

            if (group != null)
            {
                return group.Id;
            }
            else
            {
                Group newGroup = new Group();
                newGroup.Name = groupName;
                return AddwithReturnId(newGroup).Id;
            }
        }
    }

}