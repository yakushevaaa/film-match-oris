import styles from "./index.module.scss";
import { UserFriendsTable } from "./UserFriendsTable";
import { useState } from "react";
import { AddFriendModal } from "./AddFriendModal";

export function UserFriends() {
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    <>
      <div className={styles.titleContainer}>
        <h1 className={styles.title}>Ваши друзья</h1>
        <button className={styles.button} onClick={() => setIsModalOpen(true)}>
          Добавить друга
        </button>
      </div>
      <UserFriendsTable />
      <AddFriendModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} />
    </>
  );
}
