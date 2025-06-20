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
  const [username, setUsername] = useState<string>("");

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    setError("");
    axiosSettings
      .get(`/User/UsernameById/${id}`)
      .then((res) => setUsername(res.data.username || ""))
      .catch(() => setUsername(""));
    axiosSettings
      .get(`/Film/AllLikedFilms`, { params: { userId: id } })
      .then((res) => setFilms(res.data.films || []))
      .catch(() => setError("Не удалось загрузить фильмы"))
      .finally(() => setLoading(false));
  }, [id]);

  return (
    <div className={styles.content}>
      {username && (
        <h2 className={styles.profileTitle}>Профиль пользователя {username}</h2>
      )}
      {loading ? (
        <div>Загрузка...</div>
      ) : error ? (
        <div style={{ color: "#e11d48", textAlign: "center" }}>{error}</div>
      ) : films.length === 0 ? (
        <div
          style={{
            color: "#b0b0b0",
            textAlign: "center",
            fontStyle: "italic",
            marginTop: 32,
          }}
        >
          Пользователь еще не оценил никакие фильмы
        </div>
      ) : (
        <FilmsList className={styles.list} films={films} />
      )}
    </div>
  );
};
