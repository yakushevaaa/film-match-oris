import { useEffect, useState } from "react";
import Modal from "react-modal";
import styles from "./index.module.scss";
import { axiosSettings } from "@shared/api/axiosSettings";

// Настройка корневого элемента для модалки (один раз на приложение)
if (typeof window !== "undefined") {
  Modal.setAppElement("#root");
}

interface AddFriendModalProps {
  isOpen: boolean;
  onClose: () => void;
}

interface UserProfile {
  id: string;
  username: string;
}

export const AddFriendModal = ({ isOpen, onClose }: AddFriendModalProps) => {
  const [users, setUsers] = useState<UserProfile[]>([]);
  const [search, setSearch] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    if (!isOpen) return;
    setLoading(true);
    setError("");
    axiosSettings
      .get("/User/usernames")
      .then((res) => setUsers(res.data.useranames || []))
      .catch(() => setError("Не удалось загрузить пользователей"))
      .finally(() => setLoading(false));
  }, [isOpen]);

  const filteredUsers = users.filter((user) =>
    user.username.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <Modal
      isOpen={isOpen}
      onRequestClose={onClose}
      className={styles.modalContent}
      overlayClassName={styles.modalOverlay}
      contentLabel="Добавить в друзья"
      shouldCloseOnOverlayClick={true}
    >
      <button className={styles.closeButton} onClick={onClose}>
        ×
      </button>
      <h2 className={styles.title}>Добавить в друзья</h2>
      <input
        type="text"
        placeholder="Поиск по имени"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        className={styles.searchInput}
      />
      {loading ? (
        <div>Загрузка...</div>
      ) : error ? (
        <div className={styles.error}>{error}</div>
      ) : (
        <ul className={styles.userList}>
          {filteredUsers.length > 0 ? (
            filteredUsers.map((user) => (
              <li key={user.id} className={styles.userItem}>
                <span className={styles.username}>{user.username}</span>
                <button className={styles.addButton}>Добавить в друзья</button>
              </li>
            ))
          ) : (
            <li className={styles.noResults}>Пользователи не найдены</li>
          )}
        </ul>
      )}
    </Modal>
  );
};