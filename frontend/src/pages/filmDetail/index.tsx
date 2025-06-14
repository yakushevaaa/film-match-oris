import { InteractiveCard } from "@features/InteractiveCard";
import styles from "./index.module.scss";
import { useParams } from "react-router-dom";
import { useState, useEffect } from "react";
import { Film } from "@/entities/film";
import { axiosSettings } from "@shared/api/axiosSettings";
import { useFilmActions } from "@/entities/film/useFilmActions";

export const FilmDetail = () => {
  const { id } = useParams();
  const [filmData, setFilmData] = useState<Film | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  const { handleLike, handleDislike, handleFavorite } = useFilmActions();

  useEffect(() => {
    const fetchFilm = async () => {
      if (!id) return;
      setIsLoading(true);
      try {
        const response = await axiosSettings.get<Film>(`/Film/${id}`);
        setFilmData(response.data);
      } catch (error) {
        console.error("Error fetching film:", error);
      } finally {
        setIsLoading(false);
      }
    };
    fetchFilm();
  }, [id]);

  if (isLoading || !filmData) {
    return <div>Загрузка...</div>;
  }

  return (
    <div>
      <InteractiveCard
        filmData={filmData}
        onLike={handleLike}
        onDislike={handleDislike}
        onFavorite={handleFavorite}
      />
    </div>
  );
};
