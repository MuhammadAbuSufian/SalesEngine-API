using Project.Model;
using Project.Repository;
using Project.ViewModel;

namespace Project.Service
{
    public interface ISaleDetailsService : IBaseService<SalesDetail, SalesDetailViewModel>
    {

    }

    public class SaleDetailsService : BaseService<SalesDetail, SalesDetailViewModel>, ISaleDetailsService
    {
        private readonly ISaleDetailsRepository _repository;

        public SaleDetailsService(ISaleDetailsRepository repository) : base(repository)
        {
            _repository = repository;

        }
    }
}