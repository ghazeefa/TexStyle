
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.ReportsViewModel.YD;
using TexStyle.Core.YD;

namespace TexStyle.DomainServices.Interfaces.IYD
{
    public interface IStickerRepository : IRepository<Sticker>
    {
      Task<List<StickerRepository_D4ViewModel>> StickerRepository_D4(long userid);
    }
}
