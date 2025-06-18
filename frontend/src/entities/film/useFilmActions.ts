import { axiosSettings } from "@shared/api/axiosSettings";

export const useFilmActions = () => {
  const handleLike = async (id: string | number) => {
    console.log('Клик по лайку', id);
    try {
      await axiosSettings.post(`/Film/Like/${id}`);
      console.log(`Лайк: ${id}`);
    } catch (error) {
      console.error('Ошибка при лайке фильма:', error);
    }
  };

  const handleDislike = async (id: string | number) => {
    console.log('Клик по дизлайку', id);
    try {
      await axiosSettings.post(`/Film/Dislike/${id}`);
      console.log(`Дизлайк: ${id}`);
    } catch (error) {
      console.error('Ошибка при дизлайке фильма:', error);
    }
  };

  const handleFavorite = async (id: string | number) => {
    console.log('Клик по закладке', id);
    try {
      await axiosSettings.post(`/Film/Bookmark/${id}`);
      console.log(`Добавлено в избранное: ${id}`);
    } catch (error) {
      console.error('Ошибка при добавлении в избранное:', error);
    }
  };

  return {
    handleLike,
    handleDislike,
    handleFavorite,
  };
};
