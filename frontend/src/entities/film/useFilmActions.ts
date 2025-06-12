export const useFilmActions = () => {
  const handleLike = (id: number) => {
    console.log(`Лайк: ${id}`);
  };

  const handleDislike = (id: number) => {
    console.log(`Дизлайк: ${id}`);
  };

  const handleFavorite = (id: number) => {
    console.log(`Добавлено в избранное: ${id}`);
  };

  return {
    handleLike,
    handleDislike,
    handleFavorite,
  };
};
