﻿using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using ShipShop.Model.Models;
using ShipShop.Service;
using ShipShop.Web.App_Start;
using ShipShop.Web.Infrastructure.Extensions;
using ShipShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using ClosedXML.Excel;
using ShipShop.Web.Infrastructure.Core;
using ShipShop.Common;

namespace ShipShop.Web.Controllers
{
    public class OrderController : Controller
    {
        #region init
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;
        private IRegionService _regionService;
        private ApplicationUserManager _userManager;
        private int _pageSize;
        private int _maxPage;

        public OrderController(ApplicationUserManager userManager, IOrderService orderService, IOrderDetailService orderDetailService, IRegionService regionService)
        {
            UserManager = userManager;
            this._orderService = orderService;
            this._orderDetailService = orderDetailService;
            this._regionService = regionService;
            this._pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            this._maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
        } 
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion
        public ActionResult OrderDetail(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Title = "Chi tiết đơn hàng";
                var modelOrderDetail = _orderDetailService.GetAllByOrder(id, new string[] { "Order" });
                var query = Mapper.Map<IEnumerable<OrderDetailViewModel>>(modelOrderDetail);
                var order = _orderService.GetByID(id, new string[] { "ReceiverRegion", "SenderRegion" });
                var orderVM = Mapper.Map<OrderViewModel>(order);
                if (order.Username != User.Identity.Name)
                {
                    return Redirect("/");
                }
                ViewBag.Order = orderVM;
                return View(query);
            }
            else
            {
                return Redirect("/");
            }
        }
       
