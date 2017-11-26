using Chicadresse.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Business.Services.Base
{
    public class CacheService : ICacheService
    {
        #region Fields

        private readonly ICacheRepository _cacheRepository;

        #endregion

        #region ctor

        public CacheService(ICacheRepository cacheRepository)
        {
            this._cacheRepository = cacheRepository;
        }

        #endregion

        public IEnumerable<T> GetEntities<T>(Expression<Func<T, bool>> where) where T : class
        {
            return this._cacheRepository.GetMany<T>(where);
        }

    }
}
