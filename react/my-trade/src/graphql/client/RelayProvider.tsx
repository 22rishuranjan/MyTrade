'use client';

import { ReactNode } from 'react';
import { RelayEnvironmentProvider } from 'react-relay';
import { createRelayEnvironment } from '@/graphql/client/RelayEnvironment';

interface RelayProviderProps {
  children: ReactNode;
}

export function RelayProvider({ children }: RelayProviderProps) {
  const environment = createRelayEnvironment();
  return (
    <RelayEnvironmentProvider environment={environment}>
      {children}
    </RelayEnvironmentProvider>
  );
}

