import axios from "axios";

export const axiosSettings = axios.create({
  baseURL: "http://localhost:5210",
});

// Добавляю interceptor для подстановки токена авторизации
axiosSettings.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers = config.headers || {};
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);
