const fs = require("fs");

const LEVLELS_COUNT = 200;

const levels = [];

for (let i = 0; i < LEVLELS_COUNT; i++) {
  levels.push({
    number: i + 1,
    seed: Math.floor(Math.random() * 1000000),
    length: 200 + i * 3 + Math.floor(Math.random() * 50),
  });
}

fs.writeFileSync("Assets/levels.json", JSON.stringify({ levels }));
