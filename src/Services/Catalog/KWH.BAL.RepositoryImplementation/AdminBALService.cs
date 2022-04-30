using AutoMapper;
using KWH.BAL.IRepository;
using KWH.Common.ViewModel;
using KWH.DAL.DataContext;
using KWH.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.BAL.RepositoryImplementation
{
    public class AdminBALService : IAdminBALService
    {
        private readonly KWHDBContext _context;
        private readonly IMapper _mapper;

        public AdminBALService(KWHDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var RfData = await _context.RFId.FromSqlRaw<RFId>("Execute usp_GetRFData").ToListAsync();
            return RfData;
            //return await _context.RFId.ToListAsync();
        }

        public async Task<RFId> SubmitRFData(RFId entity)
        {
            RFId rFId = new RFId
            {
                TimeIn = entity.TimeIn,
                TimeOut = entity.TimeOut,
                IsActive = entity.IsActive,
                OnHold = false,
                CreatedOn = DateTime.Now,
                CreatedBy = entity.CreatedBy,
            };

            await _context.AddAsync(rFId);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateRFData(int Id, RFId entity)
        {
            var data = await _context.RFId.FindAsync(Id);
            if (data != null || data.RFIdNo != Id)
            {
                return false;
            }

            data.TimeIn = entity.TimeIn;
            data.TimeOut = entity.TimeOut;
            data.CreatedBy = entity.CreatedBy;
            data.CreatedOn = DateTime.Now;
            data.IsActive = entity.IsActive;
            data.OnHold = entity.OnHold;

            if (data != null)
            {
                _context.RFId.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
        public async Task<IEnumerable<Section>> GetAllSectionData()
        {
            var data = await _context.Section.FromSqlRaw<Section>("Execute usp_GetSectionData").ToListAsync();
            //var data = await _context.Section.Where(x => x.IsActive == true).ToListAsync();
            //if (data == null || data.Count==0)
            //{
            //     return Enumerable.Empty<Section>();

            //}
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
        public async Task<bool> SaveSectionData(Section entity)
        {
            try
            {
                var data = await _context.Section.Where(x => x.SectionName.ToUpper().Trim() == entity.SectionName.ToUpper().Trim() && x.IsActive == true).FirstOrDefaultAsync();
                if (data != null)
                {
                    return false;
                }
                Section model = new Section
                {
                    SectionId = entity.SectionId,
                    SectionName = entity.SectionName,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false,
                    OnHold = false
                };

                await _context.Section.AddAsync(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var dd = ex;
                return false;
            }
        }

        public async Task<bool> UpdateSectionData(Section entity)
        {
            var data = await _context.Section.FindAsync(entity.SectionId);
            if (data == null || data.SectionId != entity.SectionId)
            {
                return false;
            }
            else if (data.SectionName.ToUpper().Trim() == entity.SectionName.ToUpper().Trim())
            {
                return false;
            }

            data.SectionName = entity.SectionName;
            data.ModifiedDate = DateTime.Now;
            _context.Section.Update(data);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteSectionData(Guid Id)
        {
            var data = await _context.Section.FindAsync(Id);
            if (data == null || data.SectionId != Id)
            {
                return false;
            }
            data.IsActive = false;
            _context.Section.Update(data);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<ClassMasterViewModel>> GetAllClassMasterData()
        {            
            var result =  await _context.ClassMaster.Where(x => x.IsActive == true).ToListAsync();
            var  data = _mapper.Map<IEnumerable<ClassMasterViewModel>>(result);
            return data;
        }

        public async Task<ClassMaster> GetClassMasterById(Guid Id)
        {
            var data = await _context.ClassMaster.Where(x => x.ClassId == Id).FirstOrDefaultAsync();
            return data;
        }
        public async Task<bool> SaveClassData(ClassMaster entity)
        {

            var data = await _context.ClassMaster.Where(x =>
                              x.ClassName.ToUpper().Trim() == entity.ClassName.ToUpper().Trim()
                              && x.SectionId == entity.SectionId
                              ).FirstOrDefaultAsync();
            if (data != null)
            {
                return false;
            }

            ClassMaster _classModel = new ClassMaster()
            {
                ClassId = Guid.NewGuid(),
                SectionId = entity.SectionId,
                ClassName = entity.ClassName,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            await _context.ClassMaster.AddAsync(_classModel);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateClassData(ClassMaster entity)
        {
            var data = await _context.ClassMaster.FindAsync(entity.ClassId);
            if (data == null || data.ClassId != entity.ClassId)
            {
                return false;
            }
            data.ClassName = entity.ClassName;
            data.SectionId = entity.SectionId;
            data.ModifiedDate = DateTime.Now;

            _context.ClassMaster.Update(data);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteClassData(Guid Id)
        {
            var data = await _context.ClassMaster.FindAsync(Id);
            if (data == null || data.ClassId != Id)
            {
                return false;
            }
            data.IsActive = false;
            data.IsDeleted = true;
            _context.ClassMaster.Update(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
