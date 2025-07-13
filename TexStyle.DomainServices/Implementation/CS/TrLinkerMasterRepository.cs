using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;
using TexStyle;

namespace TexStyle.DomainServices.Implementation.CS
{
    class TrLinkerMasterRepository : Repository<TrLinkerMaster>, ITrLinkerMasterRepository
    {
        private AppDbContext _db; 
        public static string APP_CON_NAME = DBStringConsts.TEXSTYLE_DEV_SP;
        public TrLinkerMasterRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void ExceRateStoreProcedure(DateTime date)
        {
            DataTable dt = new DataTable();
           string connetionString = "Data Source=191.168.4.88,50507;Initial Catalog=TexStyleDB;User ID=sa;Password=Abc123#";

            using (SqlConnection sqlConn = new SqlConnection(connetionString))
            {
                string sql = "_spUpdateRate";
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {

                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@Date", date);
                    // sqlCmd.Parameters["@FruitName"].Direction = ParameterDirection.Output;
                    sqlConn.Open();
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dt);
                    }

                }
            }
        }

        //public List<StockSummaryC1ViewModel> ExceC1StoreProcedure(DateTime date)
        //{
        //    DataTable dt = new DataTable();
        //    //  string connetionString = "Data Source=DESKTOP-27E7D8A;Initial Catalog=TexStyleDB1;User ID=sa;Password=Abc123#";

        //    using (SqlConnection sqlConn = new SqlConnection(APP_CON_NAME))
        //    {
        //        string sql = "_spStockReportC1";
        //        using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
        //        {

        //            sqlCmd.CommandType = CommandType.StoredProcedure;
        //            sqlCmd.Parameters.AddWithValue("@Date", date);
        //            // sqlCmd.Parameters["@FruitName"].Direction = ParameterDirection.Output;
        //            sqlConn.Open();
        //            using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
        //            {
        //                sqlAdapter.Fill(dt);
                        
        //            }

        //        }
        //    }
       // }

    }
}
