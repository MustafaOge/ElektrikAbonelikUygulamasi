﻿using AutoMapper;
using EduPortal.Application.Interfaces.Services;
using EduPortal.Domain.Entities;
using EduPortal.MVC.Models.Invoice;
using EduPortal.Persistence.context;
using MFramework.Services.FakeData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Linq;

namespace EduPortal.MVC.Controllers
{
    public class InvoiceController
        (AppDbContext appDbContext,
        IToastNotification toast,
        IFakeDataService fakeDataService,
        IInvoiceService invoiceService,
        IMapper mapper

        )
        : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateFakeData()
        {
            fakeDataService.CreateFakeInvoiceData();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> PayInvoice(int id)
        {
            await invoiceService.PayInvoice(id);
            return RedirectToAction("Index");
        }
        //[Authorize]
        public IActionResult Find()
        {
            return View();
        }

        public IActionResult PaymentIndividual()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PaymentIndividual(string identityNumber)
        {
            var response = await invoiceService.PaymentIndividual(identityNumber);
            return View(response.Data);
        }

        [HttpGet]
        public IActionResult PaymentCorporate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PaymentCorporate(string taxIdNumber)
        {
            var response = await invoiceService.PaymentCorporate(taxIdNumber);
            return View(response.Data);
        }

        public async Task<IActionResult> InvoiceList(int id)
        {
            var subsInvoices = await invoiceService.GetInvoiceDetail(id);
            return View(subsInvoices.Data);
        }

        public async Task<IActionResult> DetailPay(int id)
        {
            var invoiceDetail = await invoiceService.DetailPay(id);
            return View(invoiceDetail.Data);
        }

        [HttpGet]
        public IActionResult Complaint()
        {
            return View();
        }

        public async Task<IActionResult> Complaint(InvoiceComplaint model)
        {
            var complaintApplication = await invoiceService.CreateComplaint(model);
            if (complaintApplication.StatusCode != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(complaintApplication);
            }
        }
    }
}
