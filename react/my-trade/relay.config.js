/** @type {import('relay-compiler').Config} */
module.exports = {
  src: "./src",
  schema: "./src/graphql/schema/schema.graphql",
  language: "typescript",

  // IMPORTANT: keep artifacts out of Next route directories (pages/app)
  artifactDirectory: "./src/graphql/__generated__",

  // optional but nice
  exclude: ["**/node_modules/**", "**/.next/**"],
};
