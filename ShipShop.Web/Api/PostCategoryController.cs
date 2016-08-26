﻿using ShipShop.Service;
using ShipShop.Web.Infrastructure.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShipShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    [Authorize]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService)
            : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        //public PostCategoryController()
        //{
        //}
        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listCategory = _postCategoryService.GetAll();
                //IMapper mapper = config.CreateMapper();
                //var listPostCategoryVM = Mapper.Map<List<PostCategoryViewModel>>(listCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategory);

                return response;
            });
        }
    }
}