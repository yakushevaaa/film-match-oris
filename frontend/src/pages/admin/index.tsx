import { FilmsTable } from "@/features/tables/FilmsTable";
import { AddFilmModal } from "@/features/AddFilmModal";
import { useState, useEffect, useCallback } from "react";
import styles from "./index.module.scss";
import { axiosSettings } from "@/shared/api/axiosSettings";
import { Film } from "@/shared/types/film";
import { CategoryTable } from "@/features/tables/CategoryTable";
import { AddCategoryModal } from "@/features/tables/CategoryTable/AddCategoryModal";

export const Admin = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isCategoryModalOpen, setIsCategoryModalOpen] = useState(false);
  const [films, setFilms] = useState<Film[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [categoryTableKey, setCategoryTableKey] = useState(0);

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

  const handleOpenCategoryModal = () => setIsCategoryModalOpen(true);
  const handleCloseCategoryModal = () => setIsCategoryModalOpen(false);
  const handleCategorySuccess = () => {
    setCategoryTableKey((k) => k + 1); // перерисовать таблицу категорий
    handleCloseCategoryModal();
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
      <div className={styles.header} style={{ marginTop: 40 }}>
        <h1>Управление категориями</h1>
        <button className={styles.addButton} onClick={handleOpenCategoryModal}>
          Добавить категорию
        </button>
      </div>
      <CategoryTable key={categoryTableKey} />
      <AddCategoryModal
        isOpen={isCategoryModalOpen}
        onClose={handleCloseCategoryModal}
        onSuccess={handleCategorySuccess}
      />
    </div>
  );
};
