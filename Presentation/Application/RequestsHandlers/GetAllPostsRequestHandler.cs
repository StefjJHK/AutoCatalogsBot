using BusinessLogic.DTO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;
using Presentation.ParametrObjects;
using Presentation.Application.Requests;

namespace Presentation.Application.RequestsHandlers
{
    public class GetAllPostsRequestHandler : IRequestHandler<GetAllPostsRequest, IEnumerable<PostDTO>>
    {
        private const ulong StartOffset = 0;
        private const ulong StepOffset = 100;

        private readonly IVkApi _api;
        private readonly ulong _groupId;


        public GetAllPostsRequestHandler(WallPostsParams param)
        {
            _api = param?.Api ?? 
                throw new ArgumentNullException(nameof(param));
            _groupId = param.Id;
        }

        public async Task<IEnumerable<PostDTO>> Handle(GetAllPostsRequest request, CancellationToken cancellationToken)
        {
            return GetAllPosts(_groupId);
        }

        #region private methods
        private IEnumerable<PostDTO> GetAllPosts(ulong groupId)
        {
            ulong offset = StartOffset;
            List<PostDTO> posts = new List<PostDTO>();
            
            while (true)
            {
                var tasks = new Task<IEnumerable<PostDTO>>[]
                {
                    new Task<IEnumerable<PostDTO>>(() => GetPosts(groupId, offset)),
                    new Task<IEnumerable<PostDTO>>(() => GetPosts(groupId, offset + StepOffset))
                };

                foreach (var task in tasks)
                {
                    task.Start();
                }

                Task.WaitAll(tasks);

                var result = tasks
                    .Select(task => task.Result)
                    .Aggregate((a, b) => a.Concat(b));

                if (!result.Any())
                {
                    break;
                }

                posts.AddRange(result);

                offset += (ulong)result.Count();
            }

            return posts;
        }
        
        private IEnumerable<PostDTO> GetPosts(ulong groupId, ulong index)
        {
            var posts = _api.Wall.Get(new WallGetParams
            {
                OwnerId = -Convert.ToInt32(groupId),
                Offset = index,
                Count = StepOffset,
            });

            //TOOD: CreatePostUrl(...)
            return posts.WallPosts
                .Select(post => new PostDTO($"https://vk.com/club{-post.OwnerId}?w=wall{post.OwnerId}_{post.Id}/all",
                    post.Text));
        }
        //TODO: Create method with name CreatePostUrl(...);
        #endregion
    }
}