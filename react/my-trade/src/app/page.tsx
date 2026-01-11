import Link from 'next/link';
import { Button } from '@/components/ui/Button';

export default function Home() {
  return (
    <main className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-20">
        <div className="text-center">
          <h1 className="text-6xl font-bold text-gray-900 mb-6">
            Welcome to <span className="text-blue-600">MyTrade</span>
          </h1>
          <p className="text-xl text-gray-600 mb-12 max-w-2xl mx-auto">
            Professional trading platform for managing orders, trades, and
            positions in real-time.
          </p>

          <div className="flex justify-center gap-4">
            <Link href="/dashboard">
              <Button size="lg">Go to Dashboard</Button>
            </Link>
            <Link href="/trades">
              <Button variant="secondary" size="lg">
                View Trades
              </Button>
            </Link>
          </div>

          <div className="mt-20 grid grid-cols-1 md:grid-cols-3 gap-8 max-w-4xl mx-auto">
            <div className="bg-white p-6 rounded-lg shadow-md">
              <h3 className="text-lg font-semibold text-gray-900 mb-2">
                Real-time Trading
              </h3>
              <p className="text-gray-600">
                Execute trades instantly with live market data and real-time
                order updates.
              </p>
            </div>

            <div className="bg-white p-6 rounded-lg shadow-md">
              <h3 className="text-lg font-semibold text-gray-900 mb-2">
                Portfolio Management
              </h3>
              <p className="text-gray-600">
                Track your positions, P&L, and account balances across multiple
                accounts.
              </p>
            </div>

            <div className="bg-white p-6 rounded-lg shadow-md">
              <h3 className="text-lg font-semibold text-gray-900 mb-2">
                Risk Controls
              </h3>
              <p className="text-gray-600">
                Comprehensive risk limits and monitoring to protect your
                investments.
              </p>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}