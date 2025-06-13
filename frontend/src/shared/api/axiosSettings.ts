import axios from "axios";

export const axiosSettings = axios.create({
  baseURL: "http://localhost:5210",
});

// Функция для получения токена из cookie
function getCookie(name: string): string | undefined {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop()?.split(';').shift();
}

axiosSettings.interceptors.request.use(
  (config) => {
    const token = getCookie("token");
    if (token) {
      config.headers = config.headers || {};
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);
