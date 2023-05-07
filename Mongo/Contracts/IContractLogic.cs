﻿using Mongo.Database.Models;
using Mongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Contracts
{
    public interface IContractLogic
    {
        List<Contract>? ReadList();
        IContract? ReadElement(int id);
        IContract CreateContract(IContract model);
        bool DeleteContract(IContract model);
        bool UpdateContract(IContract model);
    }
}
