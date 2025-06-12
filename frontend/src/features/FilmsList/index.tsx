import { FilmsListItem } from "./components/FilmsListItem";
import styles from "./index.module.scss";
import { useFilmActions } from "@entities/film/useFilmActions";
import { FC } from "react";
import cn from "classnames";
import { Film } from "@/entities/film";
import { Link } from "react-router-dom";

interface FilmsListProps {
  className?: string;
  films: Film[];
  isInteractive?: boolean;
}

export const FilmsList: FC<FilmsListProps> = ({
  className,
  isInteractive,
  films,
}) => {
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
    <ul className={cn(styles.list, className)}>
      {films.map((item) => (
        <li key={item.id} className={styles.list__item}>
          <Link to={`${item.id}`}>
            <FilmsListItem
              // onLike={handleLike}
              // onDislike={handleDislike}
              // onFavorite={handleFavorite}
              onLike={isInteractive ? handleLike : undefined}
              onDislike={isInteractive ? handleDislike : undefined}
              onFavorite={isInteractive ? handleFavorite : undefined}
              filmData={item}
            />
          </Link>
        </li>
      ))}
    </ul>
  );
};
