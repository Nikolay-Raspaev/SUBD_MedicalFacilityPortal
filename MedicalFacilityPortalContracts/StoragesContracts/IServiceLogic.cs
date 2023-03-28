﻿using MedicalFacilityPortalContracts.BindingModels;
using MedicalFacilityPortalContracts.SearchModels;
using MedicalFacilityPortalContracts.ViewModels;

namespace MedicalFacilityPortalContracts.BusinessLogicsContracts
{
    public interface IServiceStorage
    {
        List<ServiceViewModel> GetFullList();
        List<ServiceViewModel> GetFilteredList(ServiceSearchModel model);
        ServiceViewModel? GetElement(ServiceSearchModel model);
        ServiceViewModel? Insert(ServiceBindingModel model);
        ServiceViewModel? Update(ServiceBindingModel model);
        ServiceViewModel? Delete(ServiceBindingModel model);
    }
}