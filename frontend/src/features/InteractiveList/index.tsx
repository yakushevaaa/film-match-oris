import { useEffect, useState } from "react";
import { axiosSettings } from "@shared/api/axiosSettings";
import { InteractiveCard } from "./InteractiveCard";
import { useFilmActions } from "@/entities/film/useFilmActions";

export const InteractiveList = () => {
  const [films, setFilms] = useState<any[]>([]);
  const [currentIndex, setCurrentIndex] = useState(0);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [noNewFilms, setNoNewFilms] = useState(false);
  const [swipeDirection, setSwipeDirection] = useState<"left" | "right" | null>(null);

  const { handleLike, handleDislike, handleFavorite } = useFilmActions();

  useEffect(() => {
    const fetchRecommendations = async () => {
      setLoading(true);
      setError("");
      setNoNewFilms(false);
      try {
        const response = await axiosSettings.get("/Film/recommendations");
        const { recommendations, isEverythingReacted } = response.data;

        if (isEverythingReacted === true) {
          // Пользователь не поставил ни одного лайка — показываем все фильмы
          const allFilmsResp = await axiosSettings.get("/Film/GetAllFilms");
          setFilms(allFilmsResp.data);
        } else if (isEverythingReacted === false) {
          // Нет новых фильмов для пользователя
          setNoNewFilms(true);
          setFilms([]);
        } else {
          // Показываем рекомендации
          setFilms(recommendations || []);
        }
        setCurrentIndex(0);
      } catch (err) {
        setError("Не удалось загрузить рекомендации");
      } finally {
        setLoading(false);
      }
    };
    fetchRecommendations();
  }, []);

  const handleAction = async (
    action: (id: string | number) => Promise<void>,
    id: string | number,
    direction: "left" | "right"
  ) => {
    setSwipeDirection(direction);
    await action(id);
    // Переключение фильма произойдет после анимации
  };

  const handleAnimationEnd = () => {
    setSwipeDirection(null);
    setCurrentIndex((prev) => prev + 1);
  };

  if (loading) return <div>Загрузка...</div>;
  if (error) return <div>{error}</div>;
  if (noNewFilms) return <div>Пока нет новых фильмов для вас</div>;
  if (!films.length || currentIndex >= films.length) return <div>Нет фильмов для отображения</div>;

  const film = films[currentIndex];

  return (
    <InteractiveCard
      filmData={{
        id: film.id,
        title: film.title,
        releaseDate: film.releaseDate || "",
        imageUrl: film.imageUrl || film.image || "",
        descriptionLong: film.descriptionLong || film.longDescription || film.description || "",
        descriptionShort: film.descriptionShort || film.shortDescription || "",
        shortDescription: film.shortDescription || film.descriptionShort || "",
        longDescription: film.longDescription || film.descriptionLong || film.description || "",
        categoryName: film.category?.name || film.category || "",
        category: film.category,
      }}
      onLike={(id) => handleAction(handleLike, id, "right")}
      onDislike={(id) => handleAction(handleDislike, id, "left")}
      onFavorite={(id) => handleAction(handleFavorite, id, "right")}
      swipeDirection={swipeDirection}
      onAnimationEnd={handleAnimationEnd}
    />
  );
};