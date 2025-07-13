using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.Infrastructure
{
    public sealed class ReworkActivities
    {
        public static ReworkActivity ReProcess = new ReworkActivity { Id = 1, Name = "Re Process" };
        public static ReworkActivity ReWind = new ReworkActivity { Id = 2, Name = "Re Wind" };
        public static ReworkActivity ReChecking = new ReworkActivity { Id = 3, Name = "Re Checking" };

        public static List<ReworkActivity> GetAll = new List<ReworkActivity> { ReProcess, ReWind, ReChecking };
    }
}
