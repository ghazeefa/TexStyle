using System;
using System.Collections.Generic;
using System.Linq;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

//namespace TexStyle.ApplicationServices.Implementation.PPC
//{
//    internal class ContractDetailService : IContractDetailService
//    {
//        private readonly IContractDetailRepository _repo;
//    public ContractDetailService(IContractDetailRepository repo)
//    {
//        _repo = repo;
//    }

//        public ContractDetail Create(ContractDetail o)
//    {
//        try
//        {
//            o.CreatedOn = DateTime.Now;
//            _repo.Add(o);
//            return o;
//        }
//        catch (Exception ex)
//        {
//            throw ex;
//        }
//    }

    //    public ContractDetail Delete(ContractDetail o)
    //{
    //    try
    //    {
    //        o.IsDeleted = true;
    //        _repo.Update(o);
    //        return o;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public List<ContractDetail> GetAll()
    //{
    //    try
    ////    {
    ////        return _repo.GetList(x => x.IsDeleted == false
    ////        /*n => n.IssueNote*/
    //            //n => n.YarnManufacturer
    //            //n => n.YarnQuality,
    //            //n => n.YarnType,
    //            //n => n.StoreLocation
    //            ).ToList();
    ////    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public List<ContractDetail> GetBetweenDateRange(DateTime start, DateTime end)
    //{
    //    try
    //    {
    //        return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //    public ContractDetail GetById(long id)
    //{
    //    try
    //    {
    //        return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false,
    //            n => n.IssueNote,
    //            //n => n.YarnManufacturer,
    //            //n => n.YarnQuality,
    //            //n => n.YarnType,
    //            n => n.StoreLocation
    //            );
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    //////    public ContractDetail GetByContractNo(long id)
    //////{
    //////    try
    //////    {
    //////        return _repo.GetSingle(x => x.PPCPlanningId == id && x.IsDeleted == false,
    //////            n => n.IssueNote,
    //////            //n => n.YarnManufacturer,
    //////            //n => n.YarnQuality,
    //////            //n => n.YarnType,
    //////            n => n.StoreLocation
    ////            );
    ////    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

//        public ContractDetail Update(ContractDetail o)
//    {
//        try
//        {
//            _repo.Update(o);
//            return o;
//        }
//        catch (Exception ex)
//        {
//            throw ex;
//        }
//    }
//}
//}
   
