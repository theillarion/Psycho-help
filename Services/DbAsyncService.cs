﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xk7.Helper.Enums;
using Xk7.Helper.Exceptions;
using Xk7.Helper.Extensions;
using Xk7.Model;

namespace Xk7.Services
{
    internal class DbAsyncService : IDbAsyncService
    {
        private readonly DbConnection _connection;
        public DbAsyncService(DbConnection connection, bool needOpenConnection = true)
        {
            if (!needOpenConnection)
                return;
            _connection = connection;
            try
            {
                _connection.Open();
            }
            catch (Exception)
            {
                throw new ConnectionException("Connection refused");
            }
        }
        public async Task<bool> ExistsUserAsync(string login)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");

            try
            {
                await using var command = _connection.CreateCommand();
                command.CommandText = $"SELECT `Login` FROM `User` WHERE `Login` = @Login";
                command.AddParameterWithValue("@Login", login);

                return await command.ExecuteScalarAsync() != null;
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }
        public async Task<bool> IsBannedUserAsync(string login)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");

            try
            {
                await using var command = _connection.CreateCommand();
                command.CommandText = $"SELECT `IsBlocked` FROM `User` WHERE `Login` = @Login";
                command.AddParameterWithValue("@Login", login);

                var reader = await command.ExecuteScalarAsync();
                return reader == null || (bool)reader;
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }
        public async Task<string> GetHashPasswordAsync(string login)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");

            try
            {
                await using var command = _connection.CreateCommand();
                command.CommandText = $"SELECT `Password` FROM `User` WHERE `Login` = @Login";
                command.AddParameterWithValue("@Login", login);

                await using var reader = await command.ExecuteReaderAsync();
                return await reader.ReadAsync() ? reader.GetString(0) : string.Empty;
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }
        // Check without `ExistsUser`
        public async Task<AddUserResult> AddUserAsync(User user)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");

            if (await ExistsUserAsync(user.Login))
                return AddUserResult.UserExists;

            try
            {
                await using var command = _connection.CreateCommand();
                command.CommandText =
                    $"INSERT INTO `User` VALUES(@IdUserRole, @Login, @Password, @FirstName, @SecondName, @DateBirthday, @IsBlocked)";
                command.AddParameterWithValue("@IdUserRole", (int)user.IdUserRole);
                command.AddParameterWithValue("@Login", user.Login);
                command.AddParameterWithValue("@Password", user.Password);
                command.AddParameterWithValue("@FirstName", user.FirstName);
                command.AddParameterWithValue("@SecondName", user.SecondName);
                command.AddParameterWithValue("@DateBirthday", user.DateBirthday.ToString("yyyy-MM-dd"));
                command.AddParameterWithValue("@IsBlocked", user.IsBanned);

                return await command.ExecuteNonQueryAsync() == 1 ? AddUserResult.Success : AddUserResult.Unknown;
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }
        public async Task<DataRow?> GetDataUserByLoginAsync(string login)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");
            try
            {
                await using var command = _connection.CreateCommand();
                command.CommandText = $"SELECT * FROM `User` WHERE `Login` = @Login";
                command.AddParameterWithValue("@Login", login);
                await using var reader = await command.ExecuteReaderAsync();
                if (!reader.HasRows)
                    return null;
                using var table = new DataTable();
                table.Load(reader);
                return table.Rows[0];
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }
        public async Task<AddLoggingResult> AddLog(string login, LoggingType loggingType)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");

            if (!await ExistsUserAsync(login))
                return AddLoggingResult.NotExistsUser;

            try
            {
                await using var command = _connection.CreateCommand();
                command.CommandText = $"INSERT INTO `Logging`(`IdLoggingType`, `Login`, `UTCDateTime`) VALUES(@LoggingType, @Login, UTC_TIMESTAMP());";
                command.AddParameterWithValue("@LoggingType", loggingType);
                command.AddParameterWithValue("@Login", login);

                return await command.ExecuteNonQueryAsync() == 1 ? AddLoggingResult.Success : AddLoggingResult.Unknown;
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }

        public async Task<DataTable?> GetTable(string nameTable)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");

            try
            {
                await using var command = _connection.CreateCommand();
                command.CommandText = $"SELECT * FROM `{nameTable}`";
                await using var reader = await command.ExecuteReaderAsync();
                if (!reader.HasRows)
                    return null;
                using var result = new DataTable();
                result.Load(reader);
                return result;
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }
    }
}
