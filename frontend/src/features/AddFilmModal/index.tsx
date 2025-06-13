import { FC, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { axiosSettings } from "@shared/api/axiosSettings";
import { BaseModal } from "@shared/components/ui/BaseModal";
import styles from "./index.module.scss";

interface AddFilmModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess: () => void;
  editMode?: boolean;
  initialData?: Partial<FilmFormData & { id?: number }>;
  onSubmitCustom?: (data: FilmFormData) => Promise<void>;
}

interface Category {
  id: string;
  name: string;
}

interface FilmFormData {
  title: string;
  releaseDate: string;
  imageUrl: string;
  longDescription: string;
  shortDescription: string;
  categoryId: string;
}

export const AddFilmModal: FC<AddFilmModalProps> = ({ isOpen, onClose, onSuccess, editMode = false, initialData, onSubmitCustom }) => {
  const [categories, setCategories] = useState<Category[]>([]);
  const { register, handleSubmit, formState: { errors }, reset, setValue } = useForm<FilmFormData>();

  useEffect(() => {
    if (isOpen) {
      fetchCategories();
      if (editMode && initialData) {
        Object.entries(initialData).forEach(([key, value]) => {
          if (value !== undefined) setValue(key as keyof FilmFormData, value as any);
        });
      } else {
        reset();
      }
    }
  }, [isOpen, editMode, initialData, setValue, reset]);

  useEffect(() => {
    if (editMode && initialData?.categoryId && categories.length > 0) {
      setValue("categoryId", initialData.categoryId);
    }
  }, [categories, editMode, initialData, setValue]);

  const fetchCategories = async () => {
    try {
      const response = await axiosSettings.get("/Category/GetCategory");
      setCategories(response.data);
    } catch (error) {
      console.error("Error fetching categories:", error);
    }
  };

  const onSubmit = async (data: FilmFormData) => {
    // Преобразуем дату в UTC ISO-строку
    if (data.releaseDate) {
      data.releaseDate = new Date(data.releaseDate).toISOString();
    }
    try {
      if (editMode && onSubmitCustom) {
        await onSubmitCustom(data);
      } else {
        await axiosSettings.post("/Film", data);
      }
      reset();
      onSuccess();
      onClose();
    } catch (error) {
      console.error(editMode ? "Error editing film:" : "Error adding film:", error);
    }
  };

  return (
    <BaseModal isOpen={isOpen} onClose={onClose}>
      <div className={styles.modalContent}>
        <h2 className={styles.title}>{editMode ? "Редактировать фильм" : "Добавить новый фильм"}</h2>
        <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
          <div className={styles.formGroup}>
            <label htmlFor="title">Название фильма</label>
            <input
              id="title"
              type="text"
              {...register("title", { required: "Название обязательно" })}
            />
            {errors.title && <span className={styles.error}>{errors.title.message}</span>}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="releaseDate">Дата выпуска</label>
            <input
              id="releaseDate"
              type="date"
              {...register("releaseDate", { required: "Дата выпуска обязательна" })}
            />
            {errors.releaseDate && <span className={styles.error}>{errors.releaseDate.message}</span>}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="imageUrl">URL изображения</label>
            <input
              id="imageUrl"
              type="text"
              {...register("imageUrl", { required: "URL изображения обязателен" })}
            />
            {errors.imageUrl && <span className={styles.error}>{errors.imageUrl.message}</span>}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="shortDescription">Краткое описание</label>
            <textarea
              id="shortDescription"
              {...register("shortDescription", { required: "Краткое описание обязательно" })}
            />
            {errors.shortDescription && <span className={styles.error}>{errors.shortDescription.message}</span>}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="longDescription">Полное описание</label>
            <textarea
              id="longDescription"
              {...register("longDescription", { required: "Полное описание обязательно" })}
            />
            {errors.longDescription && <span className={styles.error}>{errors.longDescription.message}</span>}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="categoryId">Категория</label>
            <select
              id="categoryId"
              {...register("categoryId", { required: "Категория обязательна" })}
            >
              <option value="">Выберите категорию</option>
              {categories.map((category) => (
                <option key={category.id} value={category.id.toString()}>
                  {category.name}
                </option>
              ))}
            </select>
            {errors.categoryId && <span className={styles.error}>{errors.categoryId.message}</span>}
          </div>

          <div className={styles.buttons}>
            <button type="button" onClick={onClose} className={styles.cancelButton}>
              Отмена
            </button>
            <button type="submit" className={styles.submitButton}>
              {editMode ? "Сохранить" : "Добавить"}
            </button>
          </div>
        </form>
      </div>
    </BaseModal>
  );
}; 