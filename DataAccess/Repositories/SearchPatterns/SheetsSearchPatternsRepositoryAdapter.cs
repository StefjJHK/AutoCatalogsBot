using System;
using System.Linq;
using System.Collections.Generic;
using BusinessLogic.Utility;
using BusinessLogic.IRepositories;
using BusinessLogic.ParametrObjects;

namespace DataAccess.Repositories.SearchPatterns
{
    public class SheetsSearchPatternsRepositoryAdapter : ISearchPatternsRepository
    {
        private readonly ISheetsSearchPatternsRepository _sheetsSearchPatternsRepository;
        private readonly SheetPatternsParams _patternsParams;

        public SheetsSearchPatternsRepositoryAdapter(ISheetsSearchPatternsRepository searchPatternsRepository,
            SheetPatternsParams patternsParams)
        {
            _sheetsSearchPatternsRepository = searchPatternsRepository ?? 
                throw new ArgumentNullException(nameof(searchPatternsRepository));
            _patternsParams = patternsParams ??
                 throw new ArgumentNullException(nameof(patternsParams));
        }

        IEnumerable<SearchPatternDTO> ISearchPatternsRepository.GetAll()
        {
            var rows = _sheetsSearchPatternsRepository.Get(_patternsParams.Sheet);

            return rows
                .Select(row => new SearchPatternDTO(
                    (string)row[_patternsParams.Kind],
                    (string)row[_patternsParams.Name],
                    (string)row[_patternsParams.Tag]));
        }
    }
}
