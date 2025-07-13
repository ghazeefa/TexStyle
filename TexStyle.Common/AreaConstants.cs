using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.Common {
    public sealed class AreaConstants {
         public sealed class PRODUCTION_PLANING_CONTROL {
            public const string Name = "ProductionPlaningControl";
            public const string NormalizedName = "Production Planing Control (PPC)";
            public const string Abriviation = "ppc";
            public const string RouteString = "ppc/[controller]/[action]";
        }
        public sealed class CHEMICAL_STORE {
            public const string Name = "ChemicalStore";
            public const string NormalizedName = "Chemical Store (CS)";
            public const string Abriviation = "cs";
            public const string RouteString = "cs/[controller]/[action]";

        }
        public sealed class YARN_DYEING {
            public const string Name = "YarnDyeing";
            public const string NormalizedName = "Yarn Dyeing (YD)";
            public const string Abriviation = "yd";
            public const string RouteString = "yd/[controller]/[action]";

        }

        public sealed class GATE
        {
            public const string Name = "Gate";
            public const string NormalizedName = "Gate (Gate)";
            public const string Abriviation = "gate";
            public const string RouteString = "gate/[controller]/[action]";

        }

        public sealed class ADMIN {
            public const string Name = "Admin";
            public const string NormalizedName = "Admin";
            public const string Abriviation = "admin";
            public const string RouteString = "admin/[controller]/[action]";
        }   
        public sealed class MarketingAccounts {
            public const string Name = "MarketingAccounts";
            public const string NormalizedName = "MarketingAccounts";
            public const string Abriviation = "marketingaccounts";
            public const string RouteString = "admin/[controller]/[action]";
        }

        public sealed class USER_MANAGEMENT {
            public const string Name = "UserManagement";
            public const string NormalizedName = "User Management";
            public const string Abriviation = "usermanagement";
            public const string RouteString = "usermanagement/[controller]/[action]";
        }
        public sealed class Analysis
        {
            public const string Name = "Analysis";
            public const string NormalizedName = "Analysis";
            public const string Abriviation = "analysis";
            public const string RouteString = "analysis/[controller]/[action]";
        }
        public sealed class Fabric
        {
            public const string Name = "Fabric";
            public const string NormalizedName = "Fabric";
            public const string Abriviation = "fabric";
            public const string RouteString = "fabric/[controller]/[action]";
        }

    }
}
