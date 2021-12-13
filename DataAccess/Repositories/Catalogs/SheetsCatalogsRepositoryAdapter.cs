using System.Linq;
using System.Collections.Generic;
using BusinessLogic.IRepositories;
using BusinessLogic.ParametrObjects;
using BusinessLogic.DTO;

namespace DataAccess.Repositories.Catalogs
{
    public class SheetsCatalogsRepositoryAdapter : ICatalogsRepository
    {
        private struct CatalogRow
        {
            public string Name { get; init; }
            public string Tag { get; init; }
            public int Count { get; init; }

            public CatalogRow(string name, string tag, int count)
            {
                Name = name;
                Tag = tag;
                Count = count;
            }
        }

        private readonly ISheetsCatalogsRepository _catalogsRepository;
        private readonly IDiscussionsRepository _discussionsRepository;

        private readonly SheetCatalogParams _catalogParams;

        public SheetsCatalogsRepositoryAdapter(ISheetsCatalogsRepository catalogsRepository,
            SheetCatalogParams catalogParams, IDiscussionsRepository discussionsRepository)
        {
            _catalogsRepository = catalogsRepository;
            _discussionsRepository = discussionsRepository;
            _catalogParams = catalogParams;
        }

        #region public methods
        public void RefreshAll(IEnumerable<CatalogDTO> catalogs)
        {
            var sheets = _discussionsRepository.GetAll()
                .Select(discussion => discussion.Kind);

            foreach(var sheet in sheets)
            {
                _catalogsRepository.Refresh(sheet, new List<IList<object>>());
            }

            foreach (CatalogDTO catalog in catalogs)
            {
                Refresh(catalog);
            }
        }

        public void Refresh(CatalogDTO catalog)
        {
            var catalogRows = ConvertToCatalogRows(catalog);

            _catalogsRepository.Refresh(catalog.Discussion.Kind, catalogRows);
        }

        public IEnumerable<CatalogDTO> GetAll()
        {
            var discussions = _discussionsRepository.GetAll()
                .Select(discussion => discussion.Kind);

            var values = _catalogsRepository.Get(discussions);
            var catalogsRows = values
                .Select(catalog =>
                {
                    var catalogRows = ConvertToCatalogRows(catalog.Value);

                    return new
                    {
                        Key = catalog.Key,
                        Values = ConvertToTitlesGroups(catalogRows)
                    };
                });

            return catalogsRows
                .Select(catalogRows => new CatalogDTO(
                    _discussionsRepository.GetByKind(catalogRows.Key),
                    catalogRows.Values));
        }

        public CatalogDTO GetByKind(string kind)
        {
            var values = _catalogsRepository.GetBySheet(kind);
            var catalogRows = ConvertToCatalogRows(values);
            var titlesGroups = ConvertToTitlesGroups(catalogRows);

            return new CatalogDTO(_discussionsRepository.GetByKind(kind), titlesGroups);
        }
        #endregion

        #region private methods
        private IEnumerable<TitlesGroupDTO> ConvertToTitlesGroups(IEnumerable<CatalogRow> catalogRows)
        {
            return catalogRows
                .GroupBy(row => row.Name.FirstOrDefault())
                .Select(group => new TitlesGroupDTO(
                    group.Key,
                    group.Select(t => new TitleDTO(t.Name, t.Tag, t.Count))));
        }

        private IEnumerable<CatalogRow> ConvertToCatalogRows(IEnumerable<IList<object>> values)
        {
            return values.Select(values =>
                new CatalogRow(
                    (string)values[_catalogParams.Name],
                    (string)values[_catalogParams.Tag],
                    int.Parse((string)values[_catalogParams.Count])));
        }

        private List<IList<object>> ConvertToCatalogRows(CatalogDTO catalog)
        {
            return catalog.TitlesGroups
                .Select(group => ConvertToGroupRows(group))
                .Aggregate((summary, rows) => summary
                                                .Concat(rows)
                                                .ToList());
        }

        private List<IList<object>> ConvertToGroupRows(TitlesGroupDTO groupDTO)
        {
            var rows = groupDTO.Titles
                .Select(title => FormRow(title))
                .ToList();

            rows.First()[0] = groupDTO.Letter;

            return rows;
        }

        private IList<object> FormRow(TitleDTO title)
        {
            return new List<object> {
                "",
                title.Tag,
                title.Name,
                title.Count
            };
        }
        #endregion
    }
}