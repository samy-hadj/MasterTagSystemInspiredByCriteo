FROM node:18.19-alpine
WORKDIR /app

COPY frontend-app/package*.json ./
RUN npm install
RUN npm install -g @angular/cli@16.2.0

COPY frontend-app/ .

EXPOSE 4200

# Ajout des options pour une meilleure accessibilité réseau
CMD ["ng", "serve", "--host", "0.0.0.0", "--disable-host-check", "--poll=2000", "--port", "4200", "--public-host", "localhost:4200"]