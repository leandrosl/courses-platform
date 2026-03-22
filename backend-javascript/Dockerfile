FROM oven/bun:1
WORKDIR /usr/src/app

COPY package.json ./
RUN bun install

COPY . .

EXPOSE 8080
CMD ["bun", "run", "src/server.js"]
