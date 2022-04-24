﻿using KWH.BAL.IRepository;
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
            await _context.AddAsync(entity);
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
    }
}