using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.ReportsViewModel.YD;
using TexStyle.Core.YD;

namespace TexStyle.ApplicationServices.Interfaces.IYD
{
    public interface IStickerService : IDefaultService<Sticker>
    {
        Task<List<StickerRepository_D4ViewModel>> StickerService_D4(long LPSId);
        ///*List<StickerRepository_D4ViewModel> StickerRepository_D4(long LPSId)*/;
    }
}
