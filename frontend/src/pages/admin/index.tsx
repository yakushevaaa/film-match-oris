import { FilmsTable } from "@/features/FilmsTable";
import { AddFilmModal } from "@/features/AddFilmModal";
import { useState } from "react";
import styles from "./index.module.scss";

export const Admin = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleOpenModal = () => setIsModalOpen(true);
  const handleCloseModal = () => setIsModalOpen(false);
  const handleSuccess = () => {
    // Здесь можно добавить обновление таблицы фильмов
    handleCloseModal();
  };

  return (
    <div className={styles.admin}>
      <div className={styles.header}>
        <h1>Управление фильмами</h1>
        <button className={styles.addButton} onClick={handleOpenModal}>
          Добавить фильм
        </button>
      </div>
      <FilmsTable />
      <AddFilmModal
        isOpen={isModalOpen}
        onClose={handleCloseModal}
        onSuccess={handleSuccess}
      />
    </div>
  );
};
