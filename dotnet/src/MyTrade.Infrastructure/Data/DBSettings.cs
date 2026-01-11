
namespace MyTrade.Infrastructure.Data
{
    public sealed class DBSettings
    {
        public string ConnectionString { get; set; } = default!;
        public string DatabaseName { get; set; } = default!;
    }
}
