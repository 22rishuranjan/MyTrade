// tools/gen-persisted-ops.mjs
import fs from "node:fs";
import path from "node:path";

const map = JSON.parse(fs.readFileSync("./persisted_queries.json", "utf8"));
fs.mkdirSync("./persisted_operations", { recursive: true });

for (const [id, text] of Object.entries(map)) {
    fs.writeFileSync(path.join("./persisted_operations", `${id}.graphql`), text.trim() + "\n");
}
