﻿using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SignalRDemo.Cars;
using SignalRDemo.DataAccess;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace SignalRDemo.DapperImplementation;

/// <summary>
/// dapper generic repository'i nasıl kullanacağımı araştırırken şöyle bir github reposuyla karşılaştım
/// https://github.com/bolicd/dapper-extensions
/// buradaki tek konu bu github reposundaki kodlar mssql e göre yazılmış.
/// herhangi bağlayıcı sebep olmadığından "postgresql" üzerinde yazmaya karar verdim.
/// bu yüzden script'lerdeki düzenlenmesi gereken kısımları düzenledikten sonra yapının çalıştığını gözlemledim.
/// </summary>
/// <typeparam name="T"></typeparam>
public class DapperGenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    private static IEnumerable<string> _listOfProperties;
    private static string _selectFields = string.Empty;
    private readonly string _connectionString;
    private readonly string _tableName;

    static DapperGenericRepository()
    {
        _listOfProperties = GenerateListOfProperties(typeof(T).GetProperties());
    }

    public DapperGenericRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SignalRDemo"); ;
        _tableName = typeof(T).Name + "s";
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await LoadFields(async () =>
        {
            using var connection = CreateConnection();
            return await connection.QueryAsyncWithToken<T>($"SELECT {_selectFields} FROM public.\"{_tableName}\"",
                cancellationToken: cancellationToken);
        }, true);
    }

    public async Task<T> GetAsync(object id, CancellationToken cancellationToken = default)
    {
        return await LoadFields(async () =>
        {
            using var connection = CreateConnection();
            var result =
                await connection.QuerySingleOrDefaultWithToken<T>($"SELECT {_selectFields} FROM public.\"{_tableName}\" WHERE \"Id\"=@Id",
                    new { Id = id }, cancellationToken: cancellationToken);
            if (result == null) throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");

            return result;
        }, true);
    }

    public async Task InsertAsync(T t, CancellationToken cancellationToken = default)
    {
        var insertQuery = GenerateInsertQuery();

        using var connection = CreateConnection();
        var result = await connection.ExecuteAsyncWithToken(insertQuery, t, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        Console.Write(result);
    }

    public async Task UpdateAsync(T t, CancellationToken cancellationToken = default)
    {
        var updateQuery = GenerateUpdateQuery();

        using var connection = CreateConnection();
        await connection.ExecuteAsyncWithToken(updateQuery, t, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsyncWithToken($"DELETE FROM public.\"{_tableName}\" WHERE \"Id\"=@Id", new { Id = id },
            cancellationToken: cancellationToken);
    }

    #region PrivateHelperMethods

    private NpgsqlConnection SqlConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }

    /// <summary>
    /// Open new connection and return it for use
    /// </summary>
    /// <returns></returns>
    private NpgsqlConnection CreateConnection()
    {
        var conn = SqlConnection();
        conn.Open();
        return conn;
    }

    private TU LoadFields<TU>(Func<TU> method, bool includeId = false)
    {
        if (string.IsNullOrEmpty(_selectFields)) _selectFields = GenerateSelectFields(_listOfProperties, includeId);
        return method.Invoke();
    }

    private static IEnumerable<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
    {
        return (from prop in listOfProperties
                let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                select prop.Name).ToList();
    }

    private void IgnoreId(string property, Action action)
    {
        if (!property.Equals("Id")) action.Invoke();
    }

    private string GenerateSelectFields(IEnumerable<string> properties, bool includeId)
    {
        var fields = new StringBuilder();
        foreach (var property in properties)
        {
            if (includeId == false)
            {
                IgnoreId(property, () => { fields.Append($"\"{property}\","); });
            }
            else
            {
                fields.Append($"\"{property}\",");
            }
        }

        fields.Remove(fields.Length - 1, 1);
        return fields.ToString();
    }

    private string GenerateInsertQuery()
    {
        var insertQuery = new StringBuilder($"INSERT INTO public.\"{_tableName}\" ");

        insertQuery.Append("(");
        foreach (var listOfProperty in _listOfProperties)
        {
            //TODO: extract this check
            IgnoreId(listOfProperty, () => { insertQuery.Append($"\"{listOfProperty}\","); });
        }

        insertQuery.Remove(insertQuery.Length - 1, 1).Append(") VALUES (");

        foreach (var listOfProperty in _listOfProperties)
        {
            IgnoreId(listOfProperty, () => { insertQuery.Append($"@{listOfProperty},"); });
        }

        insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");

        return insertQuery.ToString();
    }

    private string GenerateUpdateQuery()
    {
        var updateQuery = new StringBuilder($"UPDATE public.\"{_tableName}\" SET ");

        foreach (var listOfProperty in _listOfProperties)
        {
            IgnoreId(listOfProperty, () => { updateQuery.Append($"\"{listOfProperty}\"=@{listOfProperty},"); });
        }

        updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
        updateQuery.Append(" WHERE \"Id\"=@Id");

        return updateQuery.ToString();
    }

    #endregion
}
