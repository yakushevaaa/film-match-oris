import { Search } from "@shared/components/ui/Search";
import { Filters } from "@features/Filters";
import { CheckboxFiltersList } from "@features/CheckboxFiltersList";
import { GENRE_FILTERS } from "@features/CheckboxFiltersList/consts/filters";
import { FilmsList } from "@features/FilmsList";
import styles from "./index.module.scss";
import { Film } from "@/entities/film";
import { useEffect, useState } from "react";
import { axiosSettings } from "@shared/api/axiosSettings";

export const FilmPage = () => {
  const [films, setFilms] = useState<Film[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchFilms = async () => {
      setIsLoading(true);
      try {
        const response = await axiosSettings.get<Film[]>("/Film/GetAllFilms");
        setFilms(response.data);
      } catch (error) {
        console.error("Error fetching films:", error);
      } finally {
        setIsLoading(false);
      }
    };
    fetchFilms();
  }, []);

  return (
    <div className={styles.film_page_container}>
      <div className={styles.empty}></div>
      <div className={styles.top__container}>
        <Search width={500} placeholder="Найдите фильм" />
        <Filters />
      </div>

      <CheckboxFiltersList
        className={styles.filters}
        title="Жанры"
        items={GENRE_FILTERS}
      />
      <FilmsList className={styles.films} films={films} isInteractive={true} />
    </div>
  );
};
