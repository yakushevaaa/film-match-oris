import { useState, useEffect } from "react";
import styles from "./index.module.scss";
import { axiosSettings } from "@shared/api/axiosSettings";
import { Search } from "@/shared/components/ui/Search";

interface Friend {
  id: string;
  friendId: string;
  name: string;
  email: string;
}

export const UserFriendsTable = () => {
  const [search, setSearch] = useState("");
  const [friends, setFriends] = useState<Friend[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    setLoading(true);
    setError("");
    axiosSettings
      .get<{ friends: Friend[] }>("/AllUserFriends")
      .then((res) => setFriends(res.data.friends || []))
      .catch(() => setError("Не удалось загрузить друзей"))
      .finally(() => setLoading(false));
  }, []);

  const filteredFriends = friends.filter(
    (friend) =>
      friend.name.toLowerCase().includes(search.toLowerCase()) ||
      friend.email.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className={styles.tableWrapper}>
      <Search
        type="text"
        placeholder="Поиск друга"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        className={styles.searchInput}
      />
      {loading ? (
        <div>Загрузка...</div>
      ) : error ? (
        <div className={styles.error}>{error}</div>
      ) : (
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
      )}
    </div>
  );
};
