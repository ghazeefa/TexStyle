using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC
{
    class ContractDetailRepository : Repository<ContractDetail>, IContractDetailRepository
    {
        public ContractDetailRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
            }
}
