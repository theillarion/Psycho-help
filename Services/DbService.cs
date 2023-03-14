using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Helper;
using App.Helper.Enums;
using App.Helper.Exceptions;
using App.Helper.Extensions;
using App.Model;

namespace App.Services
{
    public class DbService : IDbService
    {
        private readonly DbConnection _connection;
        private const string NameTable = "User";

        public DbService(DbConnection connection, bool needOpenConnection = true)
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

        public bool ExistsUser(string login)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");

            try
            {
                using var command = _connection.CreateCommand();
                command.CommandText = $"SELECT `Login` FROM `{NameTable}` WHERE `Login` = @Login";
                command.AddParameterWithValue("@Login", login);

                return command.ExecuteScalar() != null;
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }

        public bool IsBannedUser(string login)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");

            try
            {
                using var command = _connection.CreateCommand();
                command.CommandText = $"SELECT `IsBlocked` FROM `{NameTable}` WHERE `Login` = @Login";
                command.AddParameterWithValue("@Login", login);

                var reader = command.ExecuteScalar();
                return reader == null || (bool)reader;
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }

        public string GetHashPassword(string login)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");

            try
            {
                using var command = _connection.CreateCommand();
                command.CommandText = $"SELECT `Password` FROM `{NameTable}` WHERE `Login` = @Login";
                command.AddParameterWithValue("@Login", login);

                using var reader = command.ExecuteReader();
                return reader.Read() ? reader.GetString(0) : string.Empty;
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }
        // Check without `ExistsUser`
        public AddUserResult AddUser(User user)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");

            if (ExistsUser(user.Login))
                return AddUserResult.UserExists;

            try
            {
                using var command = _connection.CreateCommand();
                command.CommandText =
                    $"INSERT INTO `{NameTable}` VALUES(@IdUserRole, @Login, @Password, @FirstName, @SecondName, @DateBirthday, @IsBlocked)";
                command.AddParameterWithValue("@IdUserRole", (int)user.IdUserRole);
                command.AddParameterWithValue("@Login", user.Login);
                command.AddParameterWithValue("@Password", user.Password);
                command.AddParameterWithValue("@FirstName", user.FirstName);
                command.AddParameterWithValue("@SecondName", user.SecondName);
                command.AddParameterWithValue("@DateBirthday", user.DateBirthday);
                command.AddParameterWithValue("@IsBlocked", user.IsBanned);

                return command.ExecuteNonQuery() == 1 ? AddUserResult.Success : AddUserResult.Unknown;
            }
            catch (Exception ex)
            {
                throw new ExecuteException(ex.Message);
            }
        }
        public DataRow? GetDataUserByLogin(string login)
        {
            if (_connection is not { State: ConnectionState.Open })
                throw new ConnectionException("Connection refused");
            try
            {
                using var command = _connection.CreateCommand();
                command.CommandText = $"SELECT * FROM `{NameTable}` WHERE `Login` = @Login";
                command.AddParameterWithValue("@Login", login);
                using var reader = command.ExecuteReader();

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
    }
}
