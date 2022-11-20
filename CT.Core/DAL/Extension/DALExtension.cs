using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CT.Core.DAL.Extension
{
    public static class DALExtension
    {
        #region "IQueryable<TSource> Page<TSource>"

        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int brojTekuceStranice, int brojStavkiPoStranici)
        {
            return source.Skip((brojTekuceStranice - 1) * brojStavkiPoStranici).Take(brojStavkiPoStranici);
        }

        #endregion

        #region "IEnumerable<TSource> Page<TSource>"

        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int brojTekuceStranice, int brojStavkiPoStranici)
        {
            return source.Skip((brojTekuceStranice - 1) * brojStavkiPoStranici).Take(brojStavkiPoStranici);
        }

        #endregion


    }
}
