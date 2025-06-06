import { Film } from "@entities/film";
import { FilmsListItem } from "./components/FilmsListItem";
import styles from "./index.module.scss";
import { useFilmActions } from "@entities/film/useFilmActions";

export const FilmsList = () => {
  const { handleLike, handleDislike, handleFavorite } = useFilmActions();

  const filmsData: Film[] = [
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

  return (
    <ul className={styles.list}>
      {filmsData.map((item) => (
        <li key={item.id} className={styles.list__item}>
          <FilmsListItem
            onLike={handleLike}
            onDislike={handleDislike}
            onFavorite={handleFavorite}
            filmData={item}
          />
        </li>
      ))}
    </ul>
  );
};
