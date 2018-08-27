using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs;
using LibrarySystem.Models;
using Repository;
using Entities;
using Repository.Helpers;

namespace LibrarySystem.Controllers
{
    public class GeneralSettings : Controller
    {
        private IRepository<InstituteSettingModel> _repository;

        public GeneralSettings(IRepository<InstituteSettingModel> repository)
        {
            _repository = repository;
        }

        // GET: /<controller>/
        [Breadcrumb("General Settings", CacheTitle = true, FromAction = "Index", FromController = "Home")] 
        public IActionResult Index()
        {
            Counter _counter = _repository.NextCountSequence("institute_id");

            InstituteSettingModel _model = new InstituteSettingModel()
            {
                institute_address = "Some Address",
                institute_currency = "VSB",
                institute_email = "Mt Email",
                institute_favicon = "Some Link",
                institute_fk_currency = "2",
                institute_id = _counter.Value,
                institute_is_active = true,
                institute_logo = "lInk again",
                institute_name = "Name",
                institute_phone = "empty",
                institute_terms_conditions = "condtion"
            };

            _repository.Insert(_model);

            var getInfo = _repository.FindAll();
            return View();
        }
    }
}
