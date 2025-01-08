using B1SLayer;
using SamartWMSSAPWrapperApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamartWMSSAPWrapperApi
{
    public sealed class ServiceLayer
    {
        private static readonly SLConnection _serviceLayer = new SLConnection(GlobalVar.Host, GlobalVar.CompanyDB, GlobalVar.Username, GlobalVar.Password);
        static ServiceLayer() { }
        private ServiceLayer() { }
        public static SLConnection Connection => _serviceLayer;
    }
}