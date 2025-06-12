import axios from "axios";

export const axiosSettings = axios.create({
  baseURL: "http://localhost:5210",
});
