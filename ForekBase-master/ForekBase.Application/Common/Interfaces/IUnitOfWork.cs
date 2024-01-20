using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForekBase.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        INewsRepository News { get; }
        IPostRepository Post { get; }
        void Save();

    }
}
