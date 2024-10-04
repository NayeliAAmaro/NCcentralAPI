using System.Data.SqlClient;

namespace DTQuotationGS.Api
{
    public class DatabaseLogger : ILogger
    {
        private readonly string _connectionString;

        public DatabaseLogger(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel)
        {
            // Ajustar los niveles que deseas registrar (por ejemplo, Warning, Error, Critical)
            return logLevel >= LogLevel.Warning;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var message = formatter(state, exception);
            var exceptionMessage = exception?.ToString() ?? string.Empty;

            // Guardar el log en la base de datos
            SaveLog(logLevel.ToString(), message, exceptionMessage).Wait();
        }

        private async Task SaveLog(string logLevel, string message, string exception)
        {
            var query = "INSERT INTO Logs (LogLevel, Message, Exception, CreatedAt) VALUES (@LogLevel, @Message, @Exception, GETDATE())";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LogLevel", logLevel);
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@Exception", exception);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}