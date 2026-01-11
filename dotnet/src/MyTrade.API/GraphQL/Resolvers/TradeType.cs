using MyTrade.Domain.Entities;

namespace CleanArchitecture.API.GraphQL;

public sealed class TradeType : ObjectType<Trade>
{
    protected override void Configure(IObjectTypeDescriptor<Trade> descriptor)
    {
        descriptor.Field("id")
            .Type<NonNullType<StringType>>()
            .Resolve(ctx => ctx.Parent<Trade>().Id);

        // Optional: expose a cursor field if you want to debug (Relay doesn't need it)
        descriptor.Field("cursor")
            .Type<NonNullType<StringType>>()
            .Resolve(ctx =>
            {
                var t = ctx.Parent<Trade>();
                var raw = $"{t.ExecutionTime.ToUniversalTime().Ticks}|{t.Id}";
                return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(raw));
            });
    }
}
