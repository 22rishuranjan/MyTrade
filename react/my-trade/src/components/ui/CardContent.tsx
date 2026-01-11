
import { CardProps } from "@/components/ui/Card";
import { cn } from "@/lib/utils";
import React from 'react';

export const CardContent: React.FC<CardProps> = ({ children, className }) => {
  return <div className={cn('', className)}>{children}</div>;
};