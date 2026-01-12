import {
  Environment,
  Network,
  RecordSource,
  Store,
} from "relay-runtime";
import { fetchGraphQL } from "@/graphql/client/fetch";


export function createRelayEnvironment() {
  return new Environment({
    network: Network.create(fetchGraphQL),
    store: new Store(new RecordSource()),
  });
}
