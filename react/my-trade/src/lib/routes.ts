
export interface Route {
  name: string;
  path: string;
  icon?: string;
  description?: string;
}

export const routes = {
  home: {
    name: 'Home',
    path: '/',
    description: 'Landing page',
  },
  dashboard: {
    name: 'Dashboard',
    path: '/dashboard',
    description: 'Trading overview and statistics',
  },
  trade: {
    name: 'Trade',
    path: '/trade',
    description: 'Main trading interface',
  },
  orders: {
    name: 'Orders',
    path: '/orders',
    description: 'View and manage orders',
  },
  positions: {
    name: 'Positions',
    path: '/positions',
    description: 'View open positions',
  },
  accounts: {
    name: 'Accounts',
    path: '/accounts',
    description: 'Account management',
  },
  settings: {
    name: 'Settings',
    path: '/settings',
    description: 'Application settings',
  },
} as const;

export type RouteKey = keyof typeof routes;

export const navigationRoutes: Route[] = [
  routes.dashboard,
  routes.trade,
  routes.orders,
  routes.positions,
];

export const getRoute = (key: RouteKey): Route => {
  return routes[key];
};