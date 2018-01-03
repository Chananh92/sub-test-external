using System;
using System.Collections;
using System.Text;

using Framework.Caching;

namespace BetAndWin.PaymentService.Web.Controls.Maintenance
{
    class CacheComparer : IComparer
    {
        public int Compare(ICache x, ICache y)
        {
            if (x.Scope != y.Scope)
            {
                if (x.Scope == CacheScope.AppDomain)
                    return -1;
                return 1;
            }
            return x.Name.CompareTo(y.Name);
        }

        public int Compare(object x, object y)
        {
            if ((x is ICache) && (y is ICache))
                return Compare((ICache)x, (ICache)y);

            throw new ArgumentException("Argument x or y is not of ICache type");
        }
    }
}
