import { FilmsTable } from "@/features/FilmsTable";
import { AddFilmModal } from "@/features/AddFilmModal";
import { useState, useEffect, useCallback } from "react";
import styles from "./index.module.scss";
import { axiosSettings } from "@/shared/api/axiosSettings";
import { Film } from "@/shared/types/film";

export const Admin = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [films, setFilms] = useState<Film[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  const fetchFilms = useCallback(async () => {
    setIsLoading(true);
    try {
      const response = await axiosSettings.get<Film[]>("/Film/GetAllFilms");
      setFilms(response.data);
    } catch (error) {
      console.error("Error fetching films:", error);
    } finally {
      setIsLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchFilms();
  }, [fetchFilms]);

  const handleOpenModal = () => setIsModalOpen(true);
  const handleCloseModal = () => setIsModalOpen(false);
  const handleSuccess = () => {
    fetchFilms();
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
      <FilmsTable films={films} isLoading={isLoading} fetchFilms={fetchFilms} />
      <AddFilmModal
        isOpen={isModalOpen}
        onClose={handleCloseModal}
        onSuccess={handleSuccess}
      />
    </div>
  );
};
