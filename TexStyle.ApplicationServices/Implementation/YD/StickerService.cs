using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IYD;
using TexStyle.Core.ReportsViewModel.YD;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;



namespace TexStyle.ApplicationServices.Implementation.YD
{
    internal class StickerService : IStickerService
    {

        private IStickerRepository _repo;

   
        public StickerService(IStickerRepository repo)
        {
            _repo = repo;
        }
        public async Task<Sticker> Create(Sticker o)
        {
            try
            {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sticker> Delete(Sticker o)
        {
            try
            {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Sticker>> GetAll()
        {
            try
            { 
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.OrderBy(x => x.LPSId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Sticker>> GetBetweenDateRange(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sticker> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sticker> Update(Sticker o)
        {
            try
            {
                o.UpdatedOn = DateTime.Now;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      public async Task<List<StickerRepository_D4ViewModel>> StickerService_D4(long LPSId)
        {
            try
            {
                var list = await this._repo.StickerRepository_D4(LPSId);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}