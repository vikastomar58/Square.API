using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Squares.API.DataLayer.EntityFrameworkCore
{
   public interface ISquareUOW
    {
        public DbContext dbContext { get; set; }
    }
}
