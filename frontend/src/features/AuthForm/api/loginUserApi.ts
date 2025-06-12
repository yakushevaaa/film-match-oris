import axios from "axios";
import { axiosSettings } from "@shared/api/axiosSettings";

interface LoginRequest {
  email: string;
  password: string;
}

export const loginUser = async (data: LoginRequest): Promise<string | null> => {
  try {
    await axiosSettings.post("/User/login", data);
    return null;
  } catch (error: unknown) {
    if (axios.isAxiosError(error)) {
      return error.response?.data?.message || "Ошибка при входе";
    }
    return "Неизвестная ошибка";
  }
};
