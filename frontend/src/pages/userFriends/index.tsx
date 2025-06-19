import styles from "./index.module.scss";
import { UserFriendsTable } from "./UserFriendsTable";
import { useState, useEffect } from "react";
import { AddFriendModal } from "./AddFriendModal";
import { RequestsModal } from "./requestsModal";

export function UserFriends() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isRequestsModalOpen, setIsRequestsModalOpen] = useState(false);
  const [isSend, setIsSend] = useState(true);

  useEffect(() => {
    const handler = (e: any) => setIsSend(e.detail);
    window.addEventListener('switchRequestsModal', handler);
    return () => window.removeEventListener('switchRequestsModal', handler);
  }, []);

  return (
    <>
      <div className={styles.titleContainer}>
        <h1 className={styles.title}>Ваши друзья</h1>
        <button className={styles.button} onClick={() => setIsModalOpen(true)}>
          Добавить друга
        </button>
        <button className={styles.button} onClick={() => { setIsRequestsModalOpen(true); setIsSend(true); }}>
          Заявки в друзья
        </button>
      </div>
      <UserFriendsTable />
      <AddFriendModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} />
      <RequestsModal isOpen={isRequestsModalOpen} onClose={() => setIsRequestsModalOpen(false)} isSend={isSend} />
    </>
  );
}
