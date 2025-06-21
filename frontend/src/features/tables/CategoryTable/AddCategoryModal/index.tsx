import { FC, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { axiosSettings } from "@shared/api/axiosSettings";
import { BaseModal } from "@shared/components/ui/BaseModal";
import styles from "./index.module.scss";

interface AddCategoryModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSuccess: () => void;
  editMode?: boolean;
  initialData?: Partial<CategoryFormData & { id?: string; imageUrl?: string }>;
  onSubmitCustom?: (data: CategoryFormData) => Promise<void>;
}

interface CategoryFormData {
  name: string;
}

export const AddCategoryModal: FC<AddCategoryModalProps> = ({ isOpen, onClose, onSuccess, editMode = false, initialData, onSubmitCustom }) => {
  const { register, handleSubmit, formState: { errors }, reset, setValue } = useForm<CategoryFormData>();
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [imageUploadError, setImageUploadError] = useState<string | null>(null);
  const [imagePreview, setImagePreview] = useState<string>("");
  const [imageRemoved, setImageRemoved] = useState(false);

  useEffect(() => {
    if (isOpen) {
      if (editMode && initialData) {
        Object.entries(initialData).forEach(([key, value]) => {
          if (value !== undefined) setValue(key as keyof CategoryFormData, value as any);
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

  const onSubmit = async (data: CategoryFormData) => {
    const formData = new FormData();
    formData.append("name", data.name);
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
        await axiosSettings.post("/Category", formData, {
          headers: { "Content-Type": "multipart/form-data" },
        });
      }
      reset();
      onSuccess();
      onClose();
    } catch (error) {
      console.error(editMode ? "Error editing category:" : "Error adding category:", error);
    }
  };

  return (
    <BaseModal isOpen={isOpen} onClose={onClose}>
      <div className={styles.modalContent}>
        <h2 className={styles.title}>{editMode ? "Редактировать категорию" : "Добавить новую категорию"}</h2>
        <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
          <div className={styles.formGroup}>
            <label htmlFor="name">Название категории</label>
            <input
              id="name"
              type="text"
              {...register("name", { required: "Название обязательно" })}
            />
            {errors.name && <span className={styles.error}>{errors.name.message}</span>}
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
