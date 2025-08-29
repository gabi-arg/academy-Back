# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamos el csproj y restauramos dependencias (acelera cache)
COPY ["ContactApi.csproj", "./"]
RUN dotnet restore "ContactApi.csproj"

# Copiamos el resto del proyecto y publicamos
COPY . .
RUN dotnet publish "ContactApi.csproj" -c Release -o /app/publish

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Creamos carpeta persistente para la DB dentro del contenedor
RUN mkdir -p /app/data

# Copiamos la app publicada
COPY --from=build /app/publish ./

# (Opcional) si quieres incluir la DB inicial que está en el repo:
# COPY academy.db /app/data/academy.db

# Configuraciones
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "ContactApi.dll"]
