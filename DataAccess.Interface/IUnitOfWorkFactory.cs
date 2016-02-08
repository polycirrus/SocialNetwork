﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Interface
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
