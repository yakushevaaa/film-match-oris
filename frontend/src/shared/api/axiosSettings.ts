import axios from "axios";

export const axiosSettings = axios.create({
  baseURL: "http://localhost:5210",
});

export function apiSetHeader(name: string, value: string) {
  if (value) {
    axiosSettings.defaults.headers.common[name] = value;
  }
}


const JWTToken = localStorage.getItem("token");
if (JWTToken) {
  apiSetHeader("Authorization", `Bearer ${JWTToken}`);
}


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
