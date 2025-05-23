# Etapa 1: Build
FROM node:20 AS build
WORKDIR /app

# Copia package.json e package-lock.json
COPY package*.json ./

# Instala dependências
RUN npm install

# Copia o restante do código
COPY . ./

# Compila o projeto
RUN npm run build

# Etapa 2: Deploy com NGINX
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Remove arquivos padrão do NGINX
RUN rm -rf ./*

# Copia os arquivos compilados para o NGINX
# ⚠️ Ajuste conforme o builder:
# Vite → dist
# CRA → build
COPY --from=build /app/dist . 

# Expor a porta padrão do NGINX
EXPOSE 80



# Comando padrão do NGINX
CMD ["nginx", "-g", "daemon off;"]
