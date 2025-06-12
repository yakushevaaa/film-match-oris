import { Search } from "@shared/components/ui/Search";
import { Filters } from "@features/Filters";
import { CheckboxFiltersList } from "@features/CheckboxFiltersList";
import { GENRE_FILTERS } from "@features/CheckboxFiltersList/consts/filters";
import { FilmsList } from "@features/FilmsList";
import styles from "./index.module.scss";
import { Film } from "@/entities/film";
const films: Film[] = [
  {
    id: 1,
    title: "Film Name",
    releaseDate: "2022",
    imageUrl: "/images/film-img.png",
    descriptionShort: "Описание",
    descriptionLong: "Длинное описание",
    categoryName: "Комедия",
  },
  {
    id: 2,
    title: "Film Name",
    releaseDate: "2022",
    imageUrl: "/images/film-img.png",
    descriptionShort: "Описание",
    descriptionLong: "Длинное описание",
    categoryName: "Комедия",
  },
  {
    id: 3,
    title: "Film Name",
    releaseDate: "2022",
    imageUrl: "/images/film-img.png",
    descriptionShort: "Описание",
    descriptionLong: "Длинное описание",
    categoryName: "Комедия",
  },
  {
    id: 4,
    title: "Film Name",
    releaseDate: "2022",
    imageUrl: "/images/film-img.png",
    descriptionShort: "Описание",
    descriptionLong: "Длинное описание",
    categoryName: "Комедия",
  },
  {
    id: 5,
    title: "Film Name",
    releaseDate: "2022",
    imageUrl: "/images/film-img.png",
    descriptionShort: "Описание",
    descriptionLong: "Длинное описание",
    categoryName: "Комедия",
  },
];
export const FilmPage = () => {
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
