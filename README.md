# Encryption API CI/CD

Detta projekt innehåller ett enkelt C#-API med två endpoints för kryptering och avkryptering via ett Caesar-chiffer. Fokus ligger på att visa en komplett CI/CD-kedja med Git Flow, GitHub Actions och driftsättning till AWS Elastic Beanstalk.

## Endpoints

- `POST /encrypt` – krypterar texten.
- `POST /decrypt` – avkrypterar texten.

Exempel:

```json
POST /encrypt
{
  "text": "Hej världen",
  "shift": 3
}
```

Svar:

```json
{
  "result": "Kho yöugoghq"
}
```

## Git Flow

- `main` är produktionsgren.
- `develop` är integrationsgren.
- Feature-branches skapas från `develop` och mergas via Pull Requests.

## CI/CD

GitHub Actions används för att:

1. Bygga och testa projektet på varje pull request.
2. Publicera och deploya till AWS Elastic Beanstalk när `main` uppdateras.

Se `docs/ci-cd-process.md` för den visuella processen och beskrivning av branch-strategi.

## Lokal utveckling

```bash
dotnet restore src/EncryptionApi/EncryptionApi.csproj

dotnet run --project src/EncryptionApi/EncryptionApi.csproj
```

## Tester

```bash
dotnet test tests/EncryptionApi.Tests/EncryptionApi.Tests.csproj
```

## Deployment-krav

Följande secrets måste finnas i GitHub för att deployment ska fungera:

- `AWS_ACCESS_KEY_ID`
- `AWS_SECRET_ACCESS_KEY`
- `AWS_REGION`
- `EB_APPLICATION_NAME`
- `EB_ENVIRONMENT_NAME`
- `EB_S3_BUCKET`

Workflow-filen finns i `.github/workflows/deploy.yml`.
