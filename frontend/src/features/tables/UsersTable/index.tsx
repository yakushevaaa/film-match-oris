import { useEffect, useState } from 'react';
import { MaterialReactTable, type MRT_ColumnDef, type MRT_Row } from 'material-react-table';
import { axiosSettings } from '@shared/api/axiosSettings';
import styles from './index.module.scss';

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
      fetchUsers();
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
      fetchUsers(); 
    } catch (error) {
      console.error('Ошибка при блокировке/разблокировке пользователя:', error);
    }
  };

  const columns: MRT_ColumnDef<User>[] = [
    {
      header: 'Действия',
      id: 'actions',
      size: 250,
      Cell: ({ row }) => (
        <div style={{ display: 'flex', gap: 8 }}>
          {!row.original.roles.includes('Admin') && (
            <button className={styles.userActionButton} onClick={() => handleMakeAdmin(row)}>
              Сделать админом
            </button>
          )}
          {row.original.roles.includes('Blocked') ? (
            <button className={styles.userActionButton} onClick={() => handleBlockUser(row)}>
              Разблокировать
            </button>
          ) : (
            <button className={styles.userActionButton} onClick={() => handleBlockUser(row)}>
              Заблокировать
            </button>
          )}
        </div>
      ),
    },
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
      <MaterialReactTable
        columns={columns}
        data={users}
        state={{ isLoading }}
        enableRowActions={false}
        enableColumnActions={false}
        enableSorting={false}
        enableFullScreenToggle={false}
        enableDensityToggle={false}
        enableHiding={false}
        enableFilters={false}
        enableGlobalFilter={false}
        enablePagination={true}
        muiTableBodyRowProps={{ hover: true }}
      />
    </div>
  );
}; 