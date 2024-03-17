using ForekBase.Application.Common.Interfaces;
using ForekBase.Domain.Entities;
using ForekBase.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForekBase.Infrastructure.Repository
{
    public class SubscriberRepository : Repository<Subscriber>, ISubscriberRepository
    {
        private readonly ApplicationDbContext _db;

        public SubscriberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Subscriber subscriber)
        {
            _db.Subscribers.Update(subscriber);
        }
    }
}
