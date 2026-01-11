'use client';

import React, { ComponentType, SVGProps } from 'react';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { cn } from '@/lib/utils';
import { navigationRoutes } from '@/lib/routes';
import { 
  LayoutDashboard, 
  TrendingUp, 
  FileText, 
  Wallet,
  User,
  Settings
} from 'lucide-react';

const iconMap: Record<string, ComponentType<SVGProps<SVGSVGElement>>> = {
  Dashboard: LayoutDashboard,
  Trade: TrendingUp,
  Orders: FileText,
  Positions: Wallet,
  Settings: Settings,
};

export const Header= () => {
  const pathname = usePathname();

  return (
    <header className="bg-white shadow-sm border-b border-gray-200 sticky top-0 z-50">
      <nav className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between h-16 items-center">
          <div className="flex items-center space-x-8">
            <Link href="/" className="flex items-center">
              <TrendingUp className="w-8 h-8 text-blue-600" />
              <span className="ml-2 text-2xl font-bold text-blue-600">
                MyTrade
              </span>
            </Link>

            <div className="hidden md:flex space-x-1">
              {navigationRoutes.map((route) => {
                const isActive = pathname === route.path;
                const Icon = iconMap[route.name] || LayoutDashboard;
                
                return (
                  <Link
                    key={route.path}
                    href={route.path}
                    className={cn(
                      'flex items-center px-4 py-2 text-sm font-medium rounded-lg transition-colors',
                      isActive
                        ? 'bg-blue-50 text-blue-700'
                        : 'text-gray-700 hover:bg-gray-100 hover:text-gray-900'
                    )}
                  >
                    <Icon className="w-4 h-4 mr-2" />
                    {route.name}
                  </Link>
                );
              })}
            </div>
          </div>

          <div className="flex items-center space-x-4">
            <div className="hidden sm:block text-right">
              <p className="text-xs text-gray-500">Account</p>
              <p className="text-sm font-semibold text-gray-900">ACC-001</p>
            </div>
            <div className="flex items-center space-x-2 px-3 py-2 bg-gray-100 rounded-lg">
              <User className="w-4 h-4 text-gray-600" />
              <span className="text-sm font-medium text-gray-900">TDR-001</span>
            </div>
          </div>
        </div>
      </nav>
    </header>
  );
}
