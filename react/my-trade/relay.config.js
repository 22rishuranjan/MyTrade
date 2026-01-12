module.exports = {
  src: './src',
  schema: './src/graphql/schema/schema.graphql',
  exclude: [
    '**/node_modules/**',
    '**/__mocks__/**',
    '**/__generated__/**'
  ],
  language: 'typescript',
  artifactDirectory: './src/graphql/__generated__',

  // Enable persisted queries
  persistConfig: {
    file: './persisted_queries.json',  // Output file
    algorithm: 'MD5',  // or 'SHA256'
  },
};