FROM node as build
WORKDIR /usr/src/app
COPY teambarometer/package.json teambarometer/yarn.lock ./
RUN yarn
COPY teambarometer/ ./
RUN yarn build

FROM nginx:alpine
COPY --from=build /usr/src/app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]