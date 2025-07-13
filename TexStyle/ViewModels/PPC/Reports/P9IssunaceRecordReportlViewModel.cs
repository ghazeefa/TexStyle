using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
   

        public class P9IssunaceRecordReportlViewModel : DefaultViewModel


        {
            public P9IssunaceRecordReportlViewModel()
            {
                P9IssunaceRecordReportDetailViewModel = new List<P9IssunaceRecordReportDetailViewModel>();
            }
            public int LotNo { get; set; }

            public List<P9IssunaceRecordReportDetailViewModel> P9IssunaceRecordReportDetailViewModel { get; set; }


        }
    }

