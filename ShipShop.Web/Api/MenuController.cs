﻿using AutoMapper;
using ShipShop.Service;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System;
using ShipShop.Model.Models;
using ShipShop.Web.Infrastructure.Extensions;

namespace ShipShop.Web.Api
{
    [RoutePrefix("api/menu")]
    [Authorize]
    public class MenuController : ApiControllerBase
    {
        #region Init Controller

        private IMenuService _menuService;
        private IMenuGroupService _menuGroupService;

        public MenuController(IErrorService errorService, IMenuService menuService, IMenuGroupService menuGroupService)
            : base(errorService)
        {
            this._menuService = menuService;
            this._menuGroupService = menuGroupService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_GET_LIST_MENU + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int total = 0;
                var model = _menuService.GetAll();
                total = model.Count();
                var query = model.OrderBy(x => x.MenuGroup.ID).ThenBy(x => x.DisplayOrder).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<MenuViewModel>>(query);

                var paginationSet = new PaginationSet<MenuViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = total,
                    TotalPages = (int)Math.Ceiling((decimal)total / pageSize),
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [Authorize(Roles = Common.RolesConstants.ROLES_ADD_MENU + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Create(HttpRequestMessage request, MenuViewModel menuVM)
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
                    var newMenu = new Menu();
                    newMenu.UpdateMenu(menuVM);
                    _menuService.Add(newMenu);
                    _menuService.Save();
                    var responseData = Mapper.Map<Menu, MenuViewModel>(newMenu);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("getallgroup")]
        [HttpGet]

        public HttpResponseMessage GetAllGroup(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var query = _menuGroupService.GetAll();

                var responseData = Mapper.Map<List<MenuGroupViewModel>>(query);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_MENU + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var newMenu = _menuService.GetByID(id);
                var responseData = Mapper.Map<Menu, MenuViewModel>(newMenu);
                response = request.CreateResponse(HttpStatusCode.Created, responseData);

                return response;
            });
        }

        [Route("Update")]
        [HttpPut]
        [Authorize(Roles = Common.RolesConstants.ROLES_EDIT_MENU + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Update(HttpRequestMessage request, MenuViewModel menuVM)
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
                    var newMenu = _menuService.GetByID(menuVM.ID);
                    newMenu.UpdateMenu(menuVM);
                    _menuService.Update(newMenu);
                    _menuService.Save();
                    var responseData = Mapper.Map<Menu, MenuViewModel>(newMenu);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("Delete")]
        [HttpDelete]
        [Authorize(Roles = Common.RolesConstants.ROLES_DELETE_MENU + "," + Common.RolesConstants.ROLES_FULL_CONTROL)]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    _menuService.Delete(id);
                    _menuService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, "");
                }

                return response;
            });
        }
    }
}