import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
  reactCompiler: true,
  compiler: {
    relay: {
      // This must match the Relay compiler artifact directory
      src: "./src",
      artifactDirectory: "./src/graphql/__generated__",
      language: "typescript"
    }
  }

};

export default nextConfig;
