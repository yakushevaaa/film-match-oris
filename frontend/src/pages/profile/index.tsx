import { FilmsList } from "@/features/FilmsList";
import { useEffect, useState } from "react";
import { axiosSettings } from "@shared/api/axiosSettings";
import { Film } from "@/entities/film";
import styles from "./index.module.scss";

export const Profile = () => {
  const [likedFilms, setLikedFilms] = useState<Film[]>([]);
  const [bookmarkedFilms, setBookmarkedFilms] = useState<Film[]>([]);
  const [dislikedFilms, setDislikedFilms] = useState<Film[]>([]);

  useEffect(() => {
    const fetchLikedFilms = async () => {
      try {
        const response = await axiosSettings.get("/Film/AllLikedFilms");
        setLikedFilms(response.data.films || []);
      } catch (error) {
        console.error("Ошибка при загрузке лайкнутых фильмов:", error);
      }
    };
    fetchLikedFilms();
  }, []);

  useEffect(() => {
    const fetchBookmarkedFilms = async () => {
      try {
        const response = await axiosSettings.get("/Film/Bookmarked");
        const data = response.data;
        setBookmarkedFilms(Array.isArray(data) ? data : data.films || []);
      } catch (error) {
        console.error("Ошибка при загрузке фильмов из закладок:", error);
      }
    };
    fetchBookmarkedFilms();
  }, []);

  useEffect(() => {
    const fetchDislikedFilms = async () => {
      try {
        const response = await axiosSettings.get("/Film/AllDislikedFilms");
        setDislikedFilms(response.data.films || []);
      } catch (error) {
        console.error("Ошибка при загрузке дизлайкнутых фильмов:", error);
      }
    };
    fetchDislikedFilms();
  }, []);

  return (
    <div className={styles.contentContainer}>
      <h2 className={styles.filmsTitle}>Понравившиеся фильмы</h2>
      {likedFilms.length === 0 ? (
        <div
          style={{
            color: "#b0b0b0",
            textAlign: "center",
            fontStyle: "italic",
            margin: "24px 0",
          }}
        >
          Список понравившихся фильмов пуст
        </div>
      ) : (
        <FilmsList films={likedFilms} />
      )}
      <h2 className={styles.filmsTitle}>Фильмы в закладках</h2>
      {bookmarkedFilms.length === 0 ? (
        <div
          style={{
            color: "#b0b0b0",
            textAlign: "center",
            fontStyle: "italic",
            margin: "24px 0",
          }}
        >
          Список фильмов в закладках пуст
        </div>
      ) : (
        <FilmsList films={bookmarkedFilms} />
      )}
      <h2 className={styles.filmsTitle}>Дизлайкнутые фильмы</h2>
      {dislikedFilms.length === 0 ? (
        <div
          style={{
            color: "#b0b0b0",
            textAlign: "center",
            fontStyle: "italic",
            margin: "24px 0",
          }}
        >
          Список дизлайкнутых фильмов пуст
        </div>
      ) : (
        <FilmsList films={dislikedFilms} />
      )}
    </div>
  );
};
