import { CardProps } from "@/components/ui/Card";
import { cn } from "@/lib/utils";
import React from 'react';

export const CardHeader: React.FC<CardProps> = ({ children, className }) => {
  return (
    <div className={cn('mb-4 border-b border-gray-200 pb-3', className)}>
      {children}
    </div>
  );
};