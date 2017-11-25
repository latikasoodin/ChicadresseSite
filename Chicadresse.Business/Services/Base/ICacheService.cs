using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Business.Services.Base
{
    public interface ICacheService
    {
        IEnumerable<T> GetEntities<T>(Expression<Func<T, bool>> where) where T : class;
    }
}
