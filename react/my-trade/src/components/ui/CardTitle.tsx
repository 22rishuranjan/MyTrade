import { CardProps } from "@/components/ui/Card";
import { cn } from "@/lib/utils";
import React from 'react';

export const CardTitle: React.FC<CardProps> = ({ children, className }) => {
  return (
    <h3 className={cn('text-xl font-semibold text-gray-900', className)}>
      {children}
    </h3>
  );
};