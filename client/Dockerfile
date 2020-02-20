# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# production stage
FROM nginx:stable-alpine as production-stage

COPY --from=build-stage /app/dist /usr/share/nginx/html
COPY --from=build-stage /app/nginx/nginx.conf /etc/nginx/nginx.conf
COPY --from=build-stage /app/nginx/localhost.crt /etc/ssl/certs/localhost.crt
COPY --from=build-stage /app/nginx/localhost.key /etc/ssl/private/localhost.key

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
