using AutoMapper;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Services.Infrastructure;
using MealOrdering.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealOrdering.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace MealOrdering.Server.Services.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly MealOrderingDbContext context;
        private readonly IMapper mapper;

        public SupplierService(MealOrderingDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<SupplierDTO> CreateSupplier(SupplierDTO supplier)
        {
            var dbSupplier = mapper.Map<Suppliers>(supplier);
            await context.AddAsync(dbSupplier);
            await context.SaveChangesAsync();

            return mapper.Map<SupplierDTO>(dbSupplier);
        }

        public async Task DeleteSupplier(Guid SupplierId)
        {
            var Supplier = await context.Suppliers.FirstOrDefaultAsync(i => i.Id == SupplierId);
            if (Supplier == null)
                throw new Exception("Restorant bulunamadı");

            int orderCount = await context.Suppliers.Include(i => i.Orders).Select(i => i.Orders.Count).FirstOrDefaultAsync();

            if (orderCount > 0)
                throw new Exception($"Silmeye çalıştığınız restorant için oluşturulmuş {orderCount} adet sipariş mevcut.");

            context.Suppliers.Remove(Supplier);
            await context.SaveChangesAsync();
        }

        public async Task<SupplierDTO> GetSupplierById(Guid Id)
        {
            return await context.Suppliers.Where(i => i.Id == Id)
                      .ProjectTo<SupplierDTO>(mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }

        public async Task<List<SupplierDTO>> GetSuppliers()
        {
            var list = await context.Suppliers//.Where(i => i.IsActive)
                     .ProjectTo<SupplierDTO>(mapper.ConfigurationProvider)
                     .OrderBy(i => i.CreateDate)
                     .ToListAsync();

            return list;
        }

        public async Task<SupplierDTO> UpdateSupplier(SupplierDTO supplier)
        {
            var dbSupplier = await context.Suppliers.FirstOrDefaultAsync(i => i.Id == supplier.Id);
            if (dbSupplier == null)
                throw new Exception("Restorant Bulunamadı");

            mapper.Map(supplier, dbSupplier);
            await context.SaveChangesAsync();

            return mapper.Map<SupplierDTO>(dbSupplier);
        }
    }
}
