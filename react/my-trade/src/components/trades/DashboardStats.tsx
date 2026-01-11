'use client';

import { Card } from '@/components/ui/Card';
import { CardContent } from '@/components/ui/CardContent';
import { formatCurrency } from '@/lib/utils';
import { TrendingUp, TrendingDown, Activity, DollarSign } from 'lucide-react';

interface Trade {
  totalValue: number;
  side: string;
  commission: number;
  fees: number;
  status: string;
}

interface DashboardStatsProps {
  trades: Trade[];
}

export function DashboardStats({ trades }: DashboardStatsProps) {
  const totalVolume = trades.reduce((sum, trade) => sum + trade.totalValue, 0);
  const totalBuyVolume = trades
    .filter((t) => t.side === 'Buy')
    .reduce((sum, trade) => sum + trade.totalValue, 0);
  const totalSellVolume = trades
    .filter((t) => t.side === 'Sell')
    .reduce((sum, trade) => sum + trade.totalValue, 0);
  // const totalCommissions = trades.reduce(
  //   (sum, trade) => sum + trade.commission + trade.fees,
  //   0
  // );
  const executedTrades = trades.filter((t) => t.status === 'Executed' || t.status === 'Settled').length;

  const stats = [
    {
      title: 'Total Volume',
      value: formatCurrency(totalVolume),
      icon: DollarSign,
      color: 'text-blue-600',
      bgColor: 'bg-blue-100',
    },
    {
      title: 'Buy Volume',
      value: formatCurrency(totalBuyVolume),
      icon: TrendingUp,
      color: 'text-green-600',
      bgColor: 'bg-green-100',
    },
    {
      title: 'Sell Volume',
      value: formatCurrency(totalSellVolume),
      icon: TrendingDown,
      color: 'text-red-600',
      bgColor: 'bg-red-100',
    },
    {
      title: 'Total Trades',
      value: executedTrades.toString(),
      icon: Activity,
      color: 'text-purple-600',
      bgColor: 'bg-purple-100',
    },
  ];

  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
      {stats.map((stat) => (
        <Card key={stat.title}>
          <CardContent className="pt-6">
            <div className="flex items-center justify-between">
              <div>
                <p className="text-sm font-medium text-gray-600">
                  {stat.title}
                </p>
                <p className="text-2xl font-bold text-gray-900 mt-2">
                  {stat.value}
                </p>
              </div>
              <div className={`p-3 rounded-full ${stat.bgColor}`}>
                <stat.icon className={`w-6 h-6 ${stat.color}`} />
              </div>
            </div>
          </CardContent>
        </Card>
      ))}
    </div>
  );
}