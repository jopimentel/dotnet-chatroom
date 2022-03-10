FROM node:17.6.0-alpine as node
WORKDIR /app
EXPOSE 80

COPY . .
RUN npm install
RUN npm run build:prod

FROM nginx:alpine
COPY --from=node /app/dist/dotnet-chatroom-app /usr/share/nginx/html