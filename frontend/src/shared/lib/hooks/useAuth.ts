import { useState, useEffect } from 'react';
import { axiosSettings } from '@shared/api/axiosSettings';

interface User {
  id: string;
  email: string;
  roles: string[];
}

export const useAuth = () => {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await axiosSettings.get('/User/GetCurrentUser');
        setUser(response.data);
      } catch (error) {
        console.error('Ошибка при получении данных пользователя:', error);
        setUser(null);
      } finally {
        setLoading(false);
      }
    };

    fetchUser();
  }, []);

  return { user, loading };
}; 