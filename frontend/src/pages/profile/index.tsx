import { FilmsList } from "@/features/FilmsList";
import { useEffect, useState } from "react";
import { axiosSettings } from "@shared/api/axiosSettings";
import { Film } from "@/entities/film";
import styles from "./index.module.scss";

export const Profile = () => {
  const [likedFilms, setLikedFilms] = useState<Film[]>([]);
  const [bookmarkedFilms, setBookmarkedFilms] = useState<Film[]>([]);

  useEffect(() => {
    const fetchLikedFilms = async () => {
      try {
        const response = await axiosSettings.get<Film[]>("/Film/LikedBy/me");
        setLikedFilms(response.data);
      } catch (error) {
        console.error("Ошибка при загрузке лайкнутых фильмов:", error);
      }
    };
    fetchLikedFilms();
  }, []);

  useEffect(() => {
    const fetchBookmarkedFilms = async () => {
      try {
        const response = await axiosSettings.get<Film[]>("/Film/Bookmarked");
        setBookmarkedFilms(response.data);
      } catch (error) {
        console.error("Ошибка при загрузке фильмов из закладок:", error);
      }
    };
    fetchBookmarkedFilms();
  }, []);

  return (
    <div className={styles.contentContainer}>
      <h2>Понравившиеся фильмы</h2>
      <FilmsList films={likedFilms} />
      <h2>Фильмы в закладках</h2>
      <FilmsList films={bookmarkedFilms} />
    </div>
  );
};
