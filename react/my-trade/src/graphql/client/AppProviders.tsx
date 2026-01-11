'use client';

import { ReactNode } from 'react';
import { RelayProvider } from '@/graphql/client/RelayProvider';

interface AppProvidersProps {
  children: ReactNode;
}

export function AppProviders({ children }: AppProvidersProps) {
  return (
    <RelayProvider>
      {children}
    </RelayProvider>
  );
}