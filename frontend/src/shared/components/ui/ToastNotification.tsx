import { FC, useEffect } from "react";
import styles from "./ToastNotification.module.scss";

interface ToastNotificationProps {
  message: string;
  onClose: () => void;
}

export const ToastNotification: FC<ToastNotificationProps> = ({ message, onClose }) => {
  useEffect(() => {
    const timer = setTimeout(onClose, 4000);
    return () => clearTimeout(timer);
  }, [onClose]);

  return (
    <div className={styles.toast}>
      {message}
      <button className={styles.close} onClick={onClose}>×</button>
    </div>
  );
}; 