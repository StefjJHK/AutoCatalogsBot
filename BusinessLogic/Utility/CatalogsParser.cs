using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BusinessLogic.DTO;
using BusinessLogic.ParametrObjects;
using BusinessLogic.Statics;
using BusinessLogic.IRepositories;
using BusinessLogic.ParametrObjects.Notifications;
using BusinessLogic.Notifications;

namespace BusinessLogic.Utility
{
    public class CatalogsParser
    {
        const string TagSign = "sign";

        private readonly INotificationHandler<PostParseFailed> _notificationHandler;
        private readonly IDiscussionsRepository _discussionsRepository;
        private readonly ISearchPatternsRepository _searchPatternsRepository;

        public CatalogsParser(ISearchPatternsRepository searchPatternsRepository, 
            IDiscussionsRepository discussionsRepository, INotificationHandler<PostParseFailed> notificationHandler)
        {
            _searchPatternsRepository = searchPatternsRepository ?? 
                throw new ArgumentNullException(nameof(searchPatternsRepository));
            _discussionsRepository = discussionsRepository ??
                throw new ArgumentNullException(nameof(discussionsRepository));
            _notificationHandler = notificationHandler ??
                throw new ArgumentException(nameof(notificationHandler));
        }

        public IEnumerable<CatalogDTO> Parse(IEnumerable<PostDTO> posts)
        {
            Dictionary<string, DiscussionDTO> discussions = _discussionsRepository.GetAll()
                .ToDictionary(discussion => discussion.Kind,
                              discussion => discussion);
            
            var groupsTitles = FormTitlesGroupsdByKind(posts)
                .ToDictionary(g => g.Key,
                              g => FormTitlesGroups(g.Value));

            return groupsTitles.
                Select(g => new CatalogDTO(discussions[g.Key], g.Value.ToList()));
        }

        #region private methods
        private Dictionary<string, IEnumerable<TitleDTO>> FormTitlesGroupsdByKind(IEnumerable<PostDTO> posts)
        {
            IEnumerable<SearchPatternDTO> patterns = _searchPatternsRepository.GetAll();

            return posts
                .Select(post => ExtractTitleFields(post, patterns))
                .Where(title => title != null)
                .GroupBy(title => title.Kind)
                .ToDictionary(g => g.Key,
                              g => FormTitle(g));
        }

        private IEnumerable<TitleDTO> FormTitle(IEnumerable<TitleFields> fields)
        {
            return fields
                .GroupBy(g => g.Name)
                .Select(g =>
                {
                    var fields = g.FirstOrDefault();
                    return new TitleDTO(fields.Name, fields.Tag, g.Count());
                });
        }

        private IEnumerable<TitlesGroupDTO> FormTitlesGroups(IEnumerable<TitleDTO> titles)
        {
            return titles
                .GroupBy(title => title.Name.FirstOrDefault())
                .Select(g => new TitlesGroupDTO(g.Key, g.Select(t => t).ToList()));
        }

        private TitleFields ExtractTitleFields(PostDTO post, IEnumerable<SearchPatternDTO> patterns)
        {
            var titleFields = patterns
                .Select(pattern =>
                    new TitleFields(FindField(pattern.PatternKind, post.Text),
                                 FindField(pattern.PatternName, post.Text),
                                 FindField(pattern.PatternTag, post.Text)))
                .OrderByDescending(title => TitleFieldsOperations.CountNonNullFields(title))
                .FirstOrDefault();

            if (TitleFieldsOperations.IsFull(titleFields))
            {
                return titleFields;
            }
            else if(!TitleFieldsOperations.IsEmpty(titleFields))
            {
                _notificationHandler.Handle(new PostParseFailed(post));
            }

            return default;
        }

        private string FindField(string pattern, string text)
        {
            string output = null;

            if (!string.IsNullOrEmpty(pattern))
                output = Regex.Match(text, pattern, RegexOptions.IgnoreCase).Groups[TagSign].Value;

            return output;
        }
        #endregion
    }
}