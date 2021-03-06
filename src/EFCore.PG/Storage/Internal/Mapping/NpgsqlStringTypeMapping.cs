﻿using System;
using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping
{
    /// <summary>
    /// The type mapping for the PostgreSQL 'text' data type.
    /// </summary>
    /// <remarks>
    /// See: https://www.postgresql.org/docs/current/static/datatype-character.html
    /// </remarks>
    /// <inheritdoc />
    public class NpgsqlStringTypeMapping : StringTypeMapping
    {
        public NpgsqlStringTypeMapping([NotNull] string storeType, bool unicode = false, int? size = null)
            : base(storeType, System.Data.DbType.String, unicode, size) {}

        protected NpgsqlStringTypeMapping(RelationalTypeMappingParameters parameters) : base(parameters) {}

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new NpgsqlStringTypeMapping(parameters);

        protected override void ConfigureParameter(DbParameter parameter)
        {
            // TODO: See #357. We should be able to simply use StringTypeMapping but DbParameter.Size isn't managed properly.
            if (Size.HasValue)
            {
                var value = parameter.Value;
                var length = (value as string)?.Length;
                var size = Size.Value;

                parameter.Size = value == null || value == DBNull.Value || length != null && length <= size
                    ? size
                    : -1;
            }
        }
    }
}
