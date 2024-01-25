using ForekBase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForekBase.Application.Common.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        void Update(Comment comment);
    }
}
