import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import './globals.css';
import { RelayProvider } from '@/graphql/client/RelayProvider';

const inter = Inter({ subsets: ['latin'] });

export const metadata: Metadata = {
  title: 'MyTrade - Professional Trading Platform',
  description: 'Execute trades, manage portfolios, and monitor markets in real-time',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <RelayProvider>
          {children}
        </RelayProvider>
      </body>
    </html>
  );
}