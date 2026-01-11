import { Card } from "@/components/ui/Card";
import { CardContent } from "@/components/ui/CardContent";
import { CardHeader } from "@/components/ui/CardHeader";
import { CardTitle } from "@/components/ui/CardTitle";
import { graphql, useLazyLoadQuery } from "react-relay";
import type { DashboardContentQuery as DashboardContentQueryType } from "@/graphql/__generated__/DashboardContentQuery.graphql";
import { DashboardStats } from "@/components/trades/DashboardStats";
import { TradesTable } from "@/components/trades/TradeTable";

const DashboardQuery = graphql`
  query DashboardContentQuery($first: Int!, $after: String) {
    trades(first: $first, after: $after, orderBy: "executionTime_DESC") {
      edges {
        cursor
        node {
          id
          tradeId
          symbol
          side
          quantity
          price
          totalValue
          traderId
          clientId
          accountId
          orderType
          status
          executionTime
          commission
          fees
          currency
          settlementDate
          tradeDate
        }
      }
      pageInfo {
        hasNextPage
        hasPreviousPage
        startCursor
        endCursor
      }
      totalCount
    }
  }
`;

type TradeNode =
  NonNullable<
    NonNullable<DashboardContentQueryType["response"]["trades"]>["edges"]
  >[number]["node"];

type Trade = NonNullable<TradeNode>;

type TradesConnection = NonNullable<DashboardContentQueryType["response"]["trades"]>;
type PageInfo = TradesConnection["pageInfo"];

export function DashboardContent() {
  const data = useLazyLoadQuery<DashboardContentQueryType>(
    DashboardQuery,
    { first: 50, after: null },
    {
      // optional but often nice for dashboards:
      fetchPolicy: "store-and-network",
    }
  );

  const tradesConnection = data.trades;

  const trades: Trade[] =
    tradesConnection?.edges
      ?.map((edge) => edge?.node ?? null)
      .filter((node): node is Trade => node !== null) ?? [];

  const pageInfo: PageInfo | null = tradesConnection?.pageInfo ?? null;
  const totalCount = tradesConnection?.totalCount ?? 0;

  return (
    <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-gray-900">Trading Dashboard</h1>
        <p className="text-gray-600 mt-2">
          Monitor your trades and portfolio performance
        </p>
      </div>

      <DashboardStats trades={trades} />

      <Card className="mt-8">
        <CardHeader>
          <CardTitle>Recent Trades ({totalCount})</CardTitle>
        </CardHeader>
        <CardContent>
          <TradesTable trades={trades} />
        </CardContent>
      </Card>
    </main>
  );
}
