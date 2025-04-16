import { defineConfig, loadEnv } from 'vite';
import react from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
    const env = loadEnv(mode, process.cwd(), '');

    return {
        plugins: [react()],
        server: {
            port: parseInt(env.VITE_PORT),
            proxy: {
                '/api': {
                    target: process.env.services__api__https__0 ||
                        process.env.services__api__http__0,
                    changeOrigin: true,
                    rewrite: (path) => path.replace(/^\/api/, ''),
                    secure: false,
                },
                '/chat': {
                    target: process.env.services__api__https__0 ||
                        process.env.services__api__http__0,
                    ws: true,
                    secure: false
                }
            },
        },
        build: {
            outDir: 'dist',
            rollupOptions: {
                input: './index.html'
            }
        }
    }
})