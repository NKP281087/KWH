using KWH.BAL.IRepository;
using KWH.DAL.DataContext;
using KWH.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.BAL.RepositoryImplementation
{
    public class AdminBALService : IAdminBALService 
    {
        private readonly KWHContext _context;

        public AdminBALService(KWHContext context)
        {
            _context = context;
        }
        public async Task<RFId> GetRFById(int id)
        {
            var RFData = await _context.RFId.FirstOrDefaultAsync(x => x.RFIdNo == id);
            if (RFData != null)
            {
                return RFData;
            }
            return null;
        }

        public async Task<IEnumerable<RFId>> GetAllRFData()
        {
            return await _context.RFId.ToListAsync();
        }

        public async Task<RFId> SubmitRFData(RFId entity)
        {
            RFId rFId = new RFId
            {
                TimeIn = entity.TimeIn,
                TimeOut = entity.TimeOut,
                IsActive = entity.IsActive,
                OnHold = false,
                CreatedOn= DateTime.Now,
                CreatedBy= entity.CreatedBy,
            };

            await _context.AddAsync(rFId);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateRFData(int Id, RFId entity)
        {
            var data = await _context.RFId.FindAsync(Id);
            if(data != null || data.RFIdNo !=Id)
            {
                return false;
            }

            data.TimeIn = entity.TimeIn;
            data.TimeOut = entity.TimeOut;
            data.CreatedBy = entity.CreatedBy;
            data.CreatedOn = DateTime.Now;
            data.IsActive = entity.IsActive;
            data.OnHold = entity.OnHold;

            if(data != null)
            {
                _context.RFId.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
        public async Task<IEnumerable<Section>> GetAllSectionData()
        {
            var data = await _context.Section.ToListAsync();
            if (data == null)
            {
                return Enumerable.Empty<Section>();
            }
            return data;
        }
        public async Task<Section> GetSectionById(Guid SectionId)
        {
            var data = await _context.Section.Where(x => x.SectionId == SectionId).FirstOrDefaultAsync();
            if (data != null)
            {
                return data;
            }
            return null;
        }
        public async Task<Section> SaveSectionData(Section entity)
        {
            Section model = new Section
            {
                SectionId = entity.SectionId,
                SectionName = entity.SectionName,
                IsActive = true,
                CreatedOn = DateTime.Now,
                IsDeleted = false,
                OnHold = false
            };

            var obj = await _context.Section.AddAsync(model);
            _context.SaveChanges();
            return obj.Entity;
        }

        public async Task<bool> UpdateSectionData(Guid Id, Section entity)
        {
            var data = await _context.Section.FindAsync(Id);
            if (data == null || data.SectionId != Id)
            {
                return false;
            } 

            data.SectionName = entity.SectionName;
            data.ModifiedDate = DateTime.Now;
            _context.Section.Update(data);
            await _context.SaveChangesAsync();
            return true;
        }
        

    }
}
