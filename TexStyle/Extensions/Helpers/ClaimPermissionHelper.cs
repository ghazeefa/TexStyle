using TexStyle.Common;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace TexStyle.Helpers
{
    public class ClaimPermissionHelper
    {
        private static List<string> claimsList = new List<string>();
        // miss gi neche array me siraf modules k name likhne hain apne
        // jis jis pe premissions or secutiry lagani ha.

        private static List<string> modules = new List<string>() {
// area_module_xxxxx
#region Production Planing Control Modules
// Forms
"ppc_igp",
"ppc_igp_detail",
"ppc_purchase_order",
"ppc_planing",
"ppc_issue_to_winding",
"ppc_issue_to_winding_detail",
"ppc_production_received",
"ppc_production_received_detail",
"ppc_ogp",
"ppc_ogp_detail",

// Settings
"ppc_settings_yarn_type",
"ppc_settings_yarn_quality",
"ppc_settings_bag_marking",
"ppc_settings_cone_marking",
"ppc_settings_store_location",
"ppc_settings_party",
"ppc_settings_buyer",
"ppc_settings_buyer_color",
"ppc_settings_activity_type",
"ppc_settings_yarn_manufacturer",
"ppc_settings_machine",
"ppc_settings_machine_type",
"ppc_settings_season",
"ppc_settings_report_filters",
#endregion

#region Yarn Dyeing Modules
// Forms
"yd_recipe_format",
"yd_recipe",
// Settings
"yd_settings_process_types",
"yd_settings_machine_types",
"yd_settings_recipe_steps",
"yd_settings_costing",
"yd_settings_machines",
"yd_time_tracking",
#endregion

#region Chemical Store Modules
// forms
"cs_opening_tr",
"cs_chemical_dilution_in_tr",
"cs_general_consumption_in_tr",
"cs_lc_import_tr",
"cs_loan_party_return_in_tr",
"cs_loan_taken_in_tr",
"cs_local_purchase_in_tr",
"cs_inter_unit_out_tr",
"cs_loan_party_given_out_tr",
"cs_taken_return_out_tr",
"cs_chemical_issuance_recipe",
"cs_store_return_note_tr",
// settings
"cs_settings_chemicals",
"cs_settings_dyes",
"cs_settings_supplier",
#endregion

#region Gate
// forms
"gate_igp_yarn",
"gate_igp_dyes_and_chemical",
"gate_igp_loan_dyes_and_chemical",
"gate_ogp_loan_dyes_and_chemical",
"gate_ogp_yarn",
// settings
"gate_settings",
#endregion

#region User Management
"user",
"role",
#endregion

#region Marketing Accounts Management
"ogpinvoice",

#endregion

#region Analysis
"analysis_analysis_type",
"analysis_defect",
"analysis_defect_analysis",
#endregion

};

        private static List<string> permission = new List<string> {
"view",
"create_and_update",
"create",
"edit",
"delete",
};

        public static List<string> GetClaimsList()
        {
            modules.ForEach(x => {
                permission.ForEach(p => {
                    var claim = $"{x.Replace("_", ".").Replace('-', '.')}.{p.Replace('-', '.')}";
                    var claimSymbol = $"{x.ToUpper()}_{p.ToUpper().Replace('-', '_')}";
                    var constField = $@"public const string {claimSymbol} = ""{claim}"";";
                    var owinRegisteration = $@" options.AddPolicy({nameof(AccountClaimKeys)}.{claimSymbol}, policy => policy.RequireClaim({nameof(AccountClaimTypes)}.PERMISSION,{nameof(AccountClaimKeys)}.{claimSymbol}));";
                    claimsList.Add(constField);
                });
            });
            return claimsList;
        }

        public static List<ClaimModuleViewModel> GetClaimModuleViewModelList()
        {
            List<ClaimModuleViewModel> model = new List<ClaimModuleViewModel>();
            long id = 1;
            modules.ForEach(x => {
                var m = new ClaimModuleViewModel()
                {
                    ModuleName = x.Replace("_", " ").Replace('-', ' ').ToUpper()
                };
                permission.ForEach(p => {
                    var claim = $"{x.Replace("_", ".").Replace('-', '.')}.{p.Replace('-', '.')}";
                    m.ClaimsList.Add(new ClaimsPermissionViewModel { Id = id, Name = claim });
                    id++;
                });
                m.ClaimsSelectList = m.ClaimsList.ToSelectList();
                model.Add(m);
            });
            return model;
        }

        public static bool GenerateOrUpdateClaimKeys(string appNamespace = "TexStyle.Common")
        {
            try
            {

                #region Generate file code
                var fileName = "AccountClaimKeys.cs";
                var cphDir = new DirectoryInfo(Environment.CurrentDirectory);
                var textyleDir = new DirectoryInfo(cphDir.Parent.FullName).GetDirectories();
                var common = textyleDir.Where(dir => dir.Name.Equals(appNamespace)).Single();
                var claimsFile = common.GetFiles().Where(cf => cf.Name.Equals(fileName)).SingleOrDefault();

                #region ClaimkeysGeneration Script
                var contentStr = $@"namespace {appNamespace} {{" + $"\n\tpublic class AccountClaimKeys {{" + $"\n";
                modules.ForEach(x => {
                    contentStr += $"\t\t#region {x.Replace('_', ' ').ToUpper()}\n";
                    permission.ForEach(p => {
                        var claim = $"{x.Replace("_", ".").Replace('-', '.')}.{p.Replace('-', '.')}";
                        var claimSymbol = $"{x.ToUpper()}_{p.ToUpper().Replace('-', '_')}";
                        var constField = $"\t\t" + $@" public const string {claimSymbol} = ""{claim}"";" + "\n";
                        // append shit
                        contentStr += constField;
                    });
                    contentStr += $"\t\t#endregion\n\n";
                });
                contentStr += "\n\t}\n}";
                #endregion

                if (claimsFile != null && claimsFile.Exists == true)
                {
                    File.WriteAllText(claimsFile.FullName, contentStr);
                }
                else
                {
                    using (var sr = File.CreateText(Path.Combine(common.FullName, fileName)))
                    {
                        sr.Write(contentStr);
                    }
                }
                #endregion
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}