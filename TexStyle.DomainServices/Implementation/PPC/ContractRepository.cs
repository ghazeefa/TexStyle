using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC
{
   internal class ContractRepository: Repository<Contract>, IContractRepository
        {
            public ContractRepository(AppDbContext appDbContext) : base(appDbContext){ 

    }
    }
}
