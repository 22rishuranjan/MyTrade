"use client";

import { ReactNode, useMemo } from "react";
import { RelayEnvironmentProvider } from "react-relay";
import { createRelayEnvironment } from "@/graphql/client/RelayEnvironment";

export default function Providers({ children }: { children: ReactNode }) {
  const environment = useMemo(() => createRelayEnvironment(), []);

  return (
    <RelayEnvironmentProvider environment={environment}>
      {children}
    </RelayEnvironmentProvider>
  );
}