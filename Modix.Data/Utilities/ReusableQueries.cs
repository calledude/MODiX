using System;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Modix.Data.Models.Core;

namespace Modix.Data.Utilities
{
    public static class ReusableQueries
    {
        public static readonly Expression<Func<GuildUserEntity, string, bool>> StringContainsUser =
            (entity, str) => DbCaseInsensitiveEquals.Invoke(str, FormatUserName.Invoke(entity));

        public static readonly Expression<Func<GuildUserEntity, string>> FormatUserName =
            entity => entity.Nickname ?? (entity.User.Username + "#" + entity.User.Discriminator);

        public static readonly Expression<Func<string, string, bool>> DbCaseInsensitiveEquals =
            (x, y) => EF.Functions.ILike(x, _escapeLikePattern.Invoke(y));

        public static readonly Expression<Func<string, string, bool>> DbCaseInsensitiveContains = (value, search) =>
            // % _ are special characters and need to be escaped
            EF.Functions.ILike(value, "%" + _escapeLikePattern.Invoke(search) + "%");

        private static readonly Expression<Func<string, string>> _escapeLikePattern =
            s => s.Replace("_", "%_").Replace("%", "%%");
    }
}
