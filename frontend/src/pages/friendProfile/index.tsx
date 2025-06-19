import { FilmsList } from "@/features/FilmsList";
import styles from "./index.module.scss";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { axiosSettings } from "@/shared/api/axiosSettings";
import { Film } from "@/entities/film";

export const FriendProfile = () => {
  const { id } = useParams();
  const [films, setFilms] = useState<Film[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    setError("");
    axiosSettings
      .get(`/Film/LikedBy/${id}`)
      .then((res) => setFilms(res.data.films || []))
      .catch(() => setError("Не удалось загрузить фильмы"))
      .finally(() => setLoading(false));
  }, [id]);

  return (
    <div className={styles.content}>
      {loading ? (
        <div>Загрузка...</div>
      ) : error ? (
        <div style={{ color: '#e11d48', textAlign: 'center' }}>{error}</div>
      ) : films.length === 0 ? (
        <div style={{ color: '#b0b0b0', textAlign: 'center', fontStyle: 'italic', marginTop: 32 }}>
          Пользователь еще не оценил никакие фильмы
        </div>
      ) : (
        <FilmsList films={films} />
      )}
    </div>
  );
};