        // GET: Order
        public async Task<ActionResult> Index(int page = 1)
        {
           
            if (User.Identity.IsAuthenticated)
            {
                var typeOfOrder = Request["typeOfOrder"];

                int pageSize = this._pageSize;
                int totalCount = 0;
                int maxPage = this._maxPage;
                DateTime dtBeginDate = DateTime.MinValue;
                DateTime dtToDate = DateTime.MaxValue;
            
                if (Request["dtDenNgay"] != null)
                {
                    string denNgay = Request["dtDenNgay"].ToString();
                    ViewBag.dtDenNgay = denNgay;
                    if (denNgay != "")
                        dtToDate = DateTime.ParseExact(denNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(1);
                }

                if (Request["dtTuNgay"] != null)
                {
                    string tuNgay = Request["dtTuNgay"].ToString();
                    ViewBag.dtTuNgay = tuNgay;
                    if (tuNgay != "")
                        dtBeginDate = DateTime.ParseExact(tuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                if (Request["drdRegion"] != null)
                {
                    string drdRegion = Request["drdRegion"].ToString();
                    if (drdRegion != "")
                        ViewBag.drdRegion = new SelectList(_regionService.GetAll(), "RegionID", "Name", drdRegion);
                }
                else
                {
                    ViewBag.drdRegion = new SelectList(_regionService.GetAll(), "RegionID", "Name");
                }

                ViewBag.Title = "Quản trị tài khoản";
                var model = _orderService.GetAllByUserName(User.Identity.Name, dtBeginDate, dtToDate,page,pageSize,out totalCount, new string[] { "ReceiverRegion", "SenderRegion" });
                ViewBag.TypeOfOrder = 1;
                if (typeOfOrder == "2")
                {
                    var user = await UserManager.FindByNameAsync(User.Identity.Name);
                    if(user.Vendee)
                    {
                        
                           model = _orderService.GetAllBySenderMobile(User.Identity.Name, dtBeginDate, dtToDate, page, pageSize, out totalCount, new string[] { "ReceiverRegion", "SenderRegion" });
                    }
                    else
                    {
                        model = _orderService.GetAllByReceiverMobile(User.Identity.Name, dtBeginDate, dtToDate, page, pageSize, out totalCount, new string[] { "ReceiverRegion", "SenderRegion" });
                    }
                    ViewBag.TypeOfOrder = 2;
                }
                var query = Mapper.Map<IEnumerable<OrderViewModel>>(model);

                var currenUser = await _userManager.FindByNameAsync(User.Identity.Name);
                //ViewBag.CurrenUser = currenUser;
                ViewBag.Address = currenUser.Address;
                ViewBag.Vendee = currenUser.Vendee;
                ViewBag.WebsiteOrShop = currenUser.WebOrShopName;
                ViewBag.TotalCOD = query.Sum(x => x.PayCOD);
                int totalPage = (int)Math.Ceiling((double)totalCount / pageSize);
                var paginationSet = new PaginationSet<OrderViewModel>()
                {
                    Items = query,
                    MaxPage = maxPage,
                    Page = page,
                    TotalCount = totalCount,
                    TotalPages = totalPage,

                };

                return View(paginationSet);
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public async Task<JsonResult> CreateOrder(OrderHomePageViewModel orderHomePage)
        {
            if (User.Identity.IsAuthenticated)
            {
                var orderVM = orderHomePage.Order;
                var order = new Order();
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                order.UpdateOrder(orderVM);
                order.CreatedBy = User.Identity.Name;
                order.Username = User.Identity.Name;
                order.CreatedDate = DateTime.Now;
                order.Status = true;
                order.UserID = user.Id;

                _orderService.Add(order);
                _orderService.Save();

                var listOrderDetailVM = orderHomePage.listOrderDetail;
                if (listOrderDetailVM != null)
                {
                    OrderDetail orderDetail;
                    foreach (var item in listOrderDetailVM)
                    {
                        orderDetail = new OrderDetail();
                        orderDetail.UpdateOrderDetail(item);
                        orderDetail.OrderID = order.ID;
                        _orderDetailService.Add(orderDetail);
                    }
                    _orderDetailService.Save();
                }

                var response = new { Code = 1, Msg = "THành công" };
                return Json(response);
            }
            else
            {
                var response = new { Code = 0, Msg = "Thất bại" };
                return Json(response);
            }
        }

        [HttpPost]
        public JsonResult CancelOrder(OrderViewModel orderVM)
        {
            if (User.Identity.IsAuthenticated)
            {
                var order = _orderService.GetByID(orderVM.ID);
                if (order.Username == User.Identity.Name)
                {
                    order.Status = false;
                    _orderService.Update(order);
                    _orderService.Save();
                    return Json(new { Code = 1, Msg = "success" });
                }
            }
            return Json(new { Code = 0, Msg = "Faile" });

        }

        [HttpPost]
        public JsonResult ReCancelOrder(OrderViewModel orderVM)
        {
            if (User.Identity.IsAuthenticated)
            {
                var order = _orderService.GetByID(orderVM.ID);
                if (order.Username == User.Identity.Name)
                {
                    order.Status = true;
                    _orderService.Update(order);
                    _orderService.Save();
                    return Json(new { Code = 1, Msg = "success" });
                }
            }
            return Json(new { Code = 0, Msg = "Faile" });

        }

        public async Task<ActionResult> ExportExel()
        {
            System.IO.Stream spreadsheetStream = new System.IO.MemoryStream();
            XLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Danh sách đơn hàng");


            worksheet.Cell(1, 1).SetValue("Chức năng đang chờ hoàn thiện");
            //worksheet.Cells()
            workbook.SaveAs(spreadsheetStream);

            spreadsheetStream.Position = 0;

            return new FileStreamResult(spreadsheetStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = "DanhSachDonHang.xlsx" };
        }

        public bool UpdateValue(IXLWorksheet ixlWorkSheet, int row, string col, object value, int alignment, int fontStyle, bool isFormula)
        {
            if (value == null || (value.GetType() == typeof(string) && value == "") || (value.GetType() == typeof(string) && value == "0"))
            {
                return false;
            }
            if (value.GetType() == typeof(double) && Convert.ToDouble(value) == 0)
            {
                return false;
            }
            bool updated = false;

            IXLWorksheet xlsheet = ixlWorkSheet;

            if (xlsheet != null)
            {
                //if (value.GetType() == typeof(double))
                //{
                //    if (flagDonViTinh == DonViTinh.DVT_TrieuDong)
                //        value = Convert.ToDouble(value) / 1000000;
                //    else if (flagDonViTinh == DonViTinh.DVT_TyDong)
                //        value = Convert.ToDouble(value) / 1000000000;
                //    else if (flagDonViTinh == DonViTinh.DVT_Nghin)
                //        value = Convert.ToDouble(value) / 1000;
                //}

                IXLCell cell = xlsheet.Cell(row, col);
                cell.Value = value.ToString();
                if (isFormula == true)
                    cell.FormulaA1 = value.ToString();
                if (alignment > 0)
                {
                    cell.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    cell.Style.Alignment.SetHorizontal((XLAlignmentHorizontalValues)alignment);
                }
                if (fontStyle > 0)
                {
                    cell.Style.Font.Bold = (fontStyle & 1) == 1 ? true : false;
                    cell.Style.Font.Italic = (fontStyle & 2) == 2 ? true : false;
                    cell.Style.Font.Underline = (fontStyle & 3) == 3 ? XLFontUnderlineValues.Single : XLFontUnderlineValues.None;
                }
                updated = true;
            }

            return updated;
        }
    }
}