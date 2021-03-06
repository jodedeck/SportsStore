﻿using Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Abstract
{
    public interface IOrderRepository
    {

        Order GetById(int id);
        void SaveOrder(Order order);
        int Commit();
    }
}
