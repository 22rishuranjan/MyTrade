/**
 * @generated SignedSource<<d7241f66fcd74eac568937bcd929d19b>>
 * @relayHash f12173917aba01dcfc0acd2f6a6d3c4c
 * @lightSyntaxTransform
 * @nogrep
 */

/* tslint:disable */
/* eslint-disable */
// @ts-nocheck

// @relayRequestID f12173917aba01dcfc0acd2f6a6d3c4c

import { ConcreteRequest } from 'relay-runtime';
export type OrderType = "LIMIT" | "MARKET" | "STOP" | "STOP_LIMIT" | "%future added value";
export type TradeSide = "BUY" | "SELL" | "%future added value";
export type TradeStatus = "EXECUTED" | "FAILED" | "PENDING" | "SETTLED" | "%future added value";
export type DashboardContentQuery$variables = {
  after?: string | null | undefined;
  first: number;
};
export type DashboardContentQuery$data = {
  readonly trades: {
    readonly edges: ReadonlyArray<{
      readonly cursor: string;
      readonly node: {
        readonly accountId: string;
        readonly clientId: string;
        readonly commission: any;
        readonly currency: string;
        readonly executionTime: any;
        readonly fees: any;
        readonly id: string;
        readonly orderType: OrderType;
        readonly price: any;
        readonly quantity: any;
        readonly settlementDate: any;
        readonly side: TradeSide;
        readonly status: TradeStatus;
        readonly symbol: string;
        readonly totalValue: any;
        readonly tradeDate: any;
        readonly tradeId: string;
        readonly traderId: string;
      };
    }> | null | undefined;
    readonly pageInfo: {
      readonly endCursor: string | null | undefined;
      readonly hasNextPage: boolean;
      readonly hasPreviousPage: boolean;
      readonly startCursor: string | null | undefined;
    };
    readonly totalCount: number;
  } | null | undefined;
};
export type DashboardContentQuery = {
  response: DashboardContentQuery$data;
  variables: DashboardContentQuery$variables;
};

const node: ConcreteRequest = (function(){
var v0 = {
  "defaultValue": null,
  "kind": "LocalArgument",
  "name": "after"
},
v1 = {
  "defaultValue": null,
  "kind": "LocalArgument",
  "name": "first"
},
v2 = [
  {
    "alias": null,
    "args": [
      {
        "kind": "Variable",
        "name": "after",
        "variableName": "after"
      },
      {
        "kind": "Variable",
        "name": "first",
        "variableName": "first"
      },
      {
        "kind": "Literal",
        "name": "orderBy",
        "value": "executionTime_DESC"
      }
    ],
    "concreteType": "TradesConnection",
    "kind": "LinkedField",
    "name": "trades",
    "plural": false,
    "selections": [
      {
        "alias": null,
        "args": null,
        "concreteType": "TradesEdge",
        "kind": "LinkedField",
        "name": "edges",
        "plural": true,
        "selections": [
          {
            "alias": null,
            "args": null,
            "kind": "ScalarField",
            "name": "cursor",
            "storageKey": null
          },
          {
            "alias": null,
            "args": null,
            "concreteType": "Trade",
            "kind": "LinkedField",
            "name": "node",
            "plural": false,
            "selections": [
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "id",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "tradeId",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "symbol",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "side",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "quantity",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "price",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "totalValue",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "traderId",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "clientId",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "accountId",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "orderType",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "status",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "executionTime",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "commission",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "fees",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "currency",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "settlementDate",
                "storageKey": null
              },
              {
                "alias": null,
                "args": null,
                "kind": "ScalarField",
                "name": "tradeDate",
                "storageKey": null
              }
            ],
            "storageKey": null
          }
        ],
        "storageKey": null
      },
      {
        "alias": null,
        "args": null,
        "concreteType": "PageInfo",
        "kind": "LinkedField",
        "name": "pageInfo",
        "plural": false,
        "selections": [
          {
            "alias": null,
            "args": null,
            "kind": "ScalarField",
            "name": "hasNextPage",
            "storageKey": null
          },
          {
            "alias": null,
            "args": null,
            "kind": "ScalarField",
            "name": "hasPreviousPage",
            "storageKey": null
          },
          {
            "alias": null,
            "args": null,
            "kind": "ScalarField",
            "name": "startCursor",
            "storageKey": null
          },
          {
            "alias": null,
            "args": null,
            "kind": "ScalarField",
            "name": "endCursor",
            "storageKey": null
          }
        ],
        "storageKey": null
      },
      {
        "alias": null,
        "args": null,
        "kind": "ScalarField",
        "name": "totalCount",
        "storageKey": null
      }
    ],
    "storageKey": null
  }
];
return {
  "fragment": {
    "argumentDefinitions": [
      (v0/*: any*/),
      (v1/*: any*/)
    ],
    "kind": "Fragment",
    "metadata": null,
    "name": "DashboardContentQuery",
    "selections": (v2/*: any*/),
    "type": "Query",
    "abstractKey": null
  },
  "kind": "Request",
  "operation": {
    "argumentDefinitions": [
      (v1/*: any*/),
      (v0/*: any*/)
    ],
    "kind": "Operation",
    "name": "DashboardContentQuery",
    "selections": (v2/*: any*/)
  },
  "params": {
    "id": "f12173917aba01dcfc0acd2f6a6d3c4c",
    "metadata": {},
    "name": "DashboardContentQuery",
    "operationKind": "query",
    "text": null
  }
};
})();

(node as any).hash = "97306787a7cad9bc95aa7f197386fea4";

export default node;
