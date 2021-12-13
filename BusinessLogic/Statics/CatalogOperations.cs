using BusinessLogic.DTO;
using System.Linq;

namespace BusinessLogic.Statics
{
    public static class CatalogOperations
    {
        public static CatalogDTO CombineCatalogs(CatalogDTO parsedCatalog, CatalogDTO storedCatalog)
        {
            var groups = parsedCatalog.TitlesGroups
                .Concat(storedCatalog.TitlesGroups)
                .GroupBy(groups => groups.Letter)
                .Select(g =>
                {
                    if (g.Count() == 1)
                    {
                        return g.First();
                    }
                    else
                    {
                        var groups = g.ToArray();
                        return CombineGroups(groups[0], groups[1]);
                    }
                });

            return new CatalogDTO(parsedCatalog.Discussion, groups);
        }

        #region private methods
        private static TitlesGroupDTO CombineGroups(TitlesGroupDTO parsedGroup, TitlesGroupDTO storedGroup)
        {
            var titles = parsedGroup.Titles
                .Concat(storedGroup.Titles)
                .GroupBy(title => title.Tag)
                .Select(group =>
                {
                    if (group.Count() == 1)
                    {
                        return group.First();
                    }
                    else
                    {
                        var titles = group.ToArray();
                        return CombineTitles(titles[0], titles[1]);
                    }
                });

            return new TitlesGroupDTO(parsedGroup.Letter, titles);
        }

        private static TitleDTO CombineTitles(TitleDTO title_1, TitleDTO title_2)
        {
            return new TitleDTO(
                title_1.Name, 
                title_1.Tag, 
                title_1.Count + title_2.Count);
        }
        #endregion
    }
}