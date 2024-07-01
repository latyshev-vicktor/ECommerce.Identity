using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.SeedWorks
{
    public class BaseEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; private set; }
        public DateTimeOffset ModifyDate { get; private set; }

        public void MakeModify()
        {
            ModifyDate = DateTimeOffset.Now;
        }
    }
}
