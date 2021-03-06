﻿using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using NpgsqlTypes;

namespace Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping
{
    public class NpgsqlPointTypeMapping : NpgsqlTypeMapping
    {
        public NpgsqlPointTypeMapping() : base("point", typeof(NpgsqlPoint), NpgsqlDbType.Point) {}

        protected NpgsqlPointTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters, NpgsqlDbType.Point) {}

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new NpgsqlPointTypeMapping(parameters);

        protected override string GenerateNonNullSqlLiteral(object value)
        {
            var point = (NpgsqlPoint)value;
            return $"POINT '({point.X.ToString("G17", CultureInfo.InvariantCulture)},{point.Y.ToString("G17", CultureInfo.InvariantCulture)})'";
        }
    }

    public class NpgsqlLineTypeMapping : NpgsqlTypeMapping
    {
        public NpgsqlLineTypeMapping() : base("line", typeof(NpgsqlLine), NpgsqlDbType.Line) {}

        protected NpgsqlLineTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters, NpgsqlDbType.Line) {}

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new NpgsqlLineTypeMapping(parameters);

        protected override string GenerateNonNullSqlLiteral(object value)
        {
            var line = (NpgsqlLine)value;
            return $"LINE '{{{line.A.ToString("G17", CultureInfo.InvariantCulture)},{line.B.ToString("G17", CultureInfo.InvariantCulture)},{line.C.ToString("G17", CultureInfo.InvariantCulture)}}}'";
        }
    }

    public class NpgsqlLineSegmentTypeMapping : NpgsqlTypeMapping
    {
        public NpgsqlLineSegmentTypeMapping() : base("lseg", typeof(NpgsqlLSeg), NpgsqlDbType.LSeg) {}

        protected NpgsqlLineSegmentTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters, NpgsqlDbType.LSeg) {}

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new NpgsqlLineSegmentTypeMapping(parameters);

        protected override string GenerateNonNullSqlLiteral(object value)
        {
            var lseg = (NpgsqlLSeg)value;
            var x1 = lseg.Start.X.ToString("G17", CultureInfo.InvariantCulture);
            var y1 = lseg.Start.Y.ToString("G17", CultureInfo.InvariantCulture);
            var x2 = lseg.End.X.ToString("G17", CultureInfo.InvariantCulture);
            var y2 = lseg.End.Y.ToString("G17", CultureInfo.InvariantCulture);
            return $"LSEG '[({x1},{y1}),({x2},{y2})]'";
        }
    }

    public class NpgsqlBoxTypeMapping : NpgsqlTypeMapping
    {
        public NpgsqlBoxTypeMapping() : base("box", typeof(NpgsqlBox), NpgsqlDbType.Box) {}

        protected NpgsqlBoxTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters, NpgsqlDbType.Box) {}

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new NpgsqlBoxTypeMapping(parameters);

        protected override string GenerateNonNullSqlLiteral(object value)
        {
            var box = (NpgsqlBox)value;
            var right  = box.Right.ToString("G17", CultureInfo.InvariantCulture);
            var top    = box.Top.ToString("G17", CultureInfo.InvariantCulture);
            var left   = box.Left.ToString("G17", CultureInfo.InvariantCulture);
            var bottom = box.Bottom.ToString("G17", CultureInfo.InvariantCulture);
            return $"BOX '(({right},{top}),({left},{bottom}))'";
        }
    }

    public class NpgsqlPathTypeMapping : NpgsqlTypeMapping
    {
        public NpgsqlPathTypeMapping() : base("path", typeof(NpgsqlPath), NpgsqlDbType.Path) {}

        protected NpgsqlPathTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters, NpgsqlDbType.Path) {}

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new NpgsqlPathTypeMapping(parameters);

        protected override string GenerateNonNullSqlLiteral(object value)
        {
            var path = (NpgsqlPath)value;
            var sb = new StringBuilder();
            sb.Append("PATH '");
            sb.Append(path.Open ? '[' : '(');
            for (var i = 0; i < path.Count; i++)
            {
                sb.Append('(');
                sb.Append(path[i].X.ToString("G17", CultureInfo.InvariantCulture));
                sb.Append(',');
                sb.Append(path[i].Y.ToString("G17", CultureInfo.InvariantCulture));
                sb.Append(')');
                if (i < path.Count - 1)
                    sb.Append(',');
            }
            sb.Append(path.Open ? ']' : ')');
            sb.Append('\'');
            return sb.ToString();
        }
    }

    public class NpgsqlPolygonTypeMapping : NpgsqlTypeMapping
    {
        public NpgsqlPolygonTypeMapping() : base("polygon", typeof(NpgsqlPolygon), NpgsqlDbType.Polygon) {}

        protected NpgsqlPolygonTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters, NpgsqlDbType.Polygon) {}

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new NpgsqlPolygonTypeMapping(parameters);

        protected override string GenerateNonNullSqlLiteral(object value)
        {
            var polygon = (NpgsqlPolygon)value;
            var sb = new StringBuilder();
            sb.Append("POLYGON '(");
            for (var i = 0; i < polygon.Count; i++)
            {
                sb.Append('(');
                sb.Append(polygon[i].X.ToString("G17", CultureInfo.InvariantCulture));
                sb.Append(',');
                sb.Append(polygon[i].Y.ToString("G17", CultureInfo.InvariantCulture));
                sb.Append(')');
                if (i < polygon.Count - 1)
                    sb.Append(',');
            }
            sb.Append(")'");
            return sb.ToString();
        }
    }

    public class NpgsqlCircleTypeMapping : NpgsqlTypeMapping
    {
        public NpgsqlCircleTypeMapping() : base("circle", typeof(NpgsqlCircle), NpgsqlDbType.Circle) {}

        protected NpgsqlCircleTypeMapping(RelationalTypeMappingParameters parameters)
            : base(parameters, NpgsqlDbType.Circle) {}

        protected override RelationalTypeMapping Clone(RelationalTypeMappingParameters parameters)
            => new NpgsqlCircleTypeMapping(parameters);

        protected override string GenerateNonNullSqlLiteral(object value)
        {
            var circle = (NpgsqlCircle)value;
            var x = circle.X.ToString("G17", CultureInfo.InvariantCulture);
            var y = circle.Y.ToString("G17", CultureInfo.InvariantCulture);
            var radius = circle.Radius.ToString("G17", CultureInfo.InvariantCulture);
            return $"CIRCLE '<({x},{y}),{radius}>'";
        }
    }
}
