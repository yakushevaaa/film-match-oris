import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7182' 
  }
);

export default api;