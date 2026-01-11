import {
  Environment,
  Network,
  RecordSource,
  Store,
  FetchFunction,
} from "relay-runtime";

const fetchGraphQL: FetchFunction = async (request, variables) => {
  console.log("Requesting GraphQL:", request.text, variables, process.env.NEXT_PUBLIC_GRAPHQL_ENDPOINT);
  const resp = await fetch("http://localhost:5002/graphql/", {
    method: "POST",
      headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
    credentials: 'include', //  for cookies/auth
    body: JSON.stringify({
      query: request.text, // sent by Relay runtime
      variables,
    }),
  });

  return resp.json();
};

export function createRelayEnvironment() {
  return new Environment({
    network: Network.create(fetchGraphQL),
    store: new Store(new RecordSource()),
  });
}
