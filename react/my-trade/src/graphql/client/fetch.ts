import { FetchFunction, RequestParameters, Variables } from "relay-runtime";

export const fetchGraphQL: FetchFunction = async (request: RequestParameters, variables: Variables) => {
  console.log("Requesting GraphQL:", request.text, variables, process.env.NEXT_PUBLIC_GRAPHQL_ENDPOINT);
  const resp = await fetch("http://localhost:5002/graphql/", {
    method: "POST",
      headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json',
    },
    credentials: 'include',
    body: JSON.stringify({
      id: request.id, // sent by Relay runtime
      variables,
    }),
  });

  return resp.json();
};