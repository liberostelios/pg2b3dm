﻿using System;
using Npgsql;

namespace B3dm.Tileset
{
    public static class SpatialReferenceRepository
    {
        public static int GetSpatialReference(NpgsqlConnection conn, string geometry_table, string geometry_column, string query="")
        {
            var where = (query != string.Empty ? $"where {query}" : String.Empty);

            var sql = $"SELECT ST_SRID({geometry_column}) from {geometry_table} {where} limit 1";
            conn.Open();
            var cmd = new NpgsqlCommand(sql, conn);
            var reader = cmd.ExecuteReader();
            reader.Read();
            var sr = reader.GetInt32(0);
            reader.Close();
            conn.Close();
            return sr;
        }
    }
}
