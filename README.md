# dapr-dotnet-performance

## Deploy
```shell
docker compose --env-file .docker.env up -d
```

## Usage
```shell
curl -d '{"id": "someId", "foo": "bar"}' -H 'Content-Type: application/json' -X POST 'http://localhost:9000/store'
```

