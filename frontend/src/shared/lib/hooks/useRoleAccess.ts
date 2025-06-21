import { useMemo } from 'react';
import { getUserRole } from '../tokenUtils';

export type UserRole = 'God' | 'Admin' | 'User' | null;

export const useRoleAccess = () => {
  const token = localStorage.getItem('token');
  const role = useMemo<UserRole>(() => getUserRole(token) as UserRole, [token]);

  const isGod = role === 'God';
  const isAdmin = role === 'Admin';
  const isUser = role === 'User';

  // Проверка доступа к секции
  const hasAccessTo = (section: 'god' | 'admin' | 'user') => {
    if (section === 'god') return isGod;
    if (section === 'admin') return isGod || isAdmin;
    if (section === 'user') return isGod || isAdmin || isUser;
    return false;
  };

  return { role, isGod, isAdmin, isUser, hasAccessTo };
}; 