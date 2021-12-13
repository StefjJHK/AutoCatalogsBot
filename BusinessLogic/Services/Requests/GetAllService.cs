using BusinessLogic.DTO;
using BusinessLogic.IRepositories;
using BusinessLogic.ParamterObjects.Service.Requests;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Services.Requests
{
    public class GetAllService : IRequestService<GetAllRequest, IEnumerable<CatalogDTO>>
    {
        private readonly ICatalogsRepository _catalogsRepository;

        public GetAllService(ICatalogsRepository catalogsRepository)
        {
            _catalogsRepository = catalogsRepository ??
                throw new ArgumentNullException(nameof(catalogsRepository));
        }

        public IEnumerable<CatalogDTO> Execute(GetAllRequest request)
        {
            return _catalogsRepository.GetAll();
        }
    }
}
