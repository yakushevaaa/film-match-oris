import { useEffect, useState } from 'react';
import { DataTable } from '@shared/ui/DataTable';
import { axiosSettings } from '@shared/api/axiosSettings';
import type { MRT_ColumnDef, MRT_Row } from 'material-react-table';

interface User {
  id: string;
  email: string;
  roles: string[];
}

export const UsersTable = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await axiosSettings.get('/User/GetAllUsers');
      setUsers(response.data);
    } catch (error) {
      console.error('Ошибка при получении пользователей:', error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleMakeAdmin = async (row: MRT_Row<User>) => {
    try {
      await axiosSettings.post(`/User/MakeAdmin/${row.original.id}`);
      fetchUsers(); // Обновляем список после изменения
    } catch (error) {
      console.error('Ошибка при назначении админа:', error);
    }
  };

  const handleBlockUser = async (row: MRT_Row<User>) => {
    try {
      if (row.original.roles.includes('Blocked')) {
        await axiosSettings.post(`/User/UnblockUser/${row.original.id}`);
      } else {
        await axiosSettings.post(`/User/BlockUser/${row.original.id}`);
      }
      fetchUsers(); // Обновляем список после изменения
    } catch (error) {
      console.error('Ошибка при блокировке/разблокировке пользователя:', error);
    }
  };

  const columns: MRT_ColumnDef<User>[] = [
    {
      accessorKey: 'email',
      header: 'Email',
      size: 250,
    },
    {
      accessorKey: 'roles',
      header: 'Роли',
      size: 200,
      Cell: ({ cell }) => cell.getValue<string[]>().join(', '),
    },
  ];

  return (
    <div style={{ padding: '20px' }}>
      <h1>Пользователи</h1>
      <DataTable
        data={users}
        columns={columns}
        onEdit={handleMakeAdmin}
        onDelete={handleBlockUser}
        isLoading={isLoading}
      />
    </div>
  );
}; 