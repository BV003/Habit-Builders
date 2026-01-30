# Use official Node image
FROM node:20-alpine

# Set working directory
WORKDIR /app

# Copy dependency files first (for Docker cache)
COPY package.json package-lock.json ./

# Install dependencies
RUN npm ci

# Copy entire project
COPY . .

# Expose Vite dev server port
EXPOSE 5173

# Start dev server with host flag to allow external access
CMD ["npm", "run", "dev", "--", "--host", "0.0.0.0"]
