namespace MyTrade.Domain.Enums
{
    public enum TradeSide { Buy, Sell }
    public enum OrderType { Market, Limit, Stop, StopLimit }
    public enum OrderStatus { Pending, PartiallyFilled, Filled, Cancelled, Rejected }
    public enum TradeStatus { Pending, Executed, Settled, Failed }
    public enum ClientType { Institutional, Retail, Corporate }
    public enum AccountType { Cash, Margin }
    public enum EntityStatus { Active, Inactive, Suspended }
    public enum KycStatus { Pending, Approved, Rejected, Expired }
    public enum LimitType { PositionLimit, OrderLimit, DailyVolume, Exposure }
    public enum SettlementStatus { Pending, Confirmed, Settled, Failed }
}
