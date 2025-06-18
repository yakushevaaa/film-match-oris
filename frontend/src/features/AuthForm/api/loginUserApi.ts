import axios from "axios";
import { axiosSettings } from "@shared/api/axiosSettings";

interface LoginRequest {
  email: string;
  password: string;
}

export const loginUser = async (data: LoginRequest): Promise<string | null> => {
  try {
    const response = await axiosSettings.post("/User/login", data);
    const token = response.data.token;
    if (token) {
      localStorage.setItem("token", token);
    }
    return null;
  } catch (error: unknown) {
    if (axios.isAxiosError(error)) {
      return error.response?.data?.message || "Ошибка при входе";
    }
    return "Неизвестная ошибка";
  }
};
