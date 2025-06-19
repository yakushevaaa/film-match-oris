import Modal from "react-modal";
import { useEffect, useState } from "react";
import { axiosSettings } from "@shared/api/axiosSettings";
import styles from "./index.module.scss";

interface RequestsModalProps {
  isOpen: boolean;
  onClose: () => void;
  isSend: boolean;
}

interface FriendRequest {
  id: string;
  senderName: string;
  receiverName: string;
  // Добавьте другие нужные поля
}

export const RequestsModal = ({ isOpen, onClose, isSend }: RequestsModalProps) => {
  const [requests, setRequests] = useState<FriendRequest[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const fetchRequests = async () => {
    setLoading(true);
    setError("");
    try {
      const res = await axiosSettings.get(`/allFriendRequests`, { params: { IsSend: isSend } });
      setRequests(res.data.requests || []);
    } catch {
      setError("Не удалось загрузить заявки");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (isOpen) fetchRequests();
    // eslint-disable-next-line
  }, [isOpen, isSend]);

  const handleAccept = async (id: string) => {
    try {
      await axiosSettings.post(`/accept?RequestId=${id}`);
      await fetchRequests();
    } catch {
      alert('Ошибка при принятии заявки');
    }
  };

  const handleDecline = async (id: string) => {
    try {
      await axiosSettings.post(`/decline?RequestId=${id}`);
      await fetchRequests();
    } catch {
      alert('Ошибка при отклонении заявки');
    }
  };

  return (
    <Modal
      isOpen={isOpen}
      onRequestClose={onClose}
      className={styles.modalContent}
      overlayClassName={styles.modalOverlay}
      contentLabel={isSend ? "Отправленные заявки" : "Входящие заявки"}
      shouldCloseOnOverlayClick={true}
    >
      <button className={styles.closeButton} onClick={onClose}>
        ×
      </button>
      <h2 className={styles.title}>{isSend ? "Отправленные заявки" : "Входящие заявки"}</h2>
      <div style={{ display: 'flex', justifyContent: 'center', gap: 8, marginBottom: 16 }}>
        <button onClick={() => window.dispatchEvent(new CustomEvent('switchRequestsModal', { detail: true }))} disabled={isSend}>Отправленные</button>
        <button onClick={() => window.dispatchEvent(new CustomEvent('switchRequestsModal', { detail: false }))} disabled={!isSend}>Входящие</button>
      </div>
      {loading ? (
        <div>Загрузка...</div>
      ) : error ? (
        <div className={styles.error}>{error}</div>
      ) : (
        <ul className={styles.userList}>
          {requests.length > 0 ? (
            requests.map((req) => (
              <li key={req.id} className={styles.userItem}>
                {isSend ? (
                  <span>Вы отправили заявку: {req.receiverName}</span>
                ) : (
                  <>
                    <span>Вам отправил заявку: {req.senderName}</span>
                    <button onClick={() => handleAccept(req.id)} style={{marginLeft: 8}}>Принять</button>
                    <button onClick={() => handleDecline(req.id)} style={{marginLeft: 4}}>Отклонить</button>
                  </>
                )}
              </li>
            ))
          ) : (
            <li className={styles.noResults}>Заявки не найдены</li>
          )}
        </ul>
      )}
    </Modal>
  );
}; 