import { useEffect, useState } from "react";
import styles from "./index.module.scss";
import { axiosSettings } from "@shared/api/axiosSettings";
import { useAuth } from "@shared/lib/hooks/useAuth";

interface User {
  id: string;
  email: string;
  roles: string[];
}

export const AdminPanel = () => {
  const [users, setUsers] = useState<User[]>([]);
  const { user } = useAuth();
  const isGod = user?.roles.includes("God");

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await axiosSettings.get("/User/GetAllUsers");
      setUsers(response.data);
    } catch (error) {
      console.error("Ошибка при получении пользователей:", error);
    }
  };

  const handleMakeAdmin = async (userId: string) => {
    try {
      await axiosSettings.post(`/User/MakeAdmin/${userId}`);
      fetchUsers(); // Обновляем список пользователей
    } catch (error) {
      console.error("Ошибка при назначении админа:", error);
    }
  };

  const handleBlockUser = async (userId: string) => {
    try {
      await axiosSettings.post(`/User/BlockUser/${userId}`);
      fetchUsers();
    } catch (error) {
      console.error("Ошибка при блокировке пользователя:", error);
    }
  };

  const handleUnblockUser = async (userId: string) => {
    try {
      await axiosSettings.post(`/User/UnblockUser/${userId}`);
      fetchUsers();
    } catch (error) {
      console.error("Ошибка при разблокировке пользователя:", error);
    }
  };

  return (
    <div className={styles.adminPanel}>
      <h1>Панель администратора</h1>
      
      {/* Секция управления пользователями */}
      <section className={styles.usersSection}>
        <h2>Управление пользователями</h2>
        <table className={styles.usersTable}>
          <thead>
            <tr>
              <th>Email</th>
              <th>Роли</th>
              <th>Действия</th>
            </tr>
          </thead>
          <tbody>
            {users.map((user) => (
              <tr key={user.id}>
                <td>{user.email}</td>
                <td>{user.roles.join(", ")}</td>
                <td>
                  {isGod && !user.roles.includes("Admin") && (
                    <button
                      onClick={() => handleMakeAdmin(user.id)}
                      className={styles.actionButton}
                    >
                      Сделать админом
                    </button>
                  )}
                  {user.roles.includes("Blocked") ? (
                    <button
                      onClick={() => handleUnblockUser(user.id)}
                      className={styles.actionButton}
                    >
                      Разблокировать
                    </button>
                  ) : (
                    <button
                      onClick={() => handleBlockUser(user.id)}
                      className={styles.actionButton}
                    >
                      Заблокировать
                    </button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </section>

      {/* Секция управления фильмами */}
      <section className={styles.filmsSection}>
        <h2>Управление фильмами</h2>
        <div className={styles.filmActions}>
          <button className={styles.actionButton}>Добавить фильм</button>
          <button className={styles.actionButton}>Редактировать фильм</button>
          <button className={styles.actionButton}>Удалить фильм</button>
        </div>
      </section>
    </div>
  );
}; 