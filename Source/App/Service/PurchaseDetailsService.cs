using Project.Model;
using Project.Repository;
using Project.ViewModel;

namespace Project.Service
{
    public interface IPurchaseDetailsService : IBaseService<PurchaseDetail, PurchaseDetailViewModel>
    {

    }

    public class PurchaseDetailsService : BaseService<PurchaseDetail, PurchaseDetailViewModel>, IPurchaseDetailsService
    {
        private readonly IPurchaseDetailsRepository _repository;

        public PurchaseDetailsService(IPurchaseDetailsRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}