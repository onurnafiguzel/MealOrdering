using MealOrdering.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Infrastructure
{
    public interface ISupplierService
    {
        public Task<List<SupplierDTO>> GetSuppliers();

        public Task<SupplierDTO> CreateSupplier(SupplierDTO supplier);

        public Task<SupplierDTO> UpdateSupplier(SupplierDTO supplier);

        public Task DeleteSupplier(Guid SupplierId);

        public Task<SupplierDTO> GetSupplierById(Guid Id);
    }
}
