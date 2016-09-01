using AutoMapper;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Web.Infrastructure.Extensions;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShipShop.Web.Api
{
    [RoutePrefix("api/webInformation")]
    [Authorize]
    public class WebInformationController : ApiControllerBase
    {
        #region Init Controller

        private IWebInformationService _webInformationService;

        public WebInformationController(IErrorService errorService, IWebInformationService webInformationService)
            : base(errorService)
        {
            this._webInformationService = webInformationService;
        }
        #endregion

        [Route("getsinger")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_WEBINFO + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage GetSinger(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var data = _webInformationService.GetSingle();
                var responseData = Mapper.Map<WebInformation, WebInformationViewModel>(data);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);
                return response;
            });
        }

        [Route("Update")]
        [HttpPut]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_WEBINFO + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Update(HttpRequestMessage request, WebInformationViewModel webInformationVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, ModelState);
                }
                else
                {
                    var webInformation = _webInformationService.GetSingle();
                    webInformation.UpdateWebInformation(webInformationVM);
                    _webInformationService.Update(webInformation);
                    _webInformationService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, "");
                }

                return response;
            });
        }

    }
}
