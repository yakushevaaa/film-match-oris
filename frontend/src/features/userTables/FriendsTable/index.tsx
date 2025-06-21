import { useEffect, useState } from 'react';
import { MaterialReactTable, type MRT_ColumnDef, type MRT_Row } from 'material-react-table';
import { axiosSettings } from '@shared/api/axiosSettings';
import styles from './index.module.scss';

interface Friend {
  id: string;
  name: string;
  email: string;
  friendId: string;
}

export const FriendsTable = () => {
  const [friends, setFriends] = useState<Friend[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    fetchFriends();
  }, []);

  const fetchFriends = async () => {
    setIsLoading(true);
    setError('');
    try {
      const res = await axiosSettings.get('/AllUserFriends');
      setFriends(res.data.friends || []);
    } catch {
      setError('Не удалось загрузить друзей');
    } finally {
      setIsLoading(false);
    }
  };

  const handleDeleteFriend = async (row: MRT_Row<Friend>) => {
    try {
      await axiosSettings.delete(`/DeleteFriend/${row.original.friendId}`);
      setFriends((prev) => prev.filter((f) => f.friendId !== row.original.friendId));
    } catch {
      setError('Не удалось удалить друга');
    }
  };

  const columns: MRT_ColumnDef<Friend>[] = [
    { accessorKey: 'name', header: 'Имя', size: 200 },
    { accessorKey: 'email', header: 'Почта', size: 250 },
    {
      header: 'Действия',
      id: 'actions',
      size: 120,
      Cell: ({ row }) => (
        <button className={styles.deleteFriendButton} onClick={() => handleDeleteFriend(row)}>
          Удалить
        </button>
      ),
    },
  ];

  return (
    <div style={{ padding: 20 }}>
      {error && <div style={{ color: 'red', marginBottom: 8 }}>{error}</div>}
      <MaterialReactTable
        columns={columns}
        data={friends}
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