'use client';

import { PageLayout } from "@/components/layout/PageLayout";
import { DashboardContent } from "@/components/trades/DashboardContent";


export default function DashboardPage() {
  return (
    <PageLayout>
      <DashboardContent />
    </PageLayout>
  );
}