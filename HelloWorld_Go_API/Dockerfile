# Etapa 1: Compilación de la aplicación Go
FROM golang:1.22-alpine AS build

WORKDIR /app

COPY . .

RUN go mod tidy
RUN go build -o /hello-world-api

# Etapa 2: Creación de la imagen final
FROM alpine:3.18

WORKDIR /

COPY --from=build /hello-world-api /hello-world-api

EXPOSE 8080

ENTRYPOINT ["/hello-world-api"]
