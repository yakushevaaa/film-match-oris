import axios from "axios";
import { axiosSettings } from "@shared/api/axiosSettings";

interface RegisterRequest {
  name: string;
  email: string;
  password: string;
}

export const registerUser = async (
  data: RegisterRequest
): Promise<string | null> => {
  try {
    await axiosSettings.post("/User/register", data);
    return null;
  } catch (error: unknown) {
    if (axios.isAxiosError(error)) {
      return error.response?.data?.message || "Ошибка при регистрации";
    }
    return "Неизвестная ошибка";
  }
};
