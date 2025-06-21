import { Search } from "@shared/components/ui/Search";
import { CheckboxFiltersList } from "@features/CheckboxFiltersList";
import { FilmsList } from "@features/FilmsList";
import styles from "./index.module.scss";
import { Film } from "@/entities/film";
import { useEffect, useState, useCallback } from "react";
import { axiosSettings } from "@shared/api/axiosSettings";
import { useSearchParams } from "react-router-dom";

export const FilmPage = () => {
  const [films, setFilms] = useState<Film[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [genreFilters, setGenreFilters] = useState<{ label: string; value: string }[]>([]);
  const [selectedGenres, setSelectedGenres] = useState<string[]>([]);
  const [searchValue, setSearchValue] = useState<string>("");
  const [searchParams, setSearchParams] = useSearchParams();

  // Инициализация состояния из query-параметров
  useEffect(() => {
    const genres = searchParams.get("categoryId");
    const search = searchParams.get("search");
    setSelectedGenres(genres ? genres.split(",") : []);
    setSearchValue(search || "");
  }, []);

  useEffect(() => {
    const fetchGenres = async () => {
      try {
        const response = await axiosSettings.get("/Category/GetCategory");
        setGenreFilters(
          (response.data || []).map((cat: any) => ({ label: cat.name, value: cat.id }))
        );
      } catch (error) {
        console.error("Error fetching genres:", error);
      }
    };
    fetchGenres();
  }, []);

  const fetchFilms = useCallback(async () => {
    setIsLoading(true);
    try {
      const params: any = {};
      if (selectedGenres.length > 0) params.categoryId = selectedGenres.join(",");
      if (searchValue) params.search = searchValue;
      const response = await axiosSettings.get<Film[]>("/Film/GetAllFilms", { params });
      setFilms(response.data);
    } catch (error) {
      console.error("Error fetching films:", error);
    } finally {
      setIsLoading(false);
    }
  }, [selectedGenres, searchValue]);

  useEffect(() => {
    fetchFilms();
  }, [fetchFilms]);

  // Синхронизация состояния с query-параметрами
  useEffect(() => {
    const params: any = {};
    if (selectedGenres.length > 0) params.categoryId = selectedGenres.join(",");
    if (searchValue) params.search = searchValue;
    setSearchParams(params, { replace: true });
  }, [selectedGenres, searchValue, setSearchParams]);

  const handleGenreChange = (value: string) => {
    setSelectedGenres((prev) =>
      prev.includes(value) ? prev.filter((v) => v !== value) : [...prev, value]
    );
  };

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchValue(e.target.value);
  };

  return (
    <div className={styles.film_page_container}>
      <div className={styles.empty}></div>
      <div className={styles.top__container}>
        <Search
          width={500}
          placeholder="Найдите фильм"
          value={searchValue}
          onChange={handleSearchChange}
        />
      </div>

      <CheckboxFiltersList
        className={styles.filters}
        title="Жанры"
        items={genreFilters}
        onChange={handleGenreChange}
        value={selectedGenres}
      />
      <FilmsList className={styles.films} films={films} isInteractive={true} />
    </div>
  );
};
