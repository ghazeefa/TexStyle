using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.YD.Reports
{
    public class RecipeIssued_D3ViewModel : DefaultViewModel
    {
        public RecipeIssued_D3ViewModel()
        {
            this.RecipeIssuedDetail_D3ViewModel = new List<RecipeIssuedDetail_D3ViewModel>();
        }


        public DateTime Date  { get; set; }
        public List<RecipeIssuedDetail_D3ViewModel> RecipeIssuedDetail_D3ViewModel { get; set; }

    }
       
    }