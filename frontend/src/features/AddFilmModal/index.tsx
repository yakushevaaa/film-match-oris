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
  initialData?: Partial<FilmFormData & { id?: number; imageUrl?: string }>;
  onSubmitCustom?: (data: FilmFormData) => Promise<void>;
}

interface Category {
  id: string;
  name: string;
}

interface FilmFormData {
  title: string;
  releaseDate: string;
  longDescription: string;
  shortDescription: string;
  categoryId: string;
}

export const AddFilmModal: FC<AddFilmModalProps> = ({ isOpen, onClose, onSuccess, editMode = false, initialData, onSubmitCustom }) => {
  const [categories, setCategories] = useState<Category[]>([]);
  const { register, handleSubmit, formState: { errors }, reset, setValue } = useForm<FilmFormData>();
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [imageUploadError, setImageUploadError] = useState<string | null>(null);
  const [imagePreview, setImagePreview] = useState<string>("");
  const [imageRemoved, setImageRemoved] = useState(false);

  useEffect(() => {
    if (isOpen) {
      fetchCategories();
      if (editMode && initialData) {
        Object.entries(initialData).forEach(([key, value]) => {
          if (value !== undefined) setValue(key as keyof FilmFormData, value as any);
        });
        setImagePreview(initialData.imageUrl || "");
        setImageRemoved(false);
        setSelectedFile(null);
      } else {
        reset();
        setSelectedFile(null);
        setImagePreview("");
        setImageRemoved(false);
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

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) {
      setSelectedFile(e.target.files[0]);
      setImageUploadError(null);
      setImagePreview(URL.createObjectURL(e.target.files[0]));
      setImageRemoved(false);
    }
  };

  const handleRemoveImage = () => {
    setSelectedFile(null);
    setImagePreview("");
    setImageRemoved(true);
  };

  const onSubmit = async (data: FilmFormData) => {
    if (data.releaseDate) {
      data.releaseDate = new Date(data.releaseDate).toISOString();
    }
    const formData = new FormData();
    formData.append("title", data.title);
    formData.append("releaseDate", data.releaseDate);
    formData.append("longDescription", data.longDescription);
    formData.append("shortDescription", data.shortDescription);
    formData.append("categoryId", data.categoryId);
    if (selectedFile) {
      formData.append("image", selectedFile);
    } else if (editMode && imagePreview && !imageRemoved) {
      formData.append("imageUrl", imagePreview); // если сервер поддерживает imageUrl
    } else if (!selectedFile && !imagePreview && !editMode) {
      setImageUploadError("Пожалуйста, выберите изображение");
      return;
    }
    try {
      if (editMode && onSubmitCustom) {
        await onSubmitCustom(data);
      } else {
        await axiosSettings.post("/Film", formData, {
          headers: { "Content-Type": "multipart/form-data" },
        });
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

          <div className={styles.formGroup}>
            <label htmlFor="imageFile">Изображение</label>
            {imagePreview && (
              <div className={styles.imagePreview}>
                <img
                  src={imagePreview}
                  alt="Превью"
                  style={{ maxWidth: 200, marginTop: 8 }}
                />
                <button type="button" onClick={handleRemoveImage} className={styles.removeImageButton}>
                  Удалить фото
                </button>
              </div>
            )}
            <input
              id="imageFile"
              type="file"
              accept="image/*"
              onChange={handleFileChange}
              // required={!imagePreview && !editMode}
            />
            {imageUploadError && <span className={styles.error}>{imageUploadError}</span>}
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