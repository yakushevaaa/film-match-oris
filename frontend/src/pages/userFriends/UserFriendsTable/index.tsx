import { useState } from "react";
import styles from "./index.module.scss";

interface Friend {
  id: string;
  name: string;
  email: string;
}

const mockFriends: Friend[] = [
  { id: "1", name: "Иван Иванов", email: "ivan@example.com" },
  { id: "2", name: "Мария Петрова", email: "maria@example.com" },
  { id: "3", name: "Алексей Смирнов", email: "alexey@example.com" },
];

export const UserFriendsTable = () => {
  const [search, setSearch] = useState("");

  const filteredFriends = mockFriends.filter(
    (friend) =>
      friend.name.toLowerCase().includes(search.toLowerCase()) ||
      friend.email.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className={styles.tableWrapper}>
      <input
        type="text"
        placeholder="Поиск по имени или почте"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        className={styles.searchInput}
      />
      <table className={styles.table}>
        <thead>
          <tr>
            <th>Имя</th>
            <th>Почта</th>
          </tr>
        </thead>
        <tbody>
          {filteredFriends.length > 0 ? (
            filteredFriends.map((friend) => (
              <tr key={friend.id}>
                <td>{friend.name}</td>
                <td>{friend.email}</td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={2}>Ничего не найдено</td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};
