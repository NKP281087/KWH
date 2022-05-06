using AutoMapper;
using KWH.BAL.IRepository;
using KWH.Common.ViewModel;
using KWH.DAL.DataContext;
using KWH.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data;

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
        public async Task<Section> GetSectionById(int SectionId)
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
            var checkAlready = await _context.Section.Where(x =>
                                                                 x.IsActive == true && x.SectionId != entity.SectionId &&
                                                                 x.SectionName.ToUpper().Trim() == entity.SectionName.ToUpper().Trim()
                                                            ).ToListAsync();
            if (checkAlready != null)
            {
                return false;
            }

            data.SectionName = entity.SectionName;
            data.ModifiedDate = DateTime.Now;
            _context.Section.Update(data);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteSectionData(int Id)
        {
            var data = await _context.Section.FindAsync(Id);
            if (data == null || data.SectionId != Id)
            {
                return false;
            }
            data.IsActive = false;
            data.IsDeleted = true;
            data.ModifiedDate = DateTime.Now;
            _context.Section.Update(data);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<ClassMasterViewModel>> GetAllClassMasterData()
        {
            var result = await (from c in _context.ClassMaster
                                join s in _context.Section on c.SectionId equals s.SectionId
                                where c.IsActive == true
                                select new ClassMasterViewModel
                                {
                                    ClassId = c.ClassId,
                                    SectionId = c.SectionId,
                                    ClassName = c.ClassName,
                                    SectionName = s.SectionName
                                }).ToListAsync();

            var data = _mapper.Map<IEnumerable<ClassMasterViewModel>>(result);
            return data;
        }

        public async Task<ClassMaster> GetClassMasterById(int Id)
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
        public async Task<bool> DeleteClassData(int Id)
        {
            var data = await _context.ClassMaster.FindAsync(Id);
            if (data == null || data.ClassId != Id)
            {
                return false;
            }
            data.IsActive = false;
            data.IsDeleted = true;
            data.ModifiedDate = DateTime.Now;
            _context.ClassMaster.Update(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DropdownBindingViewModel>> GetSectionDropdownData()
        {
            var data = await (from s in _context.Section
                              where s.IsActive == true
                              select new DropdownBindingViewModel
                              {
                                  text = s.SectionName,
                                  value = s.SectionId
                              }).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Category>> GetAllCategoryData()
        {
            var data = await _context.Category.Where(c => c.IsActive == true).ToListAsync();
            return data;
        }

        public async Task<Category> GetCategoryById(int Id)
        {
            var data = await _context.Category.Where(x => x.CategoryId == Id).FirstOrDefaultAsync();
            return data;
        }
        public async Task<bool> SubmitCategoryData(Category entity)
        {
            try
            {
                var data = await _context.Category.Where(x => x.CategoryName.ToUpper().Trim() == entity.CategoryName.ToUpper().Trim()).FirstOrDefaultAsync();
                if (data != null)
                {
                    return false;
                }
                Category category = new Category()
                {
                    CategoryName = entity.CategoryName,
                    DateCreated = DateTime.Now,
                };
                await _context.AddAsync(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                var dd = ex;
                throw;
            }
        }
        public async Task<bool> UpdateCategoryData(Category entity)
        {
            var data = await _context.Category.FindAsync(entity.CategoryId);
            if (data == null || data.CategoryId != entity.CategoryId)
            {
                return false;
            }
            var checkAlready = await _context.Category.Where(x =>
                                                                 x.IsActive == true && x.CategoryId != entity.CategoryId &&
                                                                 x.CategoryName.ToUpper().Trim() == entity.CategoryName.ToUpper().Trim()
                                                            ).ToListAsync();
            if (checkAlready != null && checkAlready.Count() > 0)
            {
                return false;
            }

            data.CategoryName = entity.CategoryName;
            data.DateModified = DateTime.Now;
            _context.Update(data);
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeleteCategoryData(int Id)
        {
            var data = await _context.Category.FindAsync(Id);
            if (data == null || data.CategoryId != Id)
            {
                return false;
            }
            data.IsActive = false;
            data.IsDeleted = true;
            data.DateModified = DateTime.Now;
            _context.Update(data);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<CandidateViewModel>> GetAllCandidateInfoData()
        {
            try
            { 
                IEnumerable<CandidateViewModel> candidateViewModels = null;
                candidateViewModels = await _context.candidateViewModel.FromSqlRaw<CandidateViewModel>("Execute usp_GetCandidateData").ToListAsync(); 

                return candidateViewModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CandidateInfo> GetCandidateById(int Id)
        {
            var data = await _context.CandidateInfo.FindAsync(Id);
            return data;
        }
        public async Task<bool> SubmitCandidateData(CandidateInfo entity)
        {
            try
            {
                List<SqlParameter> parms = new List<SqlParameter>
            {
                new SqlParameter{ ParameterName = "@ClassRollNo", Value = entity.ClassRollNo},
                new SqlParameter{ ParameterName = "@CandidateName", Value= entity.CandidateName },
                new SqlParameter{ ParameterName = "@MobileNo", Value= entity.MobileNo },
                new SqlParameter{ ParameterName = "@AlternateNo", Value= entity.AlternateNo },
                new SqlParameter{ ParameterName = "@EmailId", Value= entity.EmailId },
                new SqlParameter{ ParameterName = "@CategoryId", Value = entity.CategoryId },
                new SqlParameter{ ParameterName = "@ICardNumber", Value = entity.ICardNumber },
                new SqlParameter{ ParameterName = "@GRNumber", Value = entity.GRNumber },
                new SqlParameter{ ParameterName = "@RFId", Value = entity.RFId },
                new SqlParameter{ ParameterName = "@ClassId", Value = entity.ClassId },
                new SqlParameter{ ParameterName = "@SectionId", Value = entity.SectionId },
                new SqlParameter{ ParameterName = "@ImpageUrl", Value = entity.ImpageUrl }
            };

                string sql = "EXEC usp_SaveCandidateData @ClassRollNo,@CandidateName,@MobileNo,@AlternateNo,@EmailId,@CategoryId,@ICardNumber,@GRNumber" +
                                                         ",@RFId,@ClassId,@SectionId,@ImpageUrl";
                int rowsAffected = await _context.Database.ExecuteSqlRawAsync(sql, parms);
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //var data = await _context.CandidateInfo.Where(x => x.IsActive == true && x.ClassRollNo == entity.ClassRollNo && x.ClassId == entity.ClassId && x.SectionId == entity.SectionId).ToListAsync();
            //if (data != null && data.Count() > 0)
            //{
            //    return false;
            //}
            //CandidateInfo canInfo = new CandidateInfo()
            //{
            //    CandidateName = entity.CandidateName,
            //    ClassRollNo = entity.ClassRollNo,
            //    MobileNo=entity.MobileNo,
            //    AlternateNo=entity.AlternateNo,
            //    EmailId=entity.EmailId,
            //    CategoryId=entity.CategoryId,
            //    ICardNumber=entity.ICardNumber,
            //    GRNumber=entity.GRNumber,
            //    RFId=entity.RFId,
            //    ClassId = entity.ClassId,
            //    SectionId = entity.SectionId,
            //    ImpageUrl=entity.ImpageUrl,
            //    IsActive=true,
            //    IsDeleted=false,
            //    DateCreated=DateTime.Now 
            //};
            //await _context.CandidateInfo.AddAsync(canInfo);
            //await _context.SaveChangesAsync();
            //return true;

        }
        public async Task<bool> DeleteCandidateData(int Id)
        {
            var data = await _context.CandidateInfo.Where(x => x.IsActive == true && x.CandidateId == Id).FirstOrDefaultAsync();
            if (data != null || data.CandidateId != Id)
            {
                return false;
            }

            CandidateInfo candidateInfo = new CandidateInfo()
            {
                CandidateId = Id,
                IsActive = false,
                IsDeleted = true,
                DateModified = DateTime.Now
            };

            _context.Update(candidateInfo);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<DropdownBindingViewModel>> GetClassDropdownData()
        {
            var data = await (from s in _context.ClassMaster
                              where s.IsActive == true
                              select new DropdownBindingViewModel
                              {
                                  text = s.ClassName,
                                  value = s.ClassId
                              }).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<DropdownBindingViewModel>> GetSectionDropdownDataByClassId(int Id)
        {
            var data = await (from s in _context.Section
                              join c in _context.ClassMaster on s.SectionId equals c.SectionId
                              where s.IsActive == true && s.SectionId == Id
                              select new DropdownBindingViewModel
                              {
                                  text = s.SectionName,
                                  value = s.SectionId
                              }).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<DropdownBindingViewModel>> GetCategoryDropdownData()
        {
            var data = await (from c in _context.Category
                              where c.IsActive == true
                              select new DropdownBindingViewModel
                              {
                                  text = c.CategoryName,
                                  value = c.CategoryId
                              }).ToListAsync();
            return data;
        }



    }
}
